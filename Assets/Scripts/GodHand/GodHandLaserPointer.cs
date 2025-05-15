using UnityEngine;

public class GodHandLaserPointer : MonoBehaviour
{
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private GameObject _laserDot;
    [SerializeField] private float _maxDistance = 100f;

    private void Start()
    {
        _laserDot.SetActive(false);
    }

    private void Update()
    {
        if (Physics.Raycast(_rayOrigin.position, Vector3.down, out RaycastHit hit, _maxDistance, _aimLayer))
        {
            _laserDot.SetActive(true);
            _laserDot.transform.position = hit.point + Vector3.up * 0.01f;
            _laserDot.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            _laserDot.SetActive(false);
        }
    }
}
