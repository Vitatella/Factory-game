using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingsDeleter : MonoBehaviour, IPlayerMode
{
    [SerializeField] private BuildingSelection _selection;
    [SerializeField] private GameObject _deleteCursor;
    [SerializeField] private Ground _ground;
    [SerializeField] private BuildingPlayerMode _buildingMode;
    [SerializeField] private GameObject _buttonSelected;
    private Vector2 _currentPosition;
    private bool _isMouseOverGUI;

    private bool _allowDelete;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Enter()
    {
        _buttonSelected.SetActive(true);
    }

    public void Exit()
    {
        _buttonSelected.SetActive(false);
        _deleteCursor.SetActive(false);
    }

    public void HandleLMBAction(bool value)
    {
        _allowDelete = value;
    }

    public void RMBClicked()
    {
        _buildingMode.EnableBuildingMode();
    }

    public void UpdatePlayerMode()
    {
        _isMouseOverGUI = EventSystem.current.IsPointerOverGameObject();
        if (_isMouseOverGUI == false)
        {
            _deleteCursor.SetActive(true);
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 roundPosition = new Vector2(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y));
            _deleteCursor.transform.position = roundPosition;
            _currentPosition = roundPosition;
            if (_allowDelete) TryDeleteObject();
        }
        else
        {
            _deleteCursor.SetActive(false);
        }
    }

    private void TryDeleteObject()
    {
        var tile = _ground.GetTile((int)_currentPosition.x, (int)_currentPosition.y);
        if (tile.Building != null && tile.Building is not PlayerBase)
        {
            tile.Building.Destroy();
            tile.Building = null;
        }
    }
}
