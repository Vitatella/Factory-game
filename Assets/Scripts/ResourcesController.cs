using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    [SerializeField] private Factory _resourcesFactory;
    [SerializeField] private ResourcesMover _resourceMover;
    public static ResourcesController Instance { get; private set; }
    private List<Item> _resources = new List<Item>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void AddResource(Item resource)
    {
        _resources.Add(resource);
    }

    public Item CreateResource(ItemType type, Vector2 position)
    {
        Item resource = _resourcesFactory.GetResource(position, type);
        AddResource(resource);
        return resource;
    }

    public void SetResourceMoving(Item resource, Vector2 startPosition, Vector2 finishPosition)
    {
        _resourceMover.AddResource(resource, startPosition, finishPosition);
    }

    public void DestroyResource(Item resource)
    {
        _resources.Remove(resource);
        resource.ReturnToPool();
    }
}
