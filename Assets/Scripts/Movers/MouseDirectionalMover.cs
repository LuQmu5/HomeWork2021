// Новый MouseDirectionalMover через Rigidbody
using UnityEngine;

public class MouseDirectionalMover : IMouseMovable
{
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private readonly float _movementForce;
    private readonly float _maxDistance;

    private Vector3 _inputDirection;
    private readonly Vector3 _startPosition;

    private readonly float _returnForce = 150f;
    private readonly float _drag = 10f;

    public MouseDirectionalMover(Rigidbody rigidbody, float movementForce, float maxDistance)
    {
        _rigidbody = rigidbody;
        _transform = rigidbody.transform;
        _movementForce = movementForce;
        _maxDistance = maxDistance;
        _startPosition = rigidbody.position;

        _rigidbody.linearDamping = _drag;
    }

    public void Enable() { }

    public void SetMoveDirection(Vector3 inputDirection)
    {
        _inputDirection = new Vector3(0f, 0f, inputDirection.z).normalized;
    }

    public void Update(float deltaTime)
    {
        Vector3 toTarget = _inputDirection * _movementForce;
        _rigidbody.AddForce(toTarget, ForceMode.Acceleration);

        Vector3 offset = _rigidbody.position - _startPosition;
        if (offset.magnitude > _maxDistance)
        {
            Vector3 correction = (_startPosition + offset.normalized * _maxDistance) - _rigidbody.position;
            _rigidbody.AddForce(correction * _returnForce, ForceMode.Acceleration);
        }
    }
}
