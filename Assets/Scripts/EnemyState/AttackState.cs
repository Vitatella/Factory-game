using UnityEngine;

public class AttackState : IState
{
    private DefenseShootingEnemy _enemy;
    public AttackState(DefenseShootingEnemy enemy)
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
        if (_enemy.TargetFinder.Target == null)
        {
            _enemy.TransitionTo(_enemy.SearchState);
        }
        else if (Vector2.Distance(_enemy.transform.position, _enemy.TargetFinder.Target.position) > _enemy.ShootingDistance)
        {
            _enemy.MoveTo(_enemy.TargetFinder.Target.position);
        }
        else
        {
            _enemy.MoveTo(_enemy.transform.position);
            _enemy.LookAt(_enemy.TargetFinder.Target.position);
            if (_enemy.Shooting.IsLoaded)
            {
                _enemy.Shooting.Shoot();
            }
        }
    }
}
