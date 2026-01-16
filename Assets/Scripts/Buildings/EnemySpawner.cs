using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Building
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private AttackEnemy _enemy;
    [SerializeField] private int _enemiesToAttack = 2;
    [SerializeField] private ShootingEnemy[] _defenseEnemies;
    [SerializeField] private int _defenseUnitsCount;
    private List<AttackEnemy> _spawnedEnemies = new List<AttackEnemy>();
    private int _defenseUnits;

    void Start()
    {
        EnemiesCounter.Instance.AddEnemy(gameObject);
        StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        while (true)
        {

            yield return new WaitForSeconds(_spawnTime);
            if (_defenseUnits < _defenseUnitsCount)
            {
                SpawnDefenseUnit();
            }
            else
            {
                SpawnAttackEnemy();
            }
        }
    }

    private void SpawnAttackEnemy()
    {
        var enemy = Instantiate(_enemy);
        enemy.transform.position = (Vector2)transform.position + Random.insideUnitCircle * 2;
        enemy.InitializeAgent((Vector2)transform.position + Random.insideUnitCircle * 2);
        enemy.MoveTo((Vector2)transform.position + Random.insideUnitCircle * 6);
        _spawnedEnemies.Add(enemy);
        if (_enemiesToAttack <= _spawnedEnemies.Count)
        {
            foreach (var spawnedEnemy in _spawnedEnemies)
            {
                spawnedEnemy.Attack();
            }
            _spawnedEnemies.Clear();
        }
    }

    private void SpawnDefenseUnit()
    {
        var enemy = Instantiate(_defenseEnemies[Random.Range(0, _defenseEnemies.Length)]);
        enemy.transform.position = (Vector2)transform.position + Random.insideUnitCircle * 2;
        enemy.InitializeAgent((Vector2)transform.position + Random.insideUnitCircle * 2);
        enemy.MoveTo((Vector2)transform.position + Random.insideUnitCircle * 2);
        _defenseUnits++;
        if (_enemiesToAttack <= _spawnedEnemies.Count)
        {
            foreach (var spawnedEnemy in _spawnedEnemies)
            {
                spawnedEnemy.Attack();
            }
            _spawnedEnemies.Clear();
        }
    }

    private void RemoveDefenseUnit()
    {
        _defenseUnits--;
    }

    public override void Destroy()
    {
        EnemiesCounter.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
