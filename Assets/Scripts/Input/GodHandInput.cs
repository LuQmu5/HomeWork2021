using UnityEngine;

public class GodHandInput : MonoBehaviour
{
    [SerializeField] private GodHandMover _mover;
    [SerializeField] private GodHandGrabberRotator _grabberRotator;

    private Controller _playerController;

    private void Awake()
    {
        _playerController = new CompositeController(
            new PlayerMouseMoveController(_mover, _mover.MovementSpeed), 
            new PlayerMouseScrollRotateController(_grabberRotator)
            );

        _playerController.Enable();
    }

    private void Update()
    {
        _playerController.Update(Time.deltaTime);
    }
}
