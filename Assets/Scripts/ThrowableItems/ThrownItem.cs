using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    private Camera _mainCamera;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private IThrowableItem _sourceItem;

    public void Init(IThrowableItem sourceItem) {
        _sourceItem = sourceItem;
    }

    private void Awake() {
        _mainCamera = Camera.main;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!IsVisibleByCamera()) {
            OnHit();
        }
    }

    private bool IsVisibleByCamera() {
        Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<IThrowableTarget>(out var target)) {
            target.OnHitByItem(_sourceItem);
            OnHit();
        }
    }

    private void OnHit() {
        if (_animator.isActiveAndEnabled) {
            _animator.SetTrigger("isDestroy");
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0;
        }
        else {
            Destroy();
        }
    }

    public void Destroy() => Destroy(gameObject);
}
