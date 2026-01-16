using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SniperEnemy : ShootingEnemy
{
    [SerializeField] private float _minDistanceToDefensePoint;
    [SerializeField] private float _shootingDistance;
    [SerializeField] private float _minShootingDistance;
    [SerializeField] private float _retreatDistance;

    private Vector2[] _directionsToRetreat =
    {
        new Vector2(1,1).normalized, new Vector2(0,1), new Vector2(1,1).normalized,
    };
    private Vector2[] _directionsToRetreat2 =
    {
        new Vector2(-1,-1), new Vector2(1,-1)
    };

    public SniperAttackState AttackState { get; private set; }
    public SniperIdleState IdleState { get; private set; }
    public SniperReturnState ReturnState { get; private set; }
    public RetreatState RetreatState { get; private set; }
    public Vector2 DefensePoint { get; private set; }
    public float MinDistanceToDefensePoint => _minDistanceToDefensePoint;
    public float ShootingDistance => _shootingDistance;
    public float MinShootingDistance => _minShootingDistance;



    private void Start()
    {
        IdleState = new SniperIdleState(this);
        AttackState = new SniperAttackState(this);
        ReturnState = new SniperReturnState(this);
        RetreatState = new RetreatState(this);
        DefensePoint = transform.position;
        TransitionTo(IdleState);
    }

    public Vector2 FindRetreatPoint()
    {
        List<Vector2> directionsToRetreat = _directionsToRetreat.ToList();
        Vector2 directionFromTarget = transform.position - TargetFinder.Target.position;
        while (directionsToRetreat.Count > 0)
        {
            Vector2 randomDirection = directionsToRetreat[Random.Range(0, directionsToRetreat.Count)];
            float angle = Mathf.Atan2(directionFromTarget.x, directionFromTarget.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 checkPosition = rotation * randomDirection * _retreatDistance + transform.position;
            if (IsPositionAvailable(checkPosition))
            {
                return checkPosition;
            }
            else
            {
                directionsToRetreat.Remove(randomDirection);
            }
        }
        directionsToRetreat = _directionsToRetreat.ToList();
        Vector2 randomDirection2 = directionsToRetreat[Random.Range(0, directionsToRetreat.Count)];
        float angle2 = Mathf.Atan2(directionFromTarget.x, directionFromTarget.y) * Mathf.Rad2Deg;
        Quaternion rotation2 = Quaternion.Euler(0, 0, angle2);
        Vector2 checkPosition2 = rotation2 * randomDirection2 * 12 + transform.position;
        return checkPosition2;
    }

    private bool IsPositionAvailable(Vector2 position)
    {
        NavMeshPath path = new NavMeshPath();
        return Agent.CalculatePath(position, path);
        
    }
}
