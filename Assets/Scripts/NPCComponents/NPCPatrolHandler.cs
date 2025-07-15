using UnityEngine;

public class NPCPatrolHandler : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    private int currentPointIndex = 0;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] private float minDistance = 0.1f;

    private Rigidbody2D _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Patrol(float deltaTime) {
        if (patrolPoints.Length == 0) return;
        Vector2 targetPosition = patrolPoints[currentPointIndex].position;
        Vector2 moveDirection = (targetPosition - _rigidbody.position).normalized;

        _rigidbody.linearVelocity = moveDirection * patrolSpeed;

        if (Vector2.Distance(_rigidbody.position, targetPosition) < minDistance) {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}
