using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool _instance;
    public static EnemyPool Instance => _instance;

    [Header("Enemy Prefabs")]
    [SerializeField]
    private Enemy[] _enemyPrefabs;

    [Header("Pool Size")]
    [SerializeField]
    private int _poolSize = 30;

    private Enemy[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        int j;
        int length = _enemyPrefabs.Length;
        //Create Pool as size of pool and number of bullet's type
        _pool = new Enemy[length, _poolSize];
        for (int i = 0; i < length; i++)
        {
            Enemy enemyPrefab = _enemyPrefabs[i];
            for (j = 0; j < _poolSize; j++)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform);
                enemy.gameObject.SetActive(false); //Deactive because do not use now
                _pool[i, j] = enemy;
            }
        }
    }

    public Enemy GetEnemy(EnemyType enemyType, Transform playerTransform, ItemDropDataTableSO itemDropDataTable)
    {
        for (int i = 0; i < _pool.GetLength(0); i++)
        {
            if (_pool[i, 0].Type != enemyType)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Enemy enemy = _pool[i, j];

                if (!enemy.gameObject.activeSelf)
                {
                    enemy.gameObject.SetActive(true);
                    enemy.OnSpawn(playerTransform, itemDropDataTable);
                    return enemy;
                }
            }
        }

        return null;
    }
}