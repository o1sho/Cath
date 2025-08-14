using UnityEngine;

[RequireComponent(typeof(ThrownItemRuntime))]
public class ThrownItemVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireFX;
    [SerializeField] private Animator animator;

    private ThrownItem _ti;
    private ThrownItemRuntime _rt;

    private void Awake() {
        _ti = GetComponent<ThrownItem>();
        _rt = GetComponent<ThrownItemRuntime>();
    }

    private void OnEnable() {
        _ti.Hit += OnHit;
        _rt.ModifierAdded += OnModifierAdded;
    }

    private void OnDisable() {
        _ti.Hit -= OnHit;
        _rt.ModifierAdded -= OnModifierAdded;
    }

    private void OnHit() {
        animator?.SetTrigger("isDestroy");
    }

    private void OnModifierAdded(ThrownModifier m) {
        if (m == ThrownModifier.Fire) {
            fireFX?.Play();
        }
    }
}
