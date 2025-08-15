using UnityEngine;

public class DashUnlockPickup : MonoBehaviour {
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag(playerTag)) return;

        var player = other.GetComponent<Player>();
        if (player != null && player.Abilities != null) {
            player.Abilities.Unlock(Ability.Dash);
            // тут можно всплывашку/звуки/анимку
            Destroy(gameObject);
        }
    }
}
