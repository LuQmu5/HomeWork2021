using UnityEngine;

public class PlayerMouseScrollRotateController : Controller
{
    private IRotatable _rotatable;

    public PlayerMouseScrollRotateController(IRotatable rotatable)
    {
        _rotatable = rotatable;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        float scrollInput = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            _rotatable.Rotate(scrollInput, deltaTime);
        }
    }
}

