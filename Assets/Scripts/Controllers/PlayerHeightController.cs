using UnityEngine;

public class PlayerHeightController : Controller
{
    private IHeightAdjustable _adjustable;
    private KeyCode _upKey;
    private KeyCode _downKey;

    private readonly float _upSpeed;
    private readonly float _downSpeed;
    private readonly float _maxY;
    private readonly float _minY;

    public PlayerHeightController(IHeightAdjustable adjustable, float minY, float maxY, float upSpeed, float downSpeed, KeyCode upKey, KeyCode downKey)
    {
        _adjustable = adjustable;
        _minY = minY;
        _maxY = maxY;
        _upSpeed = upSpeed;
        _downSpeed = downSpeed;
        _upKey = upKey;
        _downKey = downKey;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetKeyDown(_upKey))
        {
            _adjustable.MoveToHeight(_maxY, _upSpeed);
        }

        if (Input.GetKeyDown(_downKey))
        {
            _adjustable.MoveToHeight(_minY, _downSpeed);
        }

        _adjustable.Tick(deltaTime);
    }
}
