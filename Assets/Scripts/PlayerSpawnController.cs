using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSpawnController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player.Instance.CurrentSpawnPoint = transform.position;
            Debug.Log($"New player spawn position: {Player.Instance.CurrentSpawnPoint}");
        }
    }

    private void Update() {
        if (GameInput.Instance.InputSystem.Player.Respawn.WasPressedThisFrame()) {
            Player.Instance.Respawn();
        }
    }
}
