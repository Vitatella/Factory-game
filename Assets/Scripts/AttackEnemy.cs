using UnityEngine;

public class AttackEnemy : GroundEnemy
{
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private TargetFinder _targetFinder;

    private PlayerBase _base;
    private bool _isAttack;



    void Start()
    {
        _base = FindAnyObjectByType<PlayerBase>();
        EnemiesCounter.Instance.AddEnemy(gameObject);
    }

    void FixedUpdate()
    {
        if (_targetFinder.Target == null)
        {
            if (_base != null && _isAttack)
            {
                MoveTo(_base.transform.position);
            }
            else
            {
                MoveTo(transform.position);
            }
        }
        else
        {
            LookAt(_targetFinder.Target.position);
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

    public void Attack()
    {
        _isAttack = true;
    }



    private void LookAt(Vector2 point)
    {
        var dir = point - (Vector2)transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public void Destroy()
    {
        EnemiesCounter.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
