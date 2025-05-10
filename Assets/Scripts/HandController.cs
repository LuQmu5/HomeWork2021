using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private float moveSensitivity = 0.01f;
    [SerializeField] private float scrollSpeedUp = 2f;
    [SerializeField] private float scrollSpeedDown = 4f;
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private float minHeight = 0.0f;
    [SerializeField] private float maxHeight = 2.0f;

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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            targetHeight += scroll * 1.5f; // как быстро колесико влияет на высоту
            targetHeight = Mathf.Clamp(targetHeight, minHeight, maxHeight);
        }

        float currentY = transform.localPosition.y;
        float speed = targetHeight > currentY ? scrollSpeedUp : scrollSpeedDown;
        float newY = Mathf.MoveTowards(currentY, targetHeight, speed * Time.deltaTime);

        transform.localPosition = new Vector3(clampedXZ.x, newY, clampedXZ.z);
    }
}
