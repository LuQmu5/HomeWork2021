using UnityEngine;

public class GodHandMover : MonoBehaviour, IMouseMovable
{
    [field: SerializeField] public float MovementSpeed;
    [field: SerializeField] public float MaxMoveDistance;

    private MouseDirectionalMover _mover;

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
        _mover.SetMoveDirection(inputDirection);
    }
}

