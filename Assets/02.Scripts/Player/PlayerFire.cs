using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject SupportBulletPrefab;
    
    public Transform[] FirePointTransforms = new Transform[4];

    public float FireCoolTime;
    
    private float _timer;
    private bool _isAutoFire;

    private void Awake()
    {
        _timer = FireCoolTime;
        _isAutoFire = false;
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        Fire();
        ChangeFireMode();
    }

    private void Fire()
    {
        if ((_isAutoFire || Input.GetKeyDown(KeyCode.Space)) && _timer > FireCoolTime)
        {
            GameObject bulletL = Instantiate(BulletPrefab);
            bulletL.transform.position = FirePointTransforms[0].position;
            GameObject bulletR = Instantiate(BulletPrefab);
            bulletR.transform.position = FirePointTransforms[1].position;

            SupportFire();
            
            _timer = 0f;
        }
    }
    
    private void SupportFire()
    {
        GameObject supportBulletL = Instantiate(SupportBulletPrefab);
        supportBulletL.transform.position = FirePointTransforms[2].position;
        GameObject supportBulletR = Instantiate(SupportBulletPrefab);
        supportBulletR.transform.position = FirePointTransforms[3].position;
    }
    
    private void ChangeFireMode()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _isAutoFire = !_isAutoFire;
        }
    }
}
