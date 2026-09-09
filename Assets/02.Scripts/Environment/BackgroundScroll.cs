using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material _material;
    private float _offSetY = 0f;

    [SerializeField]
    private float _scrollSpeed = 0.1f;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _offSetY += Time.deltaTime * _scrollSpeed;

        //ToDo: Material Property Block을 활용한 최적화
        _material.mainTextureOffset = new Vector2(0, _offSetY);
    }
}