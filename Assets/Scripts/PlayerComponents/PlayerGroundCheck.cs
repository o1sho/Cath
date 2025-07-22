using System;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public static PlayerGroundCheck Instance { get; private set; }

    [SerializeField] private string groundTag;
    [SerializeField] private string groundForRidingTag;

    private bool _isGround = true;
    private bool _isGroundForRiding = false;
    private Transform _currentRideTarget;

    public bool IsGround => _isGround;
    public bool IsGroundForRiding => _isGroundForRiding;
    public Transform CurrentRideTarget => _currentRideTarget;

    private void Awake() {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(groundTag))
            _isGround = true;

        if (collision.CompareTag(groundForRidingTag)) {
            _isGroundForRiding = true;
            _currentRideTarget = collision.transform;
            Player.Instance.Movement.AttachToPlatform(_currentRideTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(groundTag))
            _isGround = false;

        if (collision.CompareTag(groundForRidingTag)) {
            _isGroundForRiding = false;
            _currentRideTarget = null;
            Player.Instance.Movement.DetachFromPlatform();
        }
    }

}
