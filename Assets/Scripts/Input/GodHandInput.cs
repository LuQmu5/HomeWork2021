using UnityEngine;

public class GodHandInput : MonoBehaviour
{
    [SerializeField] private GodHandMover _mover;
    [SerializeField] private GodHandGrabberRotator _grabberRotator;
    [SerializeField] private GodHandHeightAdjuster _heightAdjuster;
    [SerializeField] private GodHandMagnete _magnete;
    [SerializeField] private GodHandShooter _shooter;

    private Controller _playerController;

    private void Awake()
    {
        _playerController = new CompositeController(
            new PlayerMouseMoveController(_mover, moveSensitivity: _mover.MovementSpeed), 
            new PlayerMouseScrollRotateController(_grabberRotator),
            new PlayerHeightController(_heightAdjuster, _heightAdjuster.Target,
            _heightAdjuster.MoveUpSpeed, _heightAdjuster.MoveDownSpeed,
            KeyCode.LeftShift, KeyCode.LeftControl,
            _heightAdjuster.MinY, _heightAdjuster.MaxY),
            new MagnetController(_magnete),
            new PlayerShootController(_shooter)
            );

        _playerController.Enable();
    }

    private void Update()
    {
        _playerController.Update(Time.deltaTime);
    }
}
