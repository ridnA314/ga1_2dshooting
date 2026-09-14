using UnityEngine;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    //when Button clicked, Toggle
    //player auto attack
    //player auto move

    private Image _myImage;

    [Header("on/off Sprite")]
    [SerializeField]
    private Sprite _onSprite;

    [SerializeField]
    private Sprite _offSprite;

    private bool _autoMode = false;
    private Player _player;

    private void Start()
    {
        _myImage = GetComponent<Image>();
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

        if (_myImage == null)
        {
            Debug.LogWarning("can not find Image component");
            return;
        }

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }
}