using UnityEngine;

public class GodHandMover : MonoBehaviour, IMouseMovable
{
    private MouseDirectionalMover _mover;

    [field: SerializeField] public float MovementSpeed;
    [field: SerializeField] public float MaxMoveDistance;

    private void Awake()
    {
        _mover = new MouseDirectionalMover(transform, MovementSpeed, MaxMoveDistance);
        _mover.Enable();
    }

    private void Update()
    {
        _mover.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection)
    {
        _mover.SetInputDirection(inputDirection);
    }
}

