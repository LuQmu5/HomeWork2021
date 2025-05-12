using UnityEngine;

public class GodHandInput : MonoBehaviour
{
    [SerializeField] private GodHandMover _mover;
    [SerializeField] private GodHandGrabberRotator _grabberRotator;
    [SerializeField] private GodHandHeightAdjuster _heightAdjuster;

    private Controller _playerController;

    private void Awake()
    {
        _playerController = new CompositeController(
            new PlayerMouseMoveController(_mover, moveSensitivity: _mover.MovementSpeed), 
            new PlayerMouseScrollRotateController(_grabberRotator),
            new PlayerHeightController(_heightAdjuster, 
            _heightAdjuster.MinY, _heightAdjuster.MaxY, 
            _heightAdjuster.MoveUpSpeed, _heightAdjuster.MoveDownSpeed,
            KeyCode.LeftShift, KeyCode.LeftControl)
            );

        _playerController.Enable();
    }

    private void Update()
    {
        _playerController.Update(Time.deltaTime);
    }
}
