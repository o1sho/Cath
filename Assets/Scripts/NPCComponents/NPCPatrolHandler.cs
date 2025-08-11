using UnityEngine;
using System.Collections;

public class NPCPatrolHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;

    [Header("Movement Settings")]
    [SerializeField] private float patrolSpeed = 5f;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTimeAtPoint = 1f;
    [SerializeField] private float reachThreshold = 0.1f;

    private int _currentPointIndex = 0;
    private float _waitTimer = 0f;
    private bool _waiting = false;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
    }
    //---------------

    public void ResetPatrol() {
        _currentPointIndex = 0;
        _waitTimer = 0f;
        _waiting = false;
    }

    public void UpdatePatrol(float deltaTime) {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        Vector2 npcPosition = _npc.transform.position;
        Vector2 targetPoint = patrolPoints[_currentPointIndex].position;
        Vector2 toTarget = targetPoint - npcPosition;

        if (_waiting) {
            _npc.Movement.SetOverrideVelocity(Vector2.zero);
            _waitTimer -= deltaTime;

            _npc.Visual.DisplayHandler(false);

            if (_waitTimer <= 0f) {
                _waiting = false;
                _npc.Visual.DisplayHandler(true);
                AdvancePoint();
            }

            return;
        }

        if (toTarget.magnitude <= reachThreshold) {
            _waiting = true;
            _waitTimer = waitTimeAtPoint;
            _npc.Movement.SetOverrideVelocity(Vector2.zero);
        }
        else {
            Vector2 moveDir = toTarget.normalized;
            _npc.Movement.SetOverrideVelocity(moveDir * patrolSpeed);
            _npc.Visual?.SetFacingDirection(moveDir);
        }
    }

    private void AdvancePoint() {
        _currentPointIndex = (_currentPointIndex + 1) % patrolPoints.Length;
    }
}
