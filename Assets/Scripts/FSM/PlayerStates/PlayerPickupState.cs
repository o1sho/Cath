using UnityEngine;

public class PlayerPickupState : PlayerStateBase {
    public PlayerPickupState(Player player) : base(player) { }

    private float _pickupTimeLeft;

    private readonly float _pickupRadius = 0.5f;

    public override void Enter() {
        Debug.Log("Player entered Pickup state");
        _pickupTimeLeft = 1f;

        _player.Movement.SetMovementMode(PlayerMovementHandler.MovementMode.Frozen);
        _player.Movement.Stop();

        _visual.SetPickupAnimation(true);

        TryPickupItem();
    }

    public override void Update(float deltaTime) {
        _pickupTimeLeft -= deltaTime;
        if (_pickupTimeLeft <= 0) {
            _player.ChangeState(_player.Movement.InputVector.magnitude > 0 ? _player.MovingState : _player.IdleState);
        }
    }

    public override void Exit() {
        _player.Movement.ResetToInputControl();

        _visual.SetPickupAnimation(false);
    }

    private void TryPickupItem() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_player.transform.position, _pickupRadius);
        foreach (var collider in colliders) {
            if (collider.TryGetComponent<ItemPickup>(out var itemPickup)) {
                Debug.Log("Attempting to pick up item");
                itemPickup.Pickup(_player.Throw);
                return;
            }
        }
        Debug.Log("No item found to pick up");
    }
}
