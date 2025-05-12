using System;
using UnityEngine;

public class MouseDirectionalMover : Controller
{
    private Transform _transform;
    private float _movementSpeed;
    private Vector3 _direction;
    private float _maxDistance;
    private Vector3 _startLocalPosition;

    public MouseDirectionalMover(Transform transform, float movementSpeed, float maxDistance)
    {
        _startLocalPosition = transform.localPosition;
        _transform = transform;
        _movementSpeed = movementSpeed;
        _maxDistance = maxDistance;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetInputDirection(Vector3 inputDirection)
    {
        _direction = inputDirection;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 offset = new Vector3(_direction.x, 0, _direction.y) * _movementSpeed * deltaTime;
        Vector3 targetPos = _transform.localPosition + offset;
        Vector3 clampedPos = _startLocalPosition + Vector3.ClampMagnitude(targetPos - _startLocalPosition, _maxDistance);

        _transform.localPosition = new Vector3(clampedPos.x, _transform.localPosition.y, clampedPos.z);
    }
}
