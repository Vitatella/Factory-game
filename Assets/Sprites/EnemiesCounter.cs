using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesCounter : Singleton<EnemiesCounter>
{
    private List<GameObject> _liveEnemies = new List<GameObject>();
    private bool _allEnemiesSpawned;
    public UnityEvent LevelCompleted;

    public void AddEnemy(GameObject enemy)
    {
        _liveEnemies.Add(enemy);
    }

    public void SetAllEnemiesSpawned(bool value)
    {
        _allEnemiesSpawned = value; 
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _liveEnemies.Remove(enemy);
        if (_liveEnemies.Count == 0 && _allEnemiesSpawned)
        {
            LevelCompleted?.Invoke();
        }
    }
}
