using UnityEngine;

public class PlayerThrowHandler : MonoBehaviour
{
    [SerializeField] private PlayerHeldItemDisplayHandler displayHandler;

    [SerializeField] private Transform throwSpawnPoint;

    private IThrowableItem _heldItem;
    public IThrowableItem HeldItem => _heldItem;

    private GameObject _thrownObject;

    private void Awake() {
        displayHandler = GetComponentInChildren<PlayerHeldItemDisplayHandler>();
        Debug.Log(displayHandler);
    }

    public void ThrowItem(Vector2 direction) {
        if (_heldItem == null) return;

        _thrownObject = Instantiate(_heldItem.Prefab, throwSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = _thrownObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * _heldItem.ThrowSpeed;
        rb.angularVelocity = _heldItem.AngularSpeed;

        _heldItem.OnThrow(direction);
        ClearHeldItem();
    }

    public void SetHeldItem(IThrowableItem item) {
        _heldItem = item;
        displayHandler.ShowItem(_heldItem);
    }

    public void ClearHeldItem() {
        _heldItem = null;
        displayHandler.ShowItem(null);
    }

}
