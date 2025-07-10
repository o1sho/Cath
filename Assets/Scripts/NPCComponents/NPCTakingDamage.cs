using UnityEngine;

public class NPCTakingDamage : MonoBehaviour
{
    private void Awake() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Throwable")) {
            Debug.Log("Stun");
        }
    }
}
