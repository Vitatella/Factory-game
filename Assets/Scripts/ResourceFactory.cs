using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ResourceFactory : Factory
{
    [SerializeField] private Item _resourcePrefab;
    [SerializeField] private ResourcePool _pool;
    public override Item GetResource(Vector2 position, ItemType type)
    {
        Item resource = _pool.GetPooledObject();
        resource.Initialize(position, type);
        return resource;
    }
}
