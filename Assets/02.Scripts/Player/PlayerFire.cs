using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePointL;
    public Transform FirePointR;
    
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletL = Instantiate(BulletPrefab);
            bulletL.transform.position = FirePointL.position;
            GameObject bulletR = Instantiate(BulletPrefab);
            bulletR.transform.position = FirePointR.position;
        }
    }
}
