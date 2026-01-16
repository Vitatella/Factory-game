using UnityEngine;

public class IdleState : IState
{
    private DefenseShootingEnemy _enemy;
    public IdleState(DefenseShootingEnemy enemy)
    {
        _enemy = enemy;
    }
    public void Enter()
    {
        _enemy.MoveTo(_enemy.transform.position);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (_enemy.TargetFinder.Target != null)
        {
            _enemy.TransitionTo(_enemy.AttackState);
        }
    }
}
