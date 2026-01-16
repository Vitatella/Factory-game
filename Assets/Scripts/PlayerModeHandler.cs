using UnityEngine;

public class PlayerModeHandler : MonoBehaviour
{
    [SerializeField] private BuildingPlayerMode _buildingMode;
    [SerializeField] private ShootingMode _shootingMode;

    private IPlayerMode _currentPlayerMode;

    private void TransitionTo(IPlayerMode playerMode)
    {
        _currentPlayerMode.Exit();
        _currentPlayerMode = playerMode;
        _currentPlayerMode.Enter();
    }

    public void SwitchPlayerMode()
    {
        if (_currentPlayerMode is BuildingPlayerMode)
            TransitionTo(_shootingMode);
        else
            TransitionTo(_buildingMode);
    }

    public void BuildMode()
    {
        TransitionTo(_buildingMode);
    }
    
    private void Start()
    {
        _currentPlayerMode = _buildingMode;
        _buildingMode.Enter();
    }

    public void HandleLMBPress(bool value)
    {
        _currentPlayerMode.HandleLMBAction(value);
    }


    void Update()
    {
        _currentPlayerMode.UpdatePlayerMode();
    }

    public void RMBClick()
    {
        _currentPlayerMode.RMBClicked();
    }
}
