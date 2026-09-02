using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePointL;
    public Transform FirePointR;

    public float FireCoolTime;
    
    private float _timer;

    private void Awake()
    {
        _timer = FireCoolTime;
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _timer > FireCoolTime)
        {
            GameObject bulletL = Instantiate(BulletPrefab);
            bulletL.transform.position = FirePointL.position;
            GameObject bulletR = Instantiate(BulletPrefab);
            bulletR.transform.position = FirePointR.position;
            
            _timer = 0f;
        }
    }
}
