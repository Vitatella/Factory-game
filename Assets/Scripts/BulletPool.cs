using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private bool _collectionCheck;
    public Bullet Prefab => _bulletPrefab;


    private IObjectPool<Bullet> _pool;

    [SerializeField] private int _defaultCapacity = 500;
    [SerializeField] private int _maxCapacity = 2000;


    void Start()
    {
        _pool = new ObjectPool<Bullet>(CreateResource, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            _collectionCheck, _defaultCapacity, _maxCapacity);
    }

    private Bullet CreateResource()
    {
        Bullet resource = Instantiate(_bulletPrefab, gameObject.transform);
        resource.SetPool(_pool);
        return resource;
    }

    public void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    public void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    public void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public Bullet GetPooledObject()
    {
        return _pool.Get();
    }
}
