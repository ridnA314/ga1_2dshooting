using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 간격")]
    [SerializeField]
    private float _spawnInterval = 3f;

    [SerializeField]
    private EnemySpawnDataTableSO _spawnDataTable;

    private float _timer = 2f;

    [Header("Item Drop Table")]
    [SerializeField]
    private ItemDropDataTableSO _itemDropDataTable;

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
        if (_playerTransform == null) return;
        if (_spawnDataTable.Datas.Length <= 0) return;

        GameObject enemyObejct = null;

        //Todo: scritable Object를 사용해서 리펙토잉

        //1. summation of all weight
        int totalWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        //2. 전체 가중치 범위에서 random한 정수 추출
        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        //3. 가중치를 누적하면서 선택된 구간 탐색
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                enemyObejct = Instantiate(data.EnemyPrefab);
                enemyObejct.transform.position = transform.position;
                break;
            }
        }

        if (enemyObejct.TryGetComponent(out Enemy enemy))
        {
            enemy.Initialize(_playerTransform);
            enemy.SetItems(_itemDropDataTable);
        }
    }
}