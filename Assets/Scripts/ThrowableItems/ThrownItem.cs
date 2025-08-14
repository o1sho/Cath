using System;
using UnityEngine;

[RequireComponent(typeof(ThrownItemRuntime))]
public class ThrownItem : MonoBehaviour
{
    private Camera _mainCamera;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private IThrowableItem _source;
    private ThrownItemRuntime _rt;

    public event Action Hit;

    public void Init(IThrowableItem src) {
        _source = src;
        //if (_rt == null) {
        //    _rt = GetComponent<ThrownItemRuntime>();
        //}
        //_rt.Init(src);
        (_rt ??= GetComponent<ThrownItemRuntime>()).Init(src);
    }

    private void Awake() {
        _mainCamera = Camera.main;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _rt = GetComponent<ThrownItemRuntime>();
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
        if (other.TryGetComponent<IThrownItemReactor>(out var reactor)) { reactor.OnThrownItemPassed(_rt); return; }
        if (other.TryGetComponent<IThrownItemConsumer>(out var consumer)) { consumer.OnHitBy(_rt); OnHit(); return; }
    }

    private void OnHit() {
        if (_animator.isActiveAndEnabled) {
            Hit?.Invoke();
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0;
        }
        else {
            Destroy();
        }
    }

    public void Destroy() => Destroy(gameObject);
}
