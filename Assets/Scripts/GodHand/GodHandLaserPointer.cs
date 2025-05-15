using UnityEngine;

public class GodHandLaserPointer : MonoBehaviour
{
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private GameObject _laserDotPrefab;
    [SerializeField] private float _maxDistance = 100f;

    private GameObject _laserDotInstance;

    private void Start()
    {
        _laserDotInstance = Instantiate(_laserDotPrefab);
        _laserDotInstance.SetActive(false);
    }

    private void Update()
    {
        if (Physics.Raycast(_rayOrigin.position, Vector3.down, out RaycastHit hit, _maxDistance, _aimLayer))
        {
            _laserDotInstance.SetActive(true);
            _laserDotInstance.transform.position = hit.point + Vector3.up * 0.01f;
            _laserDotInstance.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
        else
        {
            _laserDotInstance.SetActive(false);
        }
    }
}
