using UnityEngine;

public class GodHandHeightAdjuster : MonoBehaviour, IHeightAdjustable
{
    [field: SerializeField] public Transform Target;
    [field: SerializeField] public float MinY = 0.2f;
    [field: SerializeField] public float MaxY = 2f;
    [field: SerializeField] public float MoveUpSpeed = 5f;
    [field: SerializeField] public float MoveDownSpeed = 8f;

    public void AdjustHeight(float direction, float speed, float deltaTime)
    {
        if (Target == null || Mathf.Approximately(direction, 0f))
            return;

        float currentY = Target.localPosition.y;
        float newY = currentY + direction * speed * deltaTime;
        newY = Mathf.Clamp(newY, MinY, MaxY);

        Vector3 localPos = Target.localPosition;
        localPos.y = newY;
        Target.localPosition = localPos;
    }
}
