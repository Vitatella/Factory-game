using UnityEngine;

public class ShootingMode : MonoBehaviour, IPlayerMode
{
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Player _player;
    private bool _isShooting;

    public void Enter()
    {
        _shooting.ForceReload();
        _isShooting = false;
        _player.ToggleFollowMode(true);
    }

    public void Exit()
    {
        _player.ToggleFollowMode(false);
    }

    public void HandleLMBAction(bool value)
    {
        _isShooting = value;
    }

    public void RMBClicked()
    {
        
    }

    public void UpdatePlayerMode()
    {
        if (_isShooting && _shooting.IsLoaded)
        {
            _shooting.Shoot();
        }
    }

}
