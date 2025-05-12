using UnityEngine;

public class GodHandGrabberRotator : MonoBehaviour, IRotatable
{
    [SerializeField] private Transform[] _rotatableTargets;
    [SerializeField] private float _openAngleX = 30f;
    [SerializeField] private float _closeAngleX = -15f;
    [SerializeField] private float _openSpeed = 90f;
    [SerializeField] private float _closeSpeed = 150f;

    private float _currentTargetAngle;

    private void Awake()
    {
        if (_rotatableTargets.Length > 0)
            _currentTargetAngle = NormalizeAngle(_rotatableTargets[0].localEulerAngles.x);
    }

    public void Rotate(float scrollDelta, float deltaTime)
    {
        float angle = NormalizeAngle(_rotatableTargets[0].localEulerAngles.x);

        if (scrollDelta < 0f)
        {
            _currentTargetAngle = _openAngleX;
            angle = Mathf.MoveTowards(angle, _openAngleX, _openSpeed * deltaTime);
        }
        else if (scrollDelta > 0f)
        {
            _currentTargetAngle = _closeAngleX;
            angle = Mathf.MoveTowards(angle, _closeAngleX, _closeSpeed * deltaTime);
        }

        foreach (Transform t in _rotatableTargets)
        {
            Vector3 currentEuler = t.localEulerAngles;
            currentEuler.x = angle;
            t.localEulerAngles = currentEuler;
        }
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}
