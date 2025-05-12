using UnityEngine;

public class GodHandHeightAdjuster : MonoBehaviour, IHeightAdjustable
{
    [SerializeField] private Transform _target;
    [field: SerializeField] public float MinY = 0.2f;
    [field: SerializeField] public float MaxY = 2f;
    [field: SerializeField] public float MoveUpSpeed = 5f;
    [field: SerializeField] public float MoveDownSpeed = 8f;

    private float _currentSpeed;
    private float _targetY;
    private bool _isMoving;

    public void MoveToHeight(float targetY, float speed)
    {
        _targetY = Mathf.Clamp(targetY, MinY, MaxY);
        _currentSpeed = Mathf.Abs(speed);
        _isMoving = true;
    }

    public void Tick(float deltaTime)
    {
        if (!_isMoving || _target == null)
            return;

        float currentY = _target.localPosition.y;
        float newY = Mathf.MoveTowards(currentY, _targetY, _currentSpeed * deltaTime);

        Vector3 localPos = _target.localPosition;
        localPos.y = newY;
        _target.localPosition = localPos;

        if (Mathf.Approximately(newY, _targetY))
            _isMoving = false;
    }
}

