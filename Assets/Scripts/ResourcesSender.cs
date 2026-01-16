using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class ResourcesSender
{
    private int _selectedIndex;
    private Vector2Int _position;

    public ResourcesSender(Vector2Int position)
    {
        _position = position;
    }

    public bool TrySend(List<Building> buildingsToSend, ItemType type)
    {
        for (int i = 0; i < buildingsToSend.Count; i++)
        {
            var index = (_selectedIndex + 1) % buildingsToSend.Count;
            _selectedIndex = index;
            var checkBuilding = buildingsToSend[index];
            if (checkBuilding is Conveyor conveyor && conveyor.IsFree && conveyor.OutputPosition != _position)
            {
                var resource = ResourcesController.Instance.CreateResource(type, _position);
                resource.SetPreviousPoint(_position);
                ResourcesController.Instance.SetResourceMoving(resource, _position, checkBuilding.Position);
                (checkBuilding as Conveyor).Item = resource;
                
                return true;
            }
            else if (checkBuilding is IResourceInput resourceInput)
            {
                if (resourceInput.TryDeliverResource(type, _position))
                {
                    var resource = ResourcesController.Instance.CreateResource(type, _position);
                    resource.SetPreviousPoint(_position);
                    resourceInput.SetMovingItem(resource);
                    ResourcesController.Instance.SetResourceMoving(resource, _position, checkBuilding.Position);
                    return true;
                }
            }
        }
        return false;
    }
}
