using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int count = 5;
    [SerializeField] private float spawnSpread = 0.5f;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            SpawnObjects();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnSpread;
            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward + randomOffset;

            GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 throwDirection = (Vector3.down + Random.insideUnitSphere * 0.2f).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            }
        }
    }
}
