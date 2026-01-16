using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ItemType, int> _storage = new SerializedDictionary<ItemType, int>();
    public UnityEvent StorageUpdated;

    private void Start()
    {
        StorageUpdated?.Invoke();
    }

    public void AddItem(ItemType type)
    {
        _storage[type]++;
        StorageUpdated?.Invoke();
    }

    public void RemoveItems(ItemType type, int count)
    {
        _storage[type]-=count;
        StorageUpdated?.Invoke();
    }

    public void RemoveItems(IReadOnlyDictionary<ItemType, int> cost)
    {
        foreach (var key in cost.Keys)
        {
            _storage[key] -= cost[key];
        }
        StorageUpdated?.Invoke();
    }

    public bool IsItemsEnough(IReadOnlyDictionary<ItemType, int> cost)
    {
        foreach (var key in cost.Keys)
        {
            if (_storage[key] < cost[key])
                return false;
        }
        return true;
    }

    public bool IsItemEnough(ItemType type, int cost)
    {
        return _storage[type] >= cost;
    }

    public int GetItemCount(ItemType type)
    {
        return _storage[type];
    }
}
