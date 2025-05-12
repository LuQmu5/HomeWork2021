using UnityEngine;

public class GodHandHeightAdjuster : MonoBehaviour, IHeightAdjustable
{
    [field: SerializeField] public Transform Target;
    [field: SerializeField] public float MinY = -2.5f;
    [field: SerializeField] public float MaxY = 5f;
    [field: SerializeField] public float MoveUpSpeed = 5f;
    [field: SerializeField] public float MoveDownSpeed = 8f;

    [SerializeField] private BlockingRaycastChecker _blockingRaycastChecker;

    public void AdjustHeight(float direction, float speed, float deltaTime)
    {
        if (Target == null || Mathf.Approximately(direction, 0f))
            return;

        if (direction < 0 && _blockingRaycastChecker.IsBlocked())
            return;

        float currentY = Target.localPosition.y;
        float newY = currentY + direction * speed * deltaTime;
        newY = Mathf.Clamp(newY, MinY, MaxY);

        Vector3 localPos = Target.localPosition;
        localPos.y = newY;
        Target.localPosition = localPos;
    }
}
