using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ItemType, int> _cost = new SerializedDictionary<ItemType, int>();
    [SerializeField] private BuildingStats[] _openBuildings;
    public Dictionary<ItemType, int> Cost => _cost;
    public UnityEvent UpgradeApplied;


    public void Apply()
    {
        foreach (var building in _openBuildings)
        {
            building.IsOpened = true;
        }
        UpgradeApplied?.Invoke();
    }

}
