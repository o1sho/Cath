using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] private Transform throwSpawnPoint;

    private IThrowableItem _heldItem;
    public IThrowableItem HeldItem => _heldItem;

    private GameObject _thrownObject;



    public void ThrowItem(Vector2 direction) {
        if (_heldItem == null) return;

        _thrownObject = Instantiate(_heldItem.Prefab, throwSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = _thrownObject.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * _heldItem.ThrowSpeed;

        _heldItem.OnThrow(direction);
        _heldItem = null;
    }

    public void SetHeldItem(IThrowableItem item) {
        _heldItem = item;
    }

    public void ClearHeldItem() {
        _heldItem = null;
    }

}
