using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFire : MonoBehaviour
{
    public Bullet BulletPrefab;
    public Bullet SupportBulletPrefab;

    public Transform[] FirePointTransforms = new Transform[4];

    [SerializeField]
    private float _attackCoolTime = 2.2f;

    private float _timer;
    private bool _isAutoFire;

    private float _powerBonus = 0f;

    private void Awake()
    {
        _timer = _attackCoolTime;
        _isAutoFire = false;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
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
            Bullet bulletL = Instantiate(BulletPrefab);
            bulletL.Initialize(_powerBonus);
            bulletL.transform.position = FirePointTransforms[0].position;

            Bullet bulletR = Instantiate(BulletPrefab);
            bulletR.Initialize(_powerBonus);
            bulletR.transform.position = FirePointTransforms[1].position;

            SupportFire();

            _timer = 0f;
        }
    }

    private void SupportFire()
    {
        Bullet supportBulletL = Instantiate(SupportBulletPrefab);
        supportBulletL.Initialize(_powerBonus);
        supportBulletL.transform.position = FirePointTransforms[2].position;

        Bullet supportBulletR = Instantiate(SupportBulletPrefab);
        supportBulletL.Initialize(_powerBonus);
        supportBulletR.transform.position = FirePointTransforms[3].position;
    }

    private void ChangeFireMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoFire = !_isAutoFire;
        }
    }

    public void GrowUpPower(float powerBonus)
    {
        _powerBonus += powerBonus;
    }

    public void GrowUpAttackSpeed(float attackSpeedBonus, float attackSpeedLimit)
    {
        _attackCoolTime -= attackSpeedBonus;
        if (_attackCoolTime < attackSpeedLimit)
        {
            _attackCoolTime = attackSpeedLimit;
        }
    }
}