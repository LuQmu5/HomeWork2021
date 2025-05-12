using UnityEngine;

public class PlayerDirectionalMouseController : Controller
{
    private const string VerticalMouseAxis = "Mouse Y";
    private const string HorizontalMouseAxis = "Mouse X";

    private IMouseDirectionalMovable _movable;
    private float _moveSensitivity;

    public PlayerDirectionalMouseController(IMouseDirectionalMovable movable, float moveSensitivity)
    {
        _movable = movable;
        _moveSensitivity = moveSensitivity;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        Vector3 inputDirection = GetMouseDirection();
        _movable.SetMoveDirection(inputDirection);
    }

    private Vector3 GetMouseDirection()
    {
        float moveX = Input.GetAxis(HorizontalMouseAxis) * _moveSensitivity;
        float moveY = Input.GetAxis(VerticalMouseAxis) * _moveSensitivity;

        return new Vector3(moveX, moveY, 0f);
    }
}
