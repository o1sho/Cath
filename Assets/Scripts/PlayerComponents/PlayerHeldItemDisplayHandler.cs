using UnityEngine;

public class PlayerHeldItemDisplayHandler : MonoBehaviour {
    private SpriteRenderer _displayRenderer;

    private void Awake() {
        _displayRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowItem(IThrowableItem item) {
        if (item == null) {
            _displayRenderer.sprite = null;
            return;
        }

        _displayRenderer.sprite = item.DisplaySprite;
        _displayRenderer.transform.position = transform.position;
    }
}
