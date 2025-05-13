using System;
using System.Linq.Expressions;
using UnityEngine;

public class GodHandView : MonoBehaviour
{
    private const string GrabModeAnimationName = "GrabMode";
    private const string ShootModeAnimationName = "ShootMode";
    private const string ShootAnimationName = "Shoot";
    private const string ShootFailedAnimationName = "ShootFailed";
    private const string IdleAnimationName = "Idle";

    [SerializeField] private Animator[] _grabberAnimators;
    [SerializeField] private Animator _handAnimator;

    private void Awake()
    {
        SwitchShootMode(false, GrabModeAnimationName);
    }

    public void PlayShootAnimation()
    {
        _handAnimator.Play(ShootAnimationName);
        SwitchShootMode(true, ShootModeAnimationName);
    }

    public void PlayShootFailedAnimation()
    {
        _handAnimator.Play(ShootFailedAnimationName);
        SwitchShootMode(true, ShootModeAnimationName);
    }

    public void CompleteShoot()
    {
        _handAnimator.Play(IdleAnimationName);
        SwitchShootMode(false, GrabModeAnimationName);
    }

    private void SwitchShootMode(bool state, string animationName)
    {
        foreach (Animator animator in _grabberAnimators)
        {
            animator.enabled = state;
            animator.Play(animationName);
        }
    }
}
