using UnityEngine;

public class NPCInteractionCheck : MonoBehaviour
{
    [SerializeField] private string playerTag;

    private bool _isPlayer = true;

    public bool IsPlayer => _isPlayer;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(playerTag)) {
            _isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(playerTag)) {
            _isPlayer = false;
        }
    }
}
