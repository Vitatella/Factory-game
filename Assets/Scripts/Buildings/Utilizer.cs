using NUnit.Framework;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Utilizer : Building, IAcceptResource, IResourceInput
{
    private List<Item> _movingItems = new List<Item>();
    public override void Initialize()
    {
        base.Initialize();
        UpdateNeighbours();
    }

    public void AcceptResource(Item resource)
    {
        ResourcesController.Instance.DestroyResource(resource);
    }

    public bool TryDeliverResource(ItemType resource, Vector2Int from)
    {
        return true;
    }


    public void SetMovingItem(Item item)
    {
        _movingItems.Add(item);
    }

    public override void Destroy()
    {
        foreach (var item in _movingItems)
        {
            ResourcesController.Instance.DestroyResource(item);
        }
        Destroy(gameObject);
    }
}
