using UnityEngine;

public class NPCPushReactionHandler : MonoBehaviour, INPCComponent
{
    private NPC _npc;

    public enum PushAxes {
        X,
        Y,
        XY
    }
    public enum NPCWeight {
        Heavy,
        Light
    }

    [SerializeField] private PushAxes availablePushAxes;
    [SerializeField] private NPCWeight weight;

    private Rigidbody2D _rigidbody;

    public NPCWeight Weight => weight;

    //---------------
    public void Init(NPC npc) {
        _npc = npc;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
    }
    //---------------

    private void Start() {
        _rigidbody.gravityScale = 0f;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.linearDamping = 100;
        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

        AdjustingAxes();

    }

    private void Update() {
        if (weight == NPCWeight.Heavy) {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (weight == NPCWeight.Light) {
            AdjustingAxes();
        }
    }

    private void AdjustingAxes() {
        switch (availablePushAxes) {
            case PushAxes.X:
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                _rigidbody.freezeRotation = true;
                break;
            case PushAxes.Y:
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                _rigidbody.freezeRotation = true;
                break;
            case PushAxes.XY:
                _rigidbody.freezeRotation = true;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Throwable")) {
            weight = NPCWeight.Light;
        }
    }

    public void SetHeavyWeight() => weight = NPCWeight.Heavy;
    public void SetLightWeight() => weight = NPCWeight.Light;
}
