using UnityEngine;

public class SniperAttackState : IState
{
    private SniperEnemy _enemy;
    public SniperAttackState(SniperEnemy enemy)
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
            _enemy.TransitionTo(_enemy.ReturnState);
        }
        else if (Vector2.Distance(_enemy.transform.position, _enemy.TargetFinder.Target.position) > _enemy.ShootingDistance)
        {
            _enemy.MoveTo(_enemy.TargetFinder.Target.position);
        }
        else if (Vector2.Distance(_enemy.transform.position, _enemy.TargetFinder.Target.position) >= _enemy.MinShootingDistance)
        {
            _enemy.LookAt(_enemy.TargetFinder.Target.position);
            _enemy.MoveTo(_enemy.transform.position);
            if (_enemy.Shooting.IsLoaded)
            {
                _enemy.Shooting.Shoot();
            }
        }
        else
        {
            _enemy.TransitionTo(_enemy.RetreatState);
        }

    }
}

