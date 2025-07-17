using UnityEngine;

public interface IThrowableItem
{
    string Name { get; }
    GameObject Prefab { get; }
    float ThrowSpeed { get; }
    float AngularSpeed { get; }
    Sprite DisplaySprite { get; }
    void OnThrow(Vector2 direction);
}
