using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class Assembler : Building, IUpdateable, IAcceptResource, IResourceInput
{
    [SerializeField] private SerializedDictionary<ItemType, int> _assemblyRequirements = new SerializedDictionary<ItemType, int>();
    [SerializeField] private ItemType _assembleableItem;
    [SerializeField] private float _duration;

    private Dictionary<ItemType, int> _assemblerStorage = new Dictionary<ItemType, int>();
    private Dictionary<ItemType, int> _sentResources = new Dictionary<ItemType, int>();
    private ResourcesSender _sender;
    private bool _isItemAssembled;
    private Coroutine _assembleRoutine;
    private List<Item> _movingItems = new List<Item>();

    private List<Building> _buildingsToSend = new List<Building>();
    private bool _isAssebling;

    public UnityEvent AssemblingStarted, AssemblingFinished;

    public override void Initialize()
    {
        base.Initialize();
        foreach (var key in _assemblyRequirements.Keys)
        {
            _assemblerStorage.Add(key, 0);
            _sentResources.Add(key, 0);
        }
        _sender = new ResourcesSender(Position);
        UpdateBuildingsToSend();
        UpdateNeighbours();
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

    public void AcceptResource(Item resource)
    {
        _assemblerStorage[resource.Type]++;
        _movingItems.Remove(resource);
        Debug.Log(_assemblerStorage[resource.Type]);
        ResourcesController.Instance.DestroyResource(resource);
        if (_isAssebling == false && _isItemAssembled == false)
        {
            if (CanAssemble())
            {
                Assemble();
            }
        }
    }

    private bool CanAssemble()
    {
        foreach (var key in _assemblyRequirements.Keys)
        {
            if (_assemblyRequirements[key] != _assemblerStorage[key])
                return false;
        }
        return true;
    }

    private void Assemble()
    {
        _assembleRoutine = StartCoroutine(AssembleRoutine());
        _isAssebling = true;
    }


    private IEnumerator AssembleRoutine()
    {
        AssemblingStarted?.Invoke();    
        yield return new WaitForSeconds(_duration);
        AssemblingFinished?.Invoke();
        _isItemAssembled = true;
        foreach (var key in _assemblerStorage.Keys.ToList())
        {
            _assemblerStorage[key] = 0;
            _sentResources[key] = 0;
        }
        _isAssebling = false;
        if (_sender.TrySend(_buildingsToSend, _assembleableItem) == true)
        {
            _isItemAssembled = false;
            _isAssebling = false;
            UpdateNeighbours();
        }
        
    }

    public bool TryDeliverResource(ItemType resource, Vector2Int from)
    {
        if (_isAssebling == true || _isItemAssembled == true) return false;

        if (_assemblyRequirements.ContainsKey(resource) && _assemblyRequirements[resource] > _sentResources[resource])
        {
            _sentResources[resource]++;
            return true;
        }
        return false;
    }

    public void UpdateObject()
    {
        UpdateBuildingsToSend();
        if (_isItemAssembled)
        {
            if (_sender.TrySend(_buildingsToSend, _assembleableItem) == true)
            {
                _isItemAssembled = false;
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
