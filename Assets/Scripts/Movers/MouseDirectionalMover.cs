using UnityEngine;

public class MouseDirectionalMover : IMouseMovable
{
    private readonly Transform _transform;
    private readonly float _movementSpeed;
    private readonly float _maxDistance;

    private Vector3 _inputDirection;
    private Vector3 _currentVelocity;
    private Vector3 _startPosition;

    private readonly float _acceleration = 12f;
    private readonly float _deceleration = 8f;

    public MouseDirectionalMover(Transform transform, float movementSpeed, float maxDistance)
    {
        _transform = transform;
        _movementSpeed = movementSpeed;
        _maxDistance = maxDistance;
        _startPosition = transform.position;
    }

    public void Enable()
    {
        _currentVelocity = Vector3.zero;
    }

    public void SetMoveDirection(Vector3 inputDirection)
    {
        _inputDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
    }

    public void Update(float deltaTime)
    {
        Vector3 targetVelocity = _inputDirection * _movementSpeed;

        float lerpFactor = (_inputDirection.magnitude > 0.01f) ? _acceleration : _deceleration;
        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, lerpFactor * deltaTime);

        Vector3 nextPosition = _transform.position + _currentVelocity * deltaTime;

        Vector3 offset = nextPosition - _startPosition;
        if (offset.magnitude > _maxDistance)
        {
            offset = Vector3.ClampMagnitude(offset, _maxDistance);
            nextPosition = _startPosition + offset;

            _currentVelocity = Vector3.zero;
        }

        _transform.position = nextPosition;
    }
}
