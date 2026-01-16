using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Router : Building, IAcceptResource, IResourceInput, IUpdateable
{
    private ResourcesSender _sender;
    private ItemType _itemInStorage;
    private bool _isItemMovingToStorage;
    private List<Building> _buildingsToSend = new List<Building>();
    private List<Item> _movingItems = new List<Item>();


    public override void Initialize()
    {
        base.Initialize();
        _sender = new ResourcesSender(Position);
        UpdateBuildingsToSend();
        UpdateNeighbours();
    }


    public void AcceptResource(Item resource)
    {
        _movingItems.Remove(resource);
        _isItemMovingToStorage = false;
        if (_sender.TrySend(_buildingsToSend, resource.Type) == false)
        {
            _itemInStorage = resource.Type;
        }
        else
        {
            UpdateNeighbours();
        }
        ResourcesController.Instance.DestroyResource(resource);
    }

    private void UpdateBuildingsToSend()
    {
        _buildingsToSend.Clear();
        foreach (var position in NearPositions)
        {
            if (Ground.IsTileExist(position.x, position.y) == false) continue;

            var checkTile = Ground.GetTile(position.x, position.y);
            var checkBuilding = checkTile.Building;
            if (checkBuilding != null)
            {
                _buildingsToSend.Add(checkBuilding);
            }
        }
    }

    public bool TryDeliverResource(ItemType resource, Vector2Int from)
    {
        if (_itemInStorage == null && _isItemMovingToStorage == false)
        {
            _isItemMovingToStorage = true;  
            return true;
        }
        return false;
    }

    public void UpdateObject()
    {
        UpdateBuildingsToSend();
        if (_itemInStorage != null)
        {
            if (_sender.TrySend(_buildingsToSend, _itemInStorage) == true)
            {
                _itemInStorage = null;
                UpdateNeighbours();
            }
        }
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
