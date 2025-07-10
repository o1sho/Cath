using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private string groundTag;

    private bool _isGround = true;

    public bool IsGround => _isGround;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(groundTag)) {
            _isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(groundTag)) {
            _isGround = false;
        }
    }

}
