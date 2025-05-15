using System;
using System.Collections;
using UnityEngine;

public class GodHandShooter : MonoBehaviour, IShooter
{
    [SerializeField] private GodHandView _view;
    [SerializeField] private int _energyCost = 1;
    [SerializeField] private float _shootCooldown = 1;
    [SerializeField] private int _currentEnergy;
    [SerializeField] private Transform _laserDot;

    private Coroutine _shootReloadingCoroutine;

    private void OnEnable()
    {
        _view.OnShootEffectComplete += OnShootCompleted;
        _view.OnShootFailedEffectComplete += OnShootFailed;
    }

    private void OnDisable()
    {
        _view.OnShootEffectComplete -= OnShootCompleted;
        _view.OnShootFailedEffectComplete -= OnShootFailed;
    }

    private void OnShootFailed()
    {
        Debug.Log("Shoot failed");
    }

    private void OnShootCompleted()
    {
        Debug.Log("Shoot completed");

        Vector3 explosionPosition = _laserDot.position;
        Explosion explosion = _view.CreateExplosion(explosionPosition);
        explosion.Explode(explosionPosition);
    }

    public void TryShoot()
    {
        if (_shootReloadingCoroutine != null) 
            return;
        
        if (_currentEnergy < _energyCost)
        {
            float cooldownReduce = 4;
            _shootReloadingCoroutine = StartCoroutine(ShootReloading(_shootCooldown / cooldownReduce));
            _view.PlayShootFailedAnimation();
        }
        else
        {
            _currentEnergy -= _energyCost;
            _shootReloadingCoroutine = StartCoroutine(ShootReloading(_shootCooldown));
            _view.PlayShootAnimation();
        }
    }

    private IEnumerator ShootReloading(float delay)
    {
        yield return new WaitForSeconds(delay);

        _shootReloadingCoroutine = null;
    }
}
