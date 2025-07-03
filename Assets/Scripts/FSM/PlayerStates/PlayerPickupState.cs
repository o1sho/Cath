using UnityEngine;

public class PlayerPickupState : IState
{
    private readonly Player _player;
    private readonly PlayerVisual _visual;

    private float _pickupTimeLeft;

    private readonly float _pickupRadius = 0.5f;

    public PlayerPickupState(Player player) {
        _player = player;
        _visual = Player.Instance.GetComponentInChildren<PlayerVisual>();
        _pickupTimeLeft = 1f;
    }

    public void Enter() {
        _player.Movement.Stop();

        _visual.SetPickupAnimation(true);

        TryPickupItem();

        Debug.Log("Player entered Pickup state");
    }

    public void Update(float deltaTime) {
        _player.Movement.Stop();

        _pickupTimeLeft -= deltaTime;
        if (_pickupTimeLeft <= 0) {
            _player.ChangeState(_player.Movement.InputVector.magnitude > 0 ? new PlayerMovingState(_player) : new PlayerIdleState(_player));
        }
    }

    public void Exit() {
        _player.Movement.Stop();

        _visual.SetPickupAnimation(false);
    }

    private void TryPickupItem() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_player.transform.position, _pickupRadius);
        foreach (var collider in colliders) {
            if (collider.TryGetComponent<ItemPickup>(out var itemPickup)) {
                Debug.Log("Attempting to pick up item");
                itemPickup.Pickup(_player.Throw);
                return; // Подбираем только один предмет
            }
        }
        Debug.Log("No item found to pick up");
    }
}
