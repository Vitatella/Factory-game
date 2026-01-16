using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class ShootingEnemy : GroundEnemy
{
    [SerializeField] private TargetFinder _targetFinder;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private float _timeToUpdateState;

    private IState _currentState;
    public TargetFinder TargetFinder => _targetFinder;
    
    public Shooting Shooting => _shooting;
    public event Action Die;


    private IEnumerator UpdateState()
    {
        while (true)
        {
            _currentState?.Update();
            yield return new WaitForSeconds(_timeToUpdateState);    
        }
    }

    private void Awake()
    {
        StartCoroutine(UpdateState());
    }

    public void TransitionTo(IState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Death()
    {
        Die?.Invoke();
        Destroy(gameObject);
    }
}
