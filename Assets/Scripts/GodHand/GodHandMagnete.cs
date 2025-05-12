using System.Collections.Generic;
using UnityEngine;

public class GodHandMagnete : MonoBehaviour
{
    [SerializeField] private Transform[] _grabPoints;
    [SerializeField] private Transform _magnetePoint;
    [SerializeField] private LayerMask _grabbableLayer;

    private readonly Dictionary<Transform, Collider> _pointHits = new();
    private Collider _grabbedObject;
    private Rigidbody _grabbedRigidbody;
    private bool _isFollowing;

    private void Awake()
    {
        foreach (var point in _grabPoints)
        {
            var detector = point.gameObject.AddComponent<GrabPointTrigger>();
            detector.Initialize(this);
            _pointHits[point] = null;
        }
    }

    private void Update()
    {
        if (_isFollowing && _grabbedObject != null)
        {
            _grabbedObject.transform.position = _magnetePoint.position;
        }
    }

    public void OnPointEnter(Transform point, Collider collider)
    {
        if ((_grabbableLayer.value & (1 << collider.gameObject.layer)) == 0)
            return;

        _pointHits[point] = collider;
        CheckForGrab();
    }

    public void OnPointExit(Transform point, Collider collider)
    {
        if (_pointHits[point] == collider)
            _pointHits[point] = null;

        if (_grabbedObject != null && !AllPointsTouchingSameObject(_grabbedObject))
        {
            ReleaseObject();
        }
    }

    private void CheckForGrab()
    {
        Collider first = null;

        foreach (var entry in _pointHits.Values)
        {
            if (entry == null)
                return;

            if (first == null)
                first = entry;
            else if (entry != first)
                return;
        }

        _grabbedObject = first;
        _grabbedObject.transform.position = _magnetePoint.position;
        _grabbedObject.transform.rotation = _magnetePoint.rotation;

        _grabbedRigidbody = _grabbedObject.attachedRigidbody;
        if (_grabbedRigidbody != null)
        {
            _grabbedRigidbody.isKinematic = true;
            _grabbedRigidbody.detectCollisions = false;
        }

        _isFollowing = true;
    }

    private void ReleaseObject()
    {
        if (_grabbedRigidbody != null)
        {
            _grabbedRigidbody.isKinematic = false;
            _grabbedRigidbody.detectCollisions = true;
        }

        _grabbedObject = null;
        _grabbedRigidbody = null;
        _isFollowing = false;
    }

    private bool AllPointsTouchingSameObject(Collider collider)
    {
        foreach (var col in _pointHits.Values)
        {
            if (col == null || col != collider)
                return false;
        }

        return true;
    }
}
