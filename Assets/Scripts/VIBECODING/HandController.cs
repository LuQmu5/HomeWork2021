using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private float moveSensitivity = 0.01f;
    [SerializeField] private float scrollSpeedUp = 2f;
    [SerializeField] private float scrollSpeedDown = 4f;
    [SerializeField] private float minHeight = 0.0f;
    [SerializeField] private float maxHeight = 2.0f;
    [SerializeField] private LayerMask blockLayer; // Слой, который блокирует спуск
    [SerializeField] private Vector3 boxCastSize = new Vector3(0.5f, 0.5f, 0.5f); // Размеры коробки для BoxCast
    [SerializeField] private float raycastMaxDistance = 5f; // Максимальная длина BoxCast
    [SerializeField] private Transform[] checkObjectsPoints; // Массив точек, из которых будет исходить BoxCast

    [SerializeField] private float maxDistance = 2f;
    private Vector3 startLocalPosition;
    private float targetHeight;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
        targetHeight = startLocalPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * moveSensitivity;
        float moveZ = Input.GetAxis("Mouse Y") * moveSensitivity;

        Vector3 offset = new Vector3(moveX, 0f, moveZ);
        Vector3 targetXZ = transform.localPosition + offset;
        Vector3 clampedXZ = startLocalPosition + Vector3.ClampMagnitude(targetXZ - startLocalPosition, maxDistance);
        clampedXZ.y = transform.localPosition.y;

        // Получаем значение прокрутки колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Проверка на столкновение с объектом, который блокирует спуск, с использованием BoxCast
            if (RaycastUtility.IsObjectBlockingWithBoxCast(checkObjectsPoints, boxCastSize, blockLayer, raycastMaxDistance) && scroll < 0)
            {
                // Если BoxCast касается объекта с нужным слоем, не изменяем высоту
                return;
            }

            // Если прокрутка колесика изменилась, то учитываем величину прокрутки
            float scrollMultiplier = Mathf.Abs(scroll); // Абсолютное значение прокрутки для определения "силы"
            if (scroll > 0)
            {
                // Если прокрутка вверх (положительное значение), увеличиваем высоту
                targetHeight += scrollMultiplier * scrollSpeedUp;
            }
            else
            {
                // Если прокрутка вниз (отрицательное значение), уменьшаем высоту
                targetHeight -= scrollMultiplier * scrollSpeedDown;
            }

            // Ограничиваем целевую высоту
            targetHeight = Mathf.Clamp(targetHeight, minHeight, maxHeight);
        }

        float currentY = transform.localPosition.y;
        float speed = targetHeight > currentY ? scrollSpeedUp : scrollSpeedDown;
        float newY = Mathf.MoveTowards(currentY, targetHeight, speed * Time.deltaTime);

        transform.localPosition = new Vector3(clampedXZ.x, newY, clampedXZ.z);
    }
}
