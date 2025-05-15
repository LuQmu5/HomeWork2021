using DG.Tweening;
using System;
using System.Linq.Expressions;
using UnityEngine;

public class GodHandView : MonoBehaviour
{
    private const string GrabModeAnimationName = "GrabMode";
    private const string ShootModeAnimationName = "ShootMode";

    [Header("Grabber")]
    [SerializeField] private Animator[] _grabberAnimators;

    [Header("Recoil")]
    [SerializeField] private Transform _recoilTarget;
    [SerializeField] private float _preparationLift = 0.05f;
    [SerializeField] private float _recoilLift = 0.2f;
    [SerializeField] private float _preparationDuration = 0.15f;
    [SerializeField] private float _recoilDuration = 0.1f;
    [SerializeField] private float _returnDuration = 0.15f;
    [SerializeField] private float _betweenPreparationAndRecoilDelay = 0.05f;

    [Header("prepare VFX")]
    [SerializeField] private ParticleSystem[] _chargeExplosionVFX;
    [SerializeField] private ParticleSystem _explosionAreaVFX;
    [SerializeField] private ParticleSystem[] _explosionBeamVFX;

    [Header("Explosion")]
    [SerializeField] private ParticleSystem _explosionVFXPrefab;

    private Vector3 _initialLocalPosition;

    public event Action OnShootEffectComplete;
    public event Action OnShootFailedEffectComplete;

    private void Awake()
    {
        _initialLocalPosition = _recoilTarget.localPosition;
        PlayGrabberAnimation(GrabModeAnimationName);
    }

    public Explosion CreateExplosion(Vector3 position)
    {
        ParticleSystem explosion = Instantiate(_explosionVFXPrefab, position, Quaternion.identity);
        Destroy(explosion.gameObject, explosion.main.duration);

        return explosion.GetComponent<Explosion>();
    }

    public void PlayShootAnimation()
    {
        PlayGrabberAnimation(ShootModeAnimationName);

        foreach (var vfx in _chargeExplosionVFX)
        {
            vfx.Play();
        }

        Recoil();
    }

    public void PlayShootFailedAnimation()
    {
        foreach (var vfx in _chargeExplosionVFX)
        {
            vfx.Play();
        }

        RecoilFailed();
    }

    private void Recoil()
    {
        _recoilTarget.DOKill();
        _recoilTarget.localPosition = _initialLocalPosition;

        Vector3 preparationPosition = _initialLocalPosition + new Vector3(0f, _preparationLift, 0f);
        Vector3 recoilPosition = _initialLocalPosition + new Vector3(0f, _recoilLift, 0f);

        Sequence recoilSequence = DOTween.Sequence();

        recoilSequence.Append(
            _recoilTarget.DOLocalMove(preparationPosition, _preparationDuration)
                .SetEase(Ease.InOutSine).OnComplete(PlayChargeVFX)
        );

        recoilSequence.AppendInterval(_betweenPreparationAndRecoilDelay);

        recoilSequence.Append(
            _recoilTarget.DOLocalMove(recoilPosition, _recoilDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(OnShootCompleted)
        );

        recoilSequence.Append(
            _recoilTarget.DOLocalMove(_initialLocalPosition, _returnDuration)
                .SetEase(Ease.InOutQuad)
        );
    }

    private void PlayChargeVFX()
    {
        _explosionAreaVFX.Play();

        foreach (var vfx in _explosionBeamVFX)
        {
            vfx.Play();
        }
    }

    private void OnShootCompleted()
    {
        _explosionAreaVFX.Stop();
        OnShootEffectComplete?.Invoke();
        PlayGrabberAnimation(GrabModeAnimationName);
    }

    private void RecoilFailed()
    {
        _recoilTarget.DOKill(); 
        _recoilTarget.localPosition = _initialLocalPosition; 

        _recoilTarget.DOShakePosition(0.2f, strength: 0.04f, vibrato: 6)
            .OnComplete(() => OnShootFailed());
    }

    private void OnShootFailed()
    {
        OnShootFailedEffectComplete?.Invoke();
        PlayGrabberAnimation(GrabModeAnimationName);
    }

    private void PlayGrabberAnimation(string animationName)
    {
        foreach (Animator animator in _grabberAnimators)
        {
            animator.Play(animationName);
        }
    }
}
