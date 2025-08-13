using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerHeldItemDisplayHandler : MonoBehaviour, IPlayerComponent 
{
    private Player _player;
    private SpriteRenderer _spriteRenderer;

    public void Init(Player player) {
        _player = player;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowItem(IThrowableItem item) {
        if (item == null) {
            _spriteRenderer.sprite = null;
            return;
        }

        _spriteRenderer.sprite = item.DisplaySprite;

        //_spriteRenderer.transform.position = transform.position;
    }

    private void Update() {
        var playerVelocity = _player.Rigidbody.linearVelocity;

        if (playerVelocity.x > 0) _spriteRenderer.transform.localPosition = new Vector3(0.25f, 0.25f, 0);
        if (playerVelocity.x < 0) _spriteRenderer.transform.localPosition = new Vector3(-0.25f, 0.25f, 0);
        if (playerVelocity.y < 0) _spriteRenderer.transform.localPosition = new Vector3(0f, -0.25f, 0);
        if (playerVelocity.y > 0) _spriteRenderer.transform.localPosition = new Vector3(0f, 0.5f, 0);
    }
}
