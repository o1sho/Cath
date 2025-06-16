using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _nextMapBoundary;
    private CinemachineConfiner2D _confiner;

    [SerializeField] private Direction _direction;
    [SerializeField] private float _offsetPlayrPosition = 1.85f;
    private enum Direction { Up, Down, Left, Right};

    private void Awake() {
        _confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            _confiner.BoundingShape2D = _nextMapBoundary;
            UpdatePlayerPosition();
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
