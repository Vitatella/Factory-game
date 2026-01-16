using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class UnitsSpawner : Building, IAcceptResource, IResourceInput
{
    [SerializeField] private SerializedDictionary<ItemType, int> _assemblyRequirements = new SerializedDictionary<ItemType, int>();
    [SerializeField] private ShootingUnit _unitPrefab;
    [SerializeField] private float _duration;

    private Dictionary<ItemType, int> _assemblerStorage = new Dictionary<ItemType, int>();
    private Dictionary<ItemType, int> _sentResources = new Dictionary<ItemType, int>();
    private bool _isItemAssembled;
    private Coroutine _assembleRoutine;
    private List<Item> _movingItems = new List<Item>();

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
        UpdateNeighbours();
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
        SpawnUnit();
        _isItemAssembled = false;
        _isAssebling = false;
        UpdateNeighbours();

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

    public void SpawnUnit()
    {
        var unit = Instantiate(_unitPrefab);
        unit.transform.position = transform.position;
        unit.InitializeAgent(transform.position);
        unit.MoveTo((Vector2)transform.position + Random.insideUnitCircle * 6);
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
