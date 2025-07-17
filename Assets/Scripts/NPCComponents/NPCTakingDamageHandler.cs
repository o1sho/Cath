using UnityEngine;

public class NPCTakingDamageHandler : MonoBehaviour
{

    [SerializeField] bool isDestroyed;
    [SerializeField] bool isHasADrop;
    [SerializeField] GameObject dropPrefab;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Throwable")) {
            if (isDestroyed) Destroy(gameObject);
            if (isHasADrop) Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
    }
}
