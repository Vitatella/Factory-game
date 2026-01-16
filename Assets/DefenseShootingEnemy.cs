using UnityEngine;

public class DefenseShootingEnemy : ShootingEnemy
{
    [SerializeField] private float _shootingDistance;
    [SerializeField] private float _minDistanceToDefensePoint;
    public float MinDistanceToDefensePoint => _minDistanceToDefensePoint;
    public float ShootingDistance => _shootingDistance;

    public AttackState AttackState { get; private set; }
    public IdleState IdleState { get; private set; }
    public ReturnState ReturnState { get; private set; }
    public SearchState SearchState { get; private set; }
    public Vector2 DefensePoint { get; private set; }

    private void Start()
    {
        DefensePoint = transform.position;
        IdleState = new IdleState(this);
        ReturnState = new ReturnState(this);
        AttackState = new AttackState(this);
        SearchState = new SearchState(this);
        TransitionTo(IdleState);
    }
}
