using UnityEngine;
using UnityEngine.AI;

public abstract class GroundEnemy : Enemy
{
    [SerializeField] private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;

    public void InitializeAgent(Vector2 position)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, Mathf.Infinity, NavMesh.AllAreas);
        _agent.Warp(hit.position);
    }

    public void LookAt(Vector2 point)
    {
        var dir = point - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public override void MoveTo(Vector2 position)
    {
        _agent.SetDestination(position);
    }
}
