using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake() {
        _mainCamera = Camera.main;
    }

    private void Update() {
        if (!IsVisibleByCamera()) {
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} destroyed because it left the camera bounds");
        }
    }

    private bool IsVisibleByCamera() {
        Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // if (other.CompareTag("Enemy"))
        // {
        //     Destroy(gameObject);
        //     Debug.Log($"{gameObject.name} destroyed because it hit an enemy");
        // }
    }
}
