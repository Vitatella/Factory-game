using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;
    private PlayerBase _base;

    public event Action UnitsFollowModeChanged;
    public bool IsUnitsFollowModeEnabled { get; private set; }

    private void Start()
    {
        _base = FindAnyObjectByType<PlayerBase>();
    }

    public void ToggleFollowMode(bool value)
    {
        IsUnitsFollowModeEnabled = value;
        UnitsFollowModeChanged?.Invoke();
    }

    public void Respawn()
    {
        transform.position = _base.transform.position;
        _health.Recover();
    }
}
