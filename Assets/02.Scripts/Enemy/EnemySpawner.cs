using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 간격")]
    [SerializeField]
    private float _spawnInterval = 3f;

    private float _timer;

    [Header("Downward")]
    [SerializeField]
    private Enemy _downWardEnemyPrefab;

    [Header("Aimed")]
    [SerializeField]
    private Enemy _aimedEnemyPrefab;

    [Header("Homing")]
    [SerializeField]
    private Enemy _homingEnemyPrefab;

    [Header("탐색할 플레이어")]
    [SerializeField]
    private Transform _playerTransform;

    [Header("스폰 확률")]
    [SerializeField]
    private int[] _probabilitiesForSpawnEnemy;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            _spawnInterval = UnityEngine.Random.Range(1f, 3f);

            Spawn();
        }
    }

    private void Spawn()
    {
        if (_playerTransform == null) return;
        if (_homingEnemyPrefab == null || _aimedEnemyPrefab == null || _downWardEnemyPrefab == null) return;

        Enemy enemy = GetEnemyPrefabByProbability();
        enemy = Instantiate(enemy);
        enemy.Initialize(_playerTransform);
        enemy.transform.position = transform.position;
    }

    private Enemy GetEnemyPrefabByProbability()
    {
        if (_probabilitiesForSpawnEnemy.Length < 3) return _downWardEnemyPrefab;

        int probability = UnityEngine.Random.Range(0, 100);

        if (probability <= _probabilitiesForSpawnEnemy[2])
        {
            return _homingEnemyPrefab;
        }

        if (probability <= _probabilitiesForSpawnEnemy[1] + _probabilitiesForSpawnEnemy[2])
        {
            return _aimedEnemyPrefab;
        }

        return _downWardEnemyPrefab;
    }
}