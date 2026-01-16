using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingCreator : MonoBehaviour, IPlayerMode
{
    [SerializeField] private Ground _ground;
    [SerializeField] private PlayerStorage _storage;
    private BuildingStats _stats;
    private Building _building;
    private Camera _camera;
    public bool IsCreatingObjects { get; private set; }
    private Vector2 _currentPosition;
    private bool _isMouseOverGUI;

    private float _angle;
    private bool _allowBuilding;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void CreateBuilding()
    {
        _building = Instantiate(_stats.Prefab);
        _building.transform.rotation = Quaternion.Euler(0, 0, _angle);
        IsCreatingObjects = true;
    }

    

    public void Select(BuildingStats stats)
    {
        if (_stats != null)
            ClearSelection();
        _stats = stats;
    }

    public void ClearSelection()
    {
        if (_building != null)
            Destroy(_building.gameObject);
        IsCreatingObjects = false;
        _stats = null;
    }

    public void Rotate(float value)
    {
        _angle -= value * 90;
        _building.transform.rotation = Quaternion.Euler(0, 0, _angle);
    }

    public void TryPlaceObject()
    {
        if (_isMouseOverGUI) return;
        var tile = _ground.GetTile((int)_currentPosition.x, (int)_currentPosition.y);
        if (tile.Type == TileType.Mountain) return;
        if (tile.Building == null)
        {
            if (tile.Type == TileType.Water) return;
            if (_storage.IsItemsEnough(_building.Stats.Cost))
            {
                _ground.GetTile((int)_currentPosition.x, (int)_currentPosition.y).Building = _building;
                _building.Initialize();
                _storage.RemoveItems(_building.Stats.Cost);
                _building = null;
            }
        }
        else if (tile.Building.Stats == _building.Stats)
        {
            _ground.GetTile((int)_currentPosition.x, (int)_currentPosition.y).Building.Rotate(_angle);
        }
    }

    public void AllowBuilding(bool value)
    {
        _allowBuilding = value;
    }

    public void HandleLMBAction(bool value)
    {
        AllowBuilding(value);
    }

    void IPlayerMode.UpdatePlayerMode()
    {
        if (_stats == null) return;
        _isMouseOverGUI = EventSystem.current.IsPointerOverGameObject();
        if (_isMouseOverGUI == false)
        {
            if (_building == null)
            {
                CreateBuilding();
            }
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 roundPosition = new Vector2(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));
            _building.transform.position = roundPosition;
            _currentPosition = roundPosition;
            if (_allowBuilding) TryPlaceObject();
        }
    }

    public void Exit()
    {
        ClearSelection();
    }

    public void RMBClicked()
    {
        if (IsCreatingObjects)
        {
            ClearSelection();
        }
    }

    public void Enter()
    {
        
    }
}
