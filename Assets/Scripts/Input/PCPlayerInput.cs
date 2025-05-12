using UnityEngine;

public class PCPlayerInput : MonoBehaviour
{
    [SerializeField] private GodHand _godHand;

    private Controller _godHandController;

    private void Awake()
    {
        _godHandController = new PlayerDirectionalMouseController(_godHand, _godHand.MovementSpeed);

        _godHandController.Enable();
    }

    private void Update()
    {
        _godHandController.Update(Time.deltaTime);
    }
}
