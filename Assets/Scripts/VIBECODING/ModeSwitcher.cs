using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [SerializeField] private Animator[] animators;
    [SerializeField] private string destroyerAnimationName = "Destroyer";
    [SerializeField] private string destroyerOffAnimationName = "DestroyerOff";

    private bool isCollecting = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchMode();
        }
    }

    private void SwitchMode()
    {
        isCollecting = !isCollecting;

        foreach (var animator in animators)
        {
            if (animator == null) continue;

            string animationToPlay = isCollecting ? destroyerOffAnimationName : destroyerAnimationName;
            animator.speed = 1f;
            animator.Play(animationToPlay, 0, 0f);
        }
    }
}
