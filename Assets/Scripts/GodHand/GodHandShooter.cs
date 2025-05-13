using System.Collections;
using UnityEngine;

public class GodHandShooter : MonoBehaviour, IShooter
{
    [SerializeField] private GodHandView _view;
    [SerializeField] private int _energyCost = 1;
    [SerializeField] private float _shootCooldown = 1;
    [SerializeField] private int _currentEnergy;

    private Coroutine _shootReloadingCoroutine;

    public void TryShoot()
    {
        if (_shootReloadingCoroutine != null) 
            return;
        
        if (_currentEnergy < _energyCost)
        {
            float cooldownReduce = 2;
            _shootReloadingCoroutine = StartCoroutine(ShootReloading(_shootCooldown / cooldownReduce));
            _view.PlayShootFailedAnimation();
        }
        else
        {
            _shootReloadingCoroutine = StartCoroutine(ShootReloading(_shootCooldown));
            _view.PlayShootAnimation();
        }
    }

    private IEnumerator ShootReloading(float delay)
    {
        yield return new WaitForSeconds(delay);

        _shootReloadingCoroutine = null;
    }

    public void OnShootAnimationCompleted()
    {
        _view.CompleteShoot();
    }

    public void CreateShoot()
    {
        _currentEnergy -= _energyCost;
        Debug.Log("выстрел");
    }
}