using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private float _damage, _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _lifeTime;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CircleCollider2D _collider;
    private IObjectPool<Bullet> _pool;
    private Coroutine _destroyRoutine;

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    public void Launch(Vector2 position, Vector2 direction, float speed, float damage)
    {
        _speed = speed;
        transform.position = position;
        _rigidbody.simulated = true;
        _rigidbody.linearVelocity = direction.normalized * _speed;
        _renderer.enabled = true;
        _trail.Clear();
        _trail.emitting = true;
        _damage = damage;
        _collider.enabled = true;
        _destroyRoutine = StartCoroutine(SelfDestroy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
        StopCoroutine(_destroyRoutine);
        Explode();
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        Explode();
    }

    private void Explode()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.simulated = false;
        _hit.Play();
        _trail.emitting = false;
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnParticleSystemStopped()
    {
        _pool.Release(this);
    }
}
