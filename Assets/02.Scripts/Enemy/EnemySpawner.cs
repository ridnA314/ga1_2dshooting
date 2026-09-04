using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 간격")]
    [SerializeField]
    private float _spawnInterval = 3f;

    private float _timer;

    [Header("스폰할 적 프리팹")]
    [SerializeField]
    private Enemy _enemyPrefab;

    [Header("탐색할 플레이어")]
    [SerializeField]
    private Transform _playerTransform;

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
        if (_playerTransform == null || _enemyPrefab == null) return;

        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.Initialize(_playerTransform);
        enemy.transform.position = transform.position;
    }
}