using UnityEngine;

public class PlayerHeightController : Controller
{
    private readonly IHeightAdjustable _adjustable;
    private readonly float _upSpeed;
    private readonly float _downSpeed;
    private readonly KeyCode _upKey;
    private readonly KeyCode _downKey;
    private readonly float _minY;
    private readonly float _maxY;
    private readonly Transform _target;

    public PlayerHeightController(
        IHeightAdjustable adjustable,
        Transform target,
        float upSpeed,
        float downSpeed,
        KeyCode upKey,
        KeyCode downKey,
        float minY,
        float maxY)
    {
        _adjustable = adjustable;
        _target = target;
        _upSpeed = upSpeed;
        _downSpeed = downSpeed;
        _upKey = upKey;
        _downKey = downKey;
        _minY = minY;
        _maxY = maxY;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        float direction = 0f;
        float speed = 0f;
        float currentY = _target.localPosition.y;

        if (Input.GetKey(_upKey) && currentY < _maxY)
        {
            direction = 1f;
            speed = _upSpeed;
        }
        else if (Input.GetKey(_downKey) && currentY > _minY)
        {
            direction = -1f;
            speed = _downSpeed;
        }

        if (direction != 0f)
            _adjustable.AdjustHeight(direction, speed, deltaTime);
    }
}
