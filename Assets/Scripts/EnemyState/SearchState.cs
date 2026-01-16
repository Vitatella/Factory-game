using UnityEngine;

public class SearchState : IState
{
    private DefenseShootingEnemy _enemy;
    public SearchState(DefenseShootingEnemy enemy)
    {
        _enemy = enemy;
    }
    public void Enter()
    {
        _enemy.MoveTo(_enemy.TargetFinder.LastTargetPosition);
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
        else if (Vector2.Distance(_enemy.TargetFinder.LastTargetPosition, _enemy.transform.position) <= _enemy.MinDistanceToDefensePoint)
        {
            _enemy.TransitionTo(_enemy.ReturnState);
        }
    }
}
