using UnityEngine;

public class TransformRotator : MonoBehaviour
{
    [SerializeField] private Transform checkObject;

    [Header("Rotation Settings")]
    [SerializeField] private Transform[] targets;
    [SerializeField] private float openAngleX = 30f;
    [SerializeField] private float closeAngleX = -15f;
    [SerializeField] private float openSpeed = 90f;
    [SerializeField] private float closeSpeed = 150f;

    [Header("Detection Settings")]
    [SerializeField] private Transform[] detectionPoints;
    [SerializeField] private float detectionRadius = 1f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float detectionOffsetDistance = 1f; // Дистанция оффсета относительно forward
    [SerializeField] private LayerMask blockLayer; // Слой, который блокирует вращение
    [SerializeField] private float raycastMaxDistance = 5f; // Максимальная длина луча

    private void Update()
    {
        bool rightMouseHeld = Input.GetMouseButton(1);
        bool objectDetected = IsAnyObjectDetected();

        // Проверка на блокировку вращения (если луч попадет в объект с нужным слоем)
        if (RaycastUtility.IsObjectBlocking(checkObject.position, -transform.up, blockLayer, raycastMaxDistance))
        {
            // Если луч касается объекта с нужным слоем, останавливаем вращение
            return;
        }

        float targetAngle = rightMouseHeld ? openAngleX : (objectDetected ? openAngleX : closeAngleX);
        float speed = rightMouseHeld ? openSpeed : (objectDetected ? 0f : closeSpeed);

        foreach (Transform t in targets)
        {
            Vector3 currentEuler = t.localEulerAngles;
            currentEuler.x = NormalizeAngle(currentEuler.x);
            float newAngle = Mathf.MoveTowards(currentEuler.x, targetAngle, speed * Time.deltaTime);
            t.localEulerAngles = new Vector3(newAngle, currentEuler.y, currentEuler.z);
        }
    }

    private bool IsAnyObjectDetected()
    {
        foreach (Transform point in detectionPoints)
        {
            if (point != null)
            {
                // Смещение сферы относительно forward объекта
                Vector3 offsetPosition = point.position + point.forward * detectionOffsetDistance;
                if (Physics.CheckSphere(offsetPosition, detectionRadius, detectionLayer))
                    return true;
            }
        }
        return false;
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    private void OnDrawGizmosSelected()
    {
        if (detectionPoints != null)
        {
            Gizmos.color = Color.cyan;
            foreach (Transform point in detectionPoints)
            {
                if (point != null)
                {
                    // Применяем оффсет относительно forward для отрисовки сферы
                    Vector3 offsetPosition = point.position + point.forward * detectionOffsetDistance;
                    Gizmos.DrawWireSphere(offsetPosition, detectionRadius);
                }
            }
        }
    }
}
