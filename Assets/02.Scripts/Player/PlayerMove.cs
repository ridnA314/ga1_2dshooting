using UnityEngine;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    // require field
    public float SpeedScalar;
    public float Acceleration;
    
    private float _currentAcceleration;
    
    private float _cameraStartX;
    private float _cameraEndX;
    private float _cameraStartY;
    private float _cameraHalfY;

    private List<Vector2> _moveCommandRecords;
    private List<KeyCode> _acceleationCommandRecords;
    private float _timer;
    private bool _replaying;

    private void Awake()
    {
        _moveCommandRecords = new List<Vector2>();
        _acceleationCommandRecords = new List<KeyCode>();
        _timer = 0;
        _replaying = false;
        
        Camera cam = Camera.main;
        Vector2 cameraCenter = cam.transform.position;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        _cameraStartX = cameraCenter.x - halfWidth;
        _cameraEndX =  cameraCenter.x + halfWidth;
        _cameraStartY =  cameraCenter.y - halfHeight;
        _cameraHalfY = cameraCenter.y;
    }
    
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다
    // Update는 매 프레임마다 실행 -> 초당 프레임 실행은 별도 설정이 없는 경우 성능 내에서 가능한 밚이
    private void Update()
    {
        if (!_replaying)
        {
            _timer +=  Time.deltaTime;
            Vector2 speed = GetSpeed();
            Move(speed);
        }
    }

    private Vector2 GetSpeed()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(h, v).normalized;
        KeyCode accelerationKey = KeyCode.None;
        Vector2 speed = direction * SpeedScalar;
        speed = Accelate(speed, out KeyCode key);

        if (_timer >= 0.1f)
        {
            _moveCommandRecords.Add(speed);
            _acceleationCommandRecords.Add(key);
            _timer = 0f;
        }
        
        return speed;
    }
    
    private Vector2 Accelate(Vector2 speed, out KeyCode key)
    {
        _currentAcceleration = 1f;
        key = KeyCode.None;
        if (Input.GetKey(KeyCode.E))
        {
            key = KeyCode.E;
            _currentAcceleration = Acceleration;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            key = KeyCode.Q;
            _currentAcceleration = 1f / Acceleration;
        }
        
        Vector2 acceleratedSpeed = speed * _currentAcceleration;
        return acceleratedSpeed;
    }
    
    private void Move(Vector2 speed)
    {
        Vector2 distance = speed * Time.deltaTime;

        bool isOverStartX = transform.position.x + distance.x <= _cameraStartX;
        bool isOverEndX = transform.position.x + distance.x >= _cameraEndX;
        bool isOverStartY = transform.position.y + distance.y <= _cameraStartY;
        bool isOverEndY = transform.position.y + distance.y >= _cameraHalfY;
        
        if (isOverStartX)
        {
            distance = new Vector2(_cameraEndX - transform.position.x, 0);
        }
        else if (isOverEndX)
        {
            distance = new Vector2(_cameraStartX - transform.position.x, 0);
        }
        
        if (!isOverEndY && !isOverStartY)
        {
            transform.Translate(distance);
        }
    }
}
