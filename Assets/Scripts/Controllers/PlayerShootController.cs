using UnityEngine;

public class PlayerShootController : Controller
{
    private readonly IShooter _shooter;

    public PlayerShootController(IShooter shooter)
    {
        _shooter = shooter;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetMouseButtonDown(1))
        {
            _shooter.TryShoot();
        }
    }
}
