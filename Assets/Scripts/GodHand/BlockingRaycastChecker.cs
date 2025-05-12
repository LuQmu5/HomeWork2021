using UnityEngine;

public class BlockingRaycastChecker : MonoBehaviour
{
    [SerializeField] private Transform[] _grabPoints;
    [SerializeField] private LayerMask _magnetableMask;
    [SerializeField] private float _checkBlockingDistance = 1;

    public bool IsBlocked()
    {
        return RaycastUtility.IsObjectBlockingWithBoxCast(_grabPoints, Vector3.one / 2, _magnetableMask, _checkBlockingDistance);
    }
}