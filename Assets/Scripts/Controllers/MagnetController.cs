using UnityEngine;

public class MagnetController : Controller
{
    private readonly IMagnetic _magnet;

    public MagnetController(IMagnetic magnet)
    {
        _magnet = magnet;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        bool isHoldingLMB = Input.GetMouseButton(0);
        _magnet.SetMagnetActive(isHoldingLMB);
    }
}
