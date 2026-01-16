using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ShootingUnit : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private TargetFinder _targetFinder;

    private Player _player;

    private bool _isFollowPlayer;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
        _isFollowPlayer = _player.IsUnitsFollowModeEnabled;
        _player.UnitsFollowModeChanged += UpdateFollow;
    }

    private void OnDisable()
    {
        _player.UnitsFollowModeChanged -= UpdateFollow;
    }

    private void UpdateFollow()
    {
        _isFollowPlayer = _player.IsUnitsFollowModeEnabled;
    }

    public void InitializeAgent(Vector2 position)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(position, out hit, Mathf.Infinity, NavMesh.AllAreas);
        _agent.Warp(hit.position);
    }

    public void MoveTo(Vector2 position)
    {
        _agent.SetDestination(position);
    }

    void FixedUpdate()
    {
        if (_targetFinder.Target == null)
        {
            if (_player != null && _isFollowPlayer)
            {
                MoveTo(_player.transform.position);
            }
            else
            {
                MoveTo(transform.position);
            }
        }
        else
        {
            LookAt(_targetFinder.Target.transform.position);
            if (Vector2.Distance(_targetFinder.Target.position, transform.position) > _distanceToAttack)
            {
                MoveTo(_targetFinder.Target.position);
            }
            else
            {
                MoveTo(transform.position);
                {
                    if (_shooting.IsLoaded)
                        _shooting.Shoot();
                }

            }

        }

    }

    private void LookAt(Vector2 point)
    {
        var dir = point - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
