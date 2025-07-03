using UnityEngine;

[CreateAssetMenu(fileName = "ItemForThrowing", menuName = "ThrowableItems/ItemForThrowing")]
public class ItemForThrowing : ScriptableObject, IThrowableItem {

    [SerializeField] private string _itemName = "Item";
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _throwSpeed = 10f;
    [SerializeField] private float angularSpeed = 360f;

    public string Name => _itemName;
    public GameObject Prefab => _prefab;
    public float ThrowSpeed => _throwSpeed;
    public float AngularSpeed => angularSpeed;

    public void OnThrow(Vector2 direction) {
        Debug.Log($"Throwing {_itemName} in direction {direction}");
    }
}
