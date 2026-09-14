using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    //when Button clicked, Toggle
    //player auto attack
    //player auto move

    private Image _myImage;
    private AudioSource _audioSource;

    [Header("on/off Sprite")]
    [SerializeField]
    private Sprite _onSprite;

    [SerializeField]
    private Sprite _offSprite;

    private bool _autoMode = false;
    private Player _player;

    [Header("Animation when clicked")]
    [SerializeField]
    private AnimationCurve _bumpCurve;

    private float _currentScale = 1.0f;
    private float _elapsedTime = 0f;
    private bool _isBumping = false;
    private const float BumpDuration = 0.3f;
    private const float BumpScale = 1.1f;

    private void Start()
    {
        _myImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindAnyObjectByType<Player>();

        AutoToggle();
    }

    public void AutoToggle()
    {
        if (_player == null)
        {
            Debug.LogWarning("can not find Object that has Player component");
            return;
        }

        _autoMode = !_autoMode;
        _player.GetComponent<PlayerFire>().SetAuto(_autoMode);
        _player.GetComponent<PlayerMove>().enabled = !_autoMode;
        _player.GetComponent<PlayerAutoMove>().enabled = _autoMode;
        if (_autoMode)
        {
            PlayerAnimation();
        }

        if (_myImage == null)
        {
            Debug.LogWarning("can not find Image component");
            return;
        }

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }

    public void PlayerAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0f;
    }

    private void Update()
    {
        if (!_isBumping) return;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > BumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        float time = _elapsedTime / BumpDuration;
        float curveValue = _bumpCurve.Evaluate(time);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * BumpScale, curveValue);
    }

    public void PlaySound()
    {
        if (_audioSource == null) return;
        _audioSource.Play();
    }

    //ToDo : 버튼 클릭 시 애니메이션 추가 + 사운드 추가
    //Animation : 코드로 구현 약간 커졌다가 원래대로
    //sound : 일레븐랩스에서 버튼 클릭 공ㅇ용 사운드 만들어서 적용
}