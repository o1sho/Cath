using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] targets; // Что включить
    [SerializeField] private string activatorTag = "Player";
    [SerializeField] private bool oneShot = true;
    [Header("Camera Shake")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private bool _done;

    private void Reset() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (_done && oneShot) return;
        if (!other.CompareTag(activatorTag)) return;

        foreach (var go in targets) {
            if (go) {
                if (!go.activeSelf) go.SetActive(true);
                else if (go.activeSelf) go.SetActive(false);
            }
            
        }

        if (impulseSource != null) {
            impulseSource.GenerateImpulse();
        }

        _done = true;
    }
}
