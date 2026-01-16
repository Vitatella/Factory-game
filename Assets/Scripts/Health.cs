using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    public float CurrentHealth { get; private set; }
    public UnityEvent Death;
    public UnityEvent Damaged;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        CurrentHealth = _maxHealth;
        Damaged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Death?.Invoke();
        }
        else
        {
            Damaged?.Invoke();
        }
    }

    public void Recover()
    {
        CurrentHealth = MaxHealth;
        Damaged?.Invoke();
    }
}
