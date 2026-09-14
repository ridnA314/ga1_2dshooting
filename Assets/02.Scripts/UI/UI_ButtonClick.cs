using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        if (_button != null)
        {
            _button.onClick.AddListener(PlaySound);
            _button.onClick.AddListener(PlayAnimation);
        }
    }

    public void PlayAnimation()
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
}