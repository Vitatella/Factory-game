using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TurretShooting : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed, _reloadingTime;
    [SerializeField] private Bullet _bulletPrefab;
    private Coroutine _reloadingRoutine;
    public UnityEvent Shot;
    public bool IsReloading { get; private set; }
    public bool IsLoaded { get; private set; }

    private void Start()
    {
        IsLoaded = false;
    }

    public void Shoot(float damage)
    {
        Bullet bullet = BulletPoolsController.Instance.GetPooledObject(_bulletPrefab);
        bullet.Launch(transform.position, transform.up, _bulletSpeed, damage);
        IsLoaded = false;
    }

    private IEnumerator Reloading()
    {
        IsReloading = true;
        yield return new WaitForSeconds(_reloadingTime);
        IsLoaded = true;
        IsReloading = false;
    }

    public void Reload()
    {
        _reloadingRoutine = StartCoroutine(Reloading());
    }
}
