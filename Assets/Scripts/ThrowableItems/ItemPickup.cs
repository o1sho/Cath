using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ScriptableObject item;
    private IThrowableItem _throwableItem;

    public IThrowableItem Item => _throwableItem;

    private void Awake() {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;

        // Проверяем, реализует ли item интерфейс IThrowableItem
        if (item is IThrowableItem throwable) {
            _throwableItem = throwable;
        }
        else {
            Debug.LogError($"ItemPickup on {gameObject.name} has invalid item assigned. Must implement IThrowableItem.");
        }
    }

    public void Pickup(PlayerThrow playerThrow) {
        if (_throwableItem == null) {
            Debug.LogWarning($"Cannot pick up: No valid IThrowableItem assigned to {gameObject.name}");
            return;
        }

        if (playerThrow.HeldItem == null) {
            playerThrow.SetHeldItem(_throwableItem);
            Destroy(gameObject); // Уничтожаем объект на земле
            Debug.Log($"Picked up {_throwableItem.Name}");
        }
        else {
            Debug.Log($"Cannot pick up {_throwableItem.Name}: Player already holds an item");
        }
    }
}
