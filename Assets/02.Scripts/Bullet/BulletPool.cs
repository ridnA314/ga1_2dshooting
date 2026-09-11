using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    // Object Pooling : Prepare Pool of Object -> Use GameObject that in Pool When need it
    // required attribute
    [Header("Main Bullet Prefab")]
    [SerializeField]
    private Bullet _bulletPrefab;

    [Header("Main Pool Size")]
    [SerializeField]
    private int _poolSize = 30;

    [Header("Sub Bullet Prefab")]
    [SerializeField]
    private Bullet _subBulletPrefab;

    [Header("Sub Pool Size")]
    [SerializeField]
    private int _subBulletPoolSize = 30;

    // The pool that contain created bullets
    private Bullet[] _pool;

    private Bullet[] _subBulletBool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        //Create Pool as size of pool 
        _pool = new Bullet[_poolSize];
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform);
            bullet.gameObject.SetActive(false); //Deactive because do not use now
            _pool[i] = bullet;
        }

        _subBulletBool = new Bullet[_subBulletPoolSize];
        for (int i = 0; i < _subBulletPoolSize; i++)
        {
            Bullet bullet = Instantiate(_subBulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            _subBulletBool[i] = bullet;
        }
    }

    public Bullet GetBullet()
    {
        foreach (Bullet bullet in _pool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        return null;
    }

    public Bullet GetSubBullet()
    {
        foreach (Bullet bullet in _subBulletBool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(true);
                bullet.OnSpawn();
                return bullet;
            }
        }

        return null;
    }
}