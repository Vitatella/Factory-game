using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drill : Building, IUpdateable
{
    [SerializeField] private float _extractionRateMin, _extractionrateMax;
    [SerializeField] private SerializedDictionary<TileType, ItemType> _tileItem;
    
    private Coroutine _extractionRoutine;
    private bool _isExtracted;
    private List<Building> _buildingsToSend = new List<Building>();
    private int _selectedIndex;
    private ResourcesSender _sender;
    private bool _isExtractionAvailable = false;
    private ItemType _resourceType;

    public UnityEvent DrillingStarted, DrillingFinished;

    public override void Initialize()
    {
        base.Initialize();
        _sender = new ResourcesSender(Position);
        TileType type = Ground.GetTile(Position.x, Position.y).Type;
        if (type != TileType.Nothing && _tileItem.ContainsKey(type))
        {
            UpdateBuildingsToSend();
            _resourceType = _tileItem[type];
            _isExtractionAvailable = _resourceType != null;
            UpdateNeighbours();
            if (_isExtractionAvailable)
                _extractionRoutine = StartCoroutine(Extraction());
        }
        
    }

    public void UpdateObject()
    {
        if (_isExtractionAvailable == false) return;
            UpdateBuildingsToSend();
        if (_isExtracted)
        {
            SendResource();
        }
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

    private IEnumerator Extraction()
    {
        DrillingStarted?.Invoke();  
        yield return new WaitForSeconds(Random.Range(_extractionRateMin, _extractionrateMax));
        _isExtracted = true;
        DrillingFinished?.Invoke();
        SendResource();
    }

    private void SendResource()
    {
        if (_sender.TrySend(_buildingsToSend, _resourceType))
        {
            _extractionRoutine = StartCoroutine(Extraction());
            _isExtracted = false;
        }
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}

