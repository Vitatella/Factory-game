using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ItemsCostUI : MonoBehaviour
{
    [SerializeField] private ItemCostUI _itemCostPrefab;
    [SerializeField] private ItemType[] _displayableItems;
    [SerializeField] private PlayerStorage _storage;
    private IReadOnlyDictionary<ItemType, int> _cost;

    private Dictionary<ItemType, ItemCostUI> _itemCosts = new Dictionary<ItemType, ItemCostUI>();


    private void Start()
    {
        foreach (var item in _displayableItems)
        {
            ItemCostUI cost = Instantiate(_itemCostPrefab,transform);
            cost.SetSprite(item.Sprite);
            _itemCosts.Add(item, cost);
            cost.gameObject.SetActive(false);
        }
        _storage.StorageUpdated.AddListener(UpdateItemsCount);
    }

    public void ClearSelection()
    {
        _cost = null;
        foreach (var item in _itemCosts.Values)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void ShowItems(IReadOnlyDictionary<ItemType, int> cost)
    {
        _cost = cost;
        foreach (var itemType in _itemCosts.Keys)
        {
            if (_cost.ContainsKey(itemType))
            {
                _itemCosts[itemType].gameObject.SetActive(true);
                string color = _storage.IsItemEnough(itemType, _cost[itemType]) ? "<color=#00FF00>" : "<color=#FF0000>";
                string text = $"{color}{_storage.GetItemCount(itemType)} / {_cost[itemType]}";
                _itemCosts[itemType].SetText(text);
            }
            else
            {
                _itemCosts[itemType].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateItemsCount()
    {
        if (_cost == null) return;
        foreach (var itemType in _cost.Keys)
        {
            _itemCosts[itemType].gameObject.SetActive(true);
            string color = _storage.IsItemEnough(itemType, _cost[itemType]) ? "<color=#00FF00>" : "<color=#FF0000>";
            string text = $"{color}{_storage.GetItemCount(itemType)} / {_cost[itemType]}";
            _itemCosts[itemType].SetText(text);
        }
    }
}
