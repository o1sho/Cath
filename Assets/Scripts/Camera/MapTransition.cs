using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [Header("Confiner")]
    [SerializeField] private PolygonCollider2D _nextMapBoundary;

    [Header("Camera Zoom")]
    [SerializeField] private CinemachineCamera _cmCamera;
    [SerializeField] private bool _changeZoom = false;
    [SerializeField] private float _targetOrthoSize = 5.0f;

    [Header("Player offset")]
    [SerializeField] private Direction _direction;
    [SerializeField] private float _offsetPlayrPosition = 1.85f;
    private enum Direction { Up, Down, Left, Right};

    private CinemachineConfiner2D _confiner;

    private void Awake() {
        _confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (_confiner != null && _nextMapBoundary != null) {
            _confiner.BoundingShape2D = _nextMapBoundary;
        }

        UpdatePlayerPosition();

        if (_changeZoom && _cmCamera != null) {
            var lens = _cmCamera.Lens;
            lens.OrthographicSize = _targetOrthoSize; // для 2D-камеры
            _cmCamera.Lens = lens;
        }
    }

    private void UpdatePlayerPosition() {
        Vector3 newPlayerPosition = Player.Instance.transform.position;

        switch (_direction) {
            case Direction.Up:
                newPlayerPosition.y += _offsetPlayrPosition;
                break;
            case Direction.Down:
                newPlayerPosition.y -= _offsetPlayrPosition;
                break;
            case Direction.Left:
                newPlayerPosition.x -= _offsetPlayrPosition;
                break;
            case Direction.Right:
                newPlayerPosition.x += _offsetPlayrPosition;
                break;
        }

        Player.Instance.transform.position = newPlayerPosition;
    }
}
