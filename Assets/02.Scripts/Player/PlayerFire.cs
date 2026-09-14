using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFire : MonoBehaviour
{
    public Bullet BulletPrefab;
    public Bullet SupportBulletPrefab;

    public Transform[] FirePointTransforms = new Transform[4];

    [SerializeField]
    private float _attackSpeed = 0.2f;

    private float _attackCoolTime = 4.5f;

    public float AttackCoolTime => _attackCoolTime;

    private float _timer;
    private bool _isAutoFire;

    private float _powerBonus = 0f;
    public float PowerBonus => _powerBonus;

    private void Awake()
    {
        _timer = _attackCoolTime;
        _isAutoFire = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime * (1 + _attackSpeed);
        if (_isAutoFire || Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        ChangeFireMode();
    }

    private void Fire()
    {
        if (_timer > _attackCoolTime)
        {
            //ToDo: Do not Direct creation -> Use Bullet in Pool
            Bullet bulletL = BulletPool.Instance.GetBullet(BulletType.Main, _powerBonus);
            bulletL.transform.position = FirePointTransforms[0].position;

            Bullet bulletR = BulletPool.Instance.GetBullet(BulletType.Main, _powerBonus);
            bulletR.transform.position = FirePointTransforms[1].position;

            SupportFire();

            _timer = 0f;
        }
    }

    private void SupportFire()
    {
        Bullet supportBulletL = BulletPool.Instance.GetBullet(BulletType.Sub, _powerBonus);
        supportBulletL.transform.position = FirePointTransforms[2].position;

        Bullet supportBulletR = BulletPool.Instance.GetBullet(BulletType.Sub, _powerBonus);
        supportBulletR.transform.position = FirePointTransforms[3].position;
    }

    private void ChangeFireMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoFire = !_isAutoFire;
        }
    }

    public void SetAuto(bool auto)
    {
        _isAutoFire = auto;
    }

    public void GrowUpPower(float powerBonus)
    {
        _powerBonus += powerBonus;
    }

    public void GrowUpAttackSpeed(float attackSpeedBonus, float attackSpeedLimit)
    {
        _attackSpeed += attackSpeedBonus;
        if (_attackSpeed > attackSpeedLimit)
        {
            _attackSpeed = attackSpeedLimit;
        }
    }
}