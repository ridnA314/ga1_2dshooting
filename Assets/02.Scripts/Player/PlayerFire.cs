using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePointL;
    public Transform FirePointR;

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
            bulletL.transform.position = FirePointL.position;
            GameObject bulletR = Instantiate(BulletPrefab);
            bulletR.transform.position = FirePointR.position;
            
            _timer = 0f;
        }
    }
    
    private void ChangeFireMode()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _isAutoFire = !_isAutoFire;
        }
    }
}
