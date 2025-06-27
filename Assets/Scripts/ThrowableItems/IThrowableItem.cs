using UnityEngine;

public interface IThrowableItem
{
    string Name { get; }
    GameObject Prefab { get; }
    float ThrowSpeed { get; }
    void OnThrow(Vector2 direction);
}
