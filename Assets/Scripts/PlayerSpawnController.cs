using Unity.Cinemachine;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] private float _timeToRespawn = 100f;

    [SerializeField] private PolygonCollider2D _currentMapBoundary;
    private CinemachineConfiner2D _confiner;

    private static float _respawnTimer;
    private static PlayerSpawnController _lastActiveSpawn;

    private void Awake() {
        _confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Player.Instance.CurrentSpawnPoint = transform.position;
            _confiner.BoundingShape2D = _currentMapBoundary;
            _lastActiveSpawn = this;
            _respawnTimer = _timeToRespawn;
            Debug.Log($"New player spawn position: {Player.Instance.CurrentSpawnPoint}");
        }
    }

    private void Update() {
        if (GameInput.Instance.InputSystem.Player.Respawn.WasPressedThisFrame()) {
            Player.Instance.Respawn();
        }

        if (_lastActiveSpawn == this) {
            _respawnTimer -= Time.deltaTime;
            Debug.Log($"Respawn timer: {_respawnTimer} for spawn at {this.name}");

            if (_respawnTimer <= 0) {
                Player.Instance.Respawn();
                _respawnTimer = _timeToRespawn;
            }
        }
    }

    public static int GetRespawnTimeInSeconds() {
        return Mathf.CeilToInt(_respawnTimer);
    }
}
