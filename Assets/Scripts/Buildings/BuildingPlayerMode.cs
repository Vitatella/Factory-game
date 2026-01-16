using UnityEngine;

public class BuildingPlayerMode : MonoBehaviour, IPlayerMode
{
    [SerializeField] private BuildingSelection _buidlingSelection;
    [SerializeField] private BuildingCreator _creator;
    [SerializeField] private BuildingsDeleter _deleter;
    [SerializeField] private GameObject _buildPanel;
    private IPlayerMode _playerMode;

    public void TransitionTo(IPlayerMode playerMode)
    {
        _playerMode.Exit();
        _playerMode = playerMode;
        _playerMode.Enter();
    }

    public void SwitchDeleteMode()
    {
        if (_playerMode is BuildingsDeleter)
        {
            TransitionTo(_creator);
        }
        else
        {
            TransitionTo(_deleter);
        }
    }

    public void EnableBuildingMode()
    {
        TransitionTo(_creator);
    }

    public void EnableDeleteMode()
    {
        TransitionTo(_creator);
    }

    public void Exit()
    {
        _buildPanel.SetActive(false);
    }

    public void HandleLMBAction(bool value)
    {
        _playerMode.HandleLMBAction(value);
    }

    public void RMBClicked()
    {
        _playerMode.RMBClicked();
    }

    public void UpdatePlayerMode()
    {
        _playerMode.UpdatePlayerMode();
    }

    void Start()
    {
        _playerMode = _creator;
        _creator.Enter();
    }

    public void Enter()
    {
        _buildPanel.SetActive(true);
    }
}

public interface IPlayerMode
{
    public void HandleLMBAction(bool value);
    public void UpdatePlayerMode();
    public void RMBClicked();
    public void Exit();
    public void Enter();
}
