using UnityEngine;

public class DefenceEnemy : MonoBehaviour
{
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private TargetFinder _targetFinder;

    private void Start()
    {
        EnemiesCounter.Instance.AddEnemy(gameObject);
    }

    void FixedUpdate()
    {
        if (_targetFinder.Target != null)
        {
            LookAt(_targetFinder.Target.position);
            if (_shooting.IsLoaded)
                _shooting.Shoot();
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
        EnemiesCounter.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
