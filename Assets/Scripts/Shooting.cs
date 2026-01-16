using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float _damage, _bulletSpeed, _reloadingTime;
    [SerializeField] private Bullet _bulletPrefab;
    private Coroutine _reloadingRoutine;
    public bool IsLoaded { get;private set; }

    private void Start()
    {
        IsLoaded = true;
    }

    public void Shoot()
    {
        Debug.Log(transform.eulerAngles.z);
        Debug.Log(transform.up);
        Bullet bullet = BulletPoolsController.Instance.GetPooledObject(_bulletPrefab);
        bullet.Launch(transform.position, transform.up, _bulletSpeed, _damage);
        IsLoaded = false;
        _reloadingRoutine = StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        yield return new WaitForSeconds(_reloadingTime);
        IsLoaded = true;
    }

    public void ForceReload()
    {
        IsLoaded = true;
    }
}
