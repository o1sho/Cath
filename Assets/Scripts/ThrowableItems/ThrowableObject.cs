using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    private Camera _mainCamera;
    private Animator _animator;
    private Rigidbody2D _rigidbody;


    private void Awake() {
        _mainCamera = Camera.main;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!IsVisibleByCamera()) {
            OnHit();
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
        if (other.CompareTag("Environment")) {
            OnHit();
             Debug.Log($"{gameObject.name} destroyed because it hit an Environment");
        }
    }

    private void OnHit() {
        _animator.SetTrigger("isDestroy");
        _rigidbody.linearVelocity = new Vector2(0, 0);
        _rigidbody.angularVelocity = 0;
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
