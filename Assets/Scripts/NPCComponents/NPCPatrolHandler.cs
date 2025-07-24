using UnityEngine;
using System.Collections;

public class NPCPatrolHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;

    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] private float minDistance = 0.1f;
    [SerializeField] private float waitMaxTimeAtPoint = 1.5f;
    [SerializeField] private bool isDisappear = false;

    private int _currentPointIndex = 0;
    private bool _isWaiting = false;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //---------------

    public void Patrol(float deltaTime) {
        if (_isWaiting || patrolPoints.Length == 0) return;

        Vector2 targetPosition = patrolPoints[_currentPointIndex].position;
        Vector2 moveDirection = (targetPosition - _rigidbody.position).normalized;

        _rigidbody.linearVelocity = moveDirection * patrolSpeed;

        if (Vector2.Distance(_rigidbody.position, targetPosition) < minDistance) {
            StartCoroutine(WaitAtPoint());
        }
    }

    private void Update() {
        if (_rigidbody.linearVelocity.y < 0) _spriteRenderer.flipX = true;
        if (_rigidbody.linearVelocity.y > 0) _spriteRenderer.flipX = false;
        if (_rigidbody.linearVelocity.x < 0) _spriteRenderer.flipY = true;
        if (_rigidbody.linearVelocity.x > 0) _spriteRenderer.flipY = false;
    }

    private IEnumerator WaitAtPoint() {
        _isWaiting = true;

        _rigidbody.linearVelocity = Vector2.zero;

        if (isDisappear) _spriteRenderer.enabled = false;

        yield return new WaitForSeconds(Random.Range(0, waitMaxTimeAtPoint));

        _currentPointIndex = (_currentPointIndex + 1) % patrolPoints.Length;

        if (isDisappear) _spriteRenderer.enabled = true;

        _isWaiting = false;
    }
}
