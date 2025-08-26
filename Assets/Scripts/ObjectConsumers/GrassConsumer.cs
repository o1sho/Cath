using UnityEngine;

public class GrassConsumer : MonoBehaviour, IThrownItemConsumer
{
    [SerializeField] private Sprite onHitSparite;

    private SpriteRenderer _spriteRenderer;
    private SpriteAnimator _spriteAnimator;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteAnimator = GetComponent<SpriteAnimator>();
    }

    public HitOutcome OnHitBy(ThrownItemRuntime rt) {

        _spriteRenderer.sprite = onHitSparite;
        _spriteAnimator.enabled = false;

        return HitOutcome.ContinueFlight;
    }
}
