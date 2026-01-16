using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolsController : Singleton<BulletPoolsController>
{
    [SerializeField] private BulletPool[] _pools;
    private Dictionary<Bullet, BulletPool> _poolsDictionary = new Dictionary<Bullet, BulletPool>();

    private void Start()
    {
        foreach (var pool in _pools)
        {
            _poolsDictionary.Add(pool.Prefab, pool);
        }
    }

    public Bullet GetPooledObject(Bullet bulletPrefab)
    {
        return _poolsDictionary[bulletPrefab].GetPooledObject();
    }
}
