using UnityEngine;

public class RetreatState : IState
{
    private SniperEnemy _enemy;
    private Vector2 _retreatPoint;
    public RetreatState(SniperEnemy sniperEnemy)
    {
        _enemy = sniperEnemy;
    }
    public void Enter()
    {
        FindRetreatPoint();
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
        else if (Vector2.Distance(_enemy.transform.position, _retreatPoint) < _enemy.MinDistanceToDefensePoint)
        {
            FindRetreatPoint();
        }
        else if (Vector2.Distance(_enemy.transform.position, _enemy.TargetFinder.Target.position) >= _enemy.ShootingDistance)
        {
            _enemy.TransitionTo(_enemy.AttackState);
        }
        else if (_enemy.Agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            FindRetreatPoint();
        }
    }
    private void FindRetreatPoint()
    {
        _retreatPoint = _enemy.FindRetreatPoint();
        _enemy.MoveTo(_retreatPoint);
    }
}
