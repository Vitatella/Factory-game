using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesContainer
{
    private Dictionary<ItemType, int> _resources = new Dictionary<ItemType, int>();

    public void AddResource(ItemType type)
    {
        if (_resources.ContainsKey(type))
        {
            _resources[type] = 1;
        }
        else
        {
            _resources.Add(type, 1);
        }
        
    }
}
