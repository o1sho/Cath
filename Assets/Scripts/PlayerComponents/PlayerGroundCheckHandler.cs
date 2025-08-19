using System;
using UnityEngine;

public class PlayerGroundCheckHandler : MonoBehaviour, IPlayerComponent {

    [SerializeField] private string groundTag;
    [SerializeField] private string groundForRidingTag;

    private bool _isGround = true;
    private bool _isGroundForRiding = false;
    private Transform _currentRideTarget;

    private Player _player;

    public bool IsGround => _isGround;
    public bool IsGroundForRiding => _isGroundForRiding;
    public Transform CurrentRideTarget => _currentRideTarget;

    public void Init(Player player) {
        _player = player;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(groundTag))
            _isGround = true;

        if (collision.CompareTag(groundForRidingTag)) {
            _isGroundForRiding = true;
            _currentRideTarget = collision.transform;
            _player.Movement.AttachToPlatform(_currentRideTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(groundTag))
            _isGround = false;

        if (collision.CompareTag(groundForRidingTag)) {
            _isGroundForRiding = false;
            _currentRideTarget = null;
            _player.Movement.DetachFromPlatform();
        }
    }

    public void ResetGroundChecker() {
        _isGround = true;
        _isGroundForRiding = false;
    }
}
