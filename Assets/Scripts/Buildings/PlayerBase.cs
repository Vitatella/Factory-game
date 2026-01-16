using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Building, IAcceptResource, IResourceInput
{
    private PlayerStorage _storage;
    private BaseDestroyHandler _destroyHandler;
    private List<Item> _movingItems = new List<Item>();

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        _storage = FindAnyObjectByType<PlayerStorage>();
        _destroyHandler = FindAnyObjectByType<BaseDestroyHandler>();
    }
    public void AcceptResource(Item item)
    {
        _storage.AddItem(item.Type);
        ResourcesController.Instance.DestroyResource(item);
    }

    public bool TryDeliverResource(ItemType resource, Vector2Int from)
    {
        return true;
    }

    public void OnDestroyed()
    {
        _destroyHandler.OnBaseDestroyed();
    }

    private void OnDestroy()
    {
        foreach (var item in _movingItems)
        {
            ResourcesController.Instance.DestroyResource(item);
        }
    }

    public void SetMovingItem(Item item)
    {
        _movingItems.Add(item);
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
