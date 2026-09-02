using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // require field
    public float Speed;
    
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다
    // Update는 매 프레임마다 실행 -> 초당 프레임 실행은 별도 설정이 없는 경우 성능 내에서 가능한 밚이
    private void Update()
    {
        //1. 키보드 입력을 받는다.
        float h = Input.GetAxis("Horizontal"); // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxis("Vertical"); // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f

        Debug.Log($"h :{h}, v:{v}");
        
        //2. 키보드 입력에 따라 방향을 구한다.
        // 게임에는 벡터라는 타입이 있다. 벡터는(크기와 방향을 의미)
        Vector2 direction = new Vector2(h, v); // 왼쪽 방향
        //Vector2 direction = Vector2.left; //위와 동일
        
        //3. 방향과 속도에 따라 이동한다. //매직 넘버란 : 보는 사람에 따라 의미가 달라질 수 있는 숫자(즉, 헷갈릴 수 있는)
        //transform.Translate(direction * Speed * Time.deltaTime);
        // deltaTime : 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 지났는가를 MS 단위로 반환
        
        // 새로운 위치 = 현재 위치 + 속도(방향 * 크기) * 시간
        //transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }
}
