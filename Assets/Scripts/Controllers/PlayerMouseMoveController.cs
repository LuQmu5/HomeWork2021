using UnityEngine;

public class PlayerMouseMoveController : Controller
{
    private const string VerticalMouseAxis = "Mouse Y";
    private const string HorizontalMouseAxis = "Mouse X";

    private IMouseMovable _movable;
    private float _moveSensitivity;

    public PlayerMouseMoveController(IMouseMovable movable, float moveSensitivity)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");
        return new Vector3(moveX, moveY, 0f);
    }
}
