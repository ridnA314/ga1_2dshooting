using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField]
    private float _throwDistance = 2f;

    [SerializeField]
    private float _attackCoolTime = 3.5f;

    [SerializeField]
    private Bomb _bombPrefab;

    private float _timer;

    private void Start()
    {
        _timer = _attackCoolTime;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B))
        {
            ShootBomb();
        }
    }

    private void ShootBomb()
    {
        if (_timer >= _attackCoolTime)
        {
            Vector2 bombPosition = transform.position;
            bombPosition.y += _throwDistance;

            Bomb bomb = Instantiate(_bombPrefab);
            bomb.transform.position = transform.position;
            bomb.Initialize(bombPosition);

            _timer = 0f;
        }
    }
}