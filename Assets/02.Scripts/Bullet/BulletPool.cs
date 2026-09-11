using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    // Object Pooling : Prepare Pool of Object -> Use GameObject that in Pool When need it
    // required attribute
    [Header("Bullet Prefabs")]
    [SerializeField]
    private Bullet[] _bulletPrefabs;

    [Header("Pool Size")]
    [SerializeField]
    private int _poolSize = 30;

    // The pool that contain created bullets
    private Bullet[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        int j;
        int length = _bulletPrefabs.Length;
        //Create Pool as size of pool and number of bullet's type
        _pool = new Bullet[length, _poolSize];
        for (int i = 0; i < length; i++)
        {
            Bullet bulletPrefab = _bulletPrefabs[i];
            for (j = 0; j < _poolSize; j++)
            {
                Bullet bullet = Instantiate(bulletPrefab, transform);
                bullet.gameObject.SetActive(false); //Deactive because do not use now
                _pool[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType bulletType, float powerBonus)
    {
        for (int i = 0; i < _pool.GetLength(0); i++) //loop by type
        {
            if (_pool[i, 0].Type != bulletType) //first element's type not equal target type
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++) //loop at bullet's pool of target type
            {
                Bullet bullet = _pool[i, j];

                if (!bullet.gameObject.activeSelf)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn(powerBonus);
                    return bullet;
                }
            }
        }

        return null;
    }
}