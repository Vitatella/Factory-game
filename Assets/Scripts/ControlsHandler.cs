using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private BuildingSelection _selection;
    [SerializeField] private PlayerModeHandler _playerModeHandler;

    private void OnMove(InputValue inputValue)
    {
        _movement.MoveDirection = inputValue.Get<Vector2>();
    }

    private void OnRotate(InputValue input)
    {
        var value = input.Get<float>();
        _buildingCreator.Rotate(value);
    }

    private void OnAttack(InputValue input)
    {
        var value = input.Get<float>();
        _playerModeHandler.HandleLMBPress(value > 0);
        
    }

    private void OnPlayerModeSwitch()
    {
        _playerModeHandler.SwitchPlayerMode();
    }

    private void OnRightMouseClick()
    {
        _playerModeHandler.RMBClick();
    }
}
