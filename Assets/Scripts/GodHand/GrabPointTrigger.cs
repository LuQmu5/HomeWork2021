using UnityEngine;

public class GrabPointTrigger : MonoBehaviour
{
    private GodHandMagnete _magnete;
    private Transform _point;

    public void Initialize(GodHandMagnete magnete)
    {
        _magnete = magnete;
        _point = transform;

        if (!TryGetComponent<Collider>(out var col))
        {
            col = gameObject.AddComponent<SphereCollider>();
            col.isTrigger = true;
        }
        else
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _magnete.OnPointEnter(_point, other);
    }

    private void OnTriggerExit(Collider other)
    {
        _magnete.OnPointExit(_point, other);
    }
}
