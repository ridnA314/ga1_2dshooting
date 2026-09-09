using UnityEngine;

public class TransparencyItem : Item
{
    [SerializeField]
    private float _transparencyTime = 0.6f;

    protected override void GiveEffect()
    {
        if (_playerTransform.gameObject.TryGetComponent(out Player player))
        {
            player.Transparency(_transparencyTime);
        }
    }
}