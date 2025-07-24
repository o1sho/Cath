using UnityEngine;

public class PlayerThrowHandler : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private PlayerHeldItemDisplayHandler displayHandler;

    [SerializeField] private Transform throwSpawnPoint;

    private IThrowableItem _heldItem;
    private GameObject _thrownObject;
    private Player _player;

    public IThrowableItem HeldItem => _heldItem;

    //---------------
    public void Init(Player player) {
        _player = player;
        if (displayHandler == null)
            displayHandler = GetComponentInChildren<PlayerHeldItemDisplayHandler>();
    }
    //---------------

    public void ThrowItem(Vector2 direction) {
        if (_heldItem == null) return;

        _thrownObject = Instantiate(_heldItem.Prefab, throwSpawnPoint.position, Quaternion.identity);

        if (_thrownObject.TryGetComponent<Rigidbody2D>(out var rb)) {
            rb.linearVelocity = direction * _heldItem.ThrowSpeed;
            rb.angularVelocity = _heldItem.AngularSpeed;
        }

        if (_thrownObject.TryGetComponent<ThrownItem>(out var thrownItem)) {
            thrownItem.Init(_heldItem);
        }

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
