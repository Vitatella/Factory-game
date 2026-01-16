using UnityEngine;

public class ReturnState : IState
{
    private DefenseShootingEnemy _enemy;
    public ReturnState(DefenseShootingEnemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.MoveTo(_enemy.DefensePoint);
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
        else if (Vector2.Distance(_enemy.transform.position, _enemy.DefensePoint) < _enemy.MinDistanceToDefensePoint)
        {
            _enemy.TransitionTo(_enemy.IdleState);
        }
    }
}
