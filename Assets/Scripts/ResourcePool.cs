using UnityEngine;
using UnityEngine.Pool;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Item _resourcePrefab;
    [SerializeField] private bool _collectionCheck;

    private IObjectPool<Item> _pool;

    [SerializeField] private int _defaultCapacity = 500;
    [SerializeField] private int _maxCapacity = 2000;


    void Start()
    {
        _pool = new ObjectPool<Item>(CreateResource, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            _collectionCheck, _defaultCapacity, _maxCapacity);
    }

    private Item CreateResource()
    {
        Item resource = Instantiate(_resourcePrefab, gameObject.transform);
        resource.SetPool(_pool);
        return resource;
    }

    public void OnReleaseToPool(Item pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    public void OnGetFromPool(Item pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    public void OnDestroyPooledObject(Item pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public Item GetPooledObject()
    {
        return _pool.Get();
    }
}
