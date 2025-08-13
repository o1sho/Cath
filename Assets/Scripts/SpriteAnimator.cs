using UnityEngine;


// ������� �������� ����� ����� ��������, ������� �������� ������ ����� ������ ����� �������.
// ������� �� ������ � SpriteRenderer, ������� ����� � FPS.
// ���� ����� "����-����".

[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public class SpriteAnimator : MonoBehaviour {
    [Header("����� ��������")]
    [SerializeField] private Sprite[] frames;

    [Header("���������")]
    [Tooltip("������ � �������.")]
    [SerializeField, Min(0.1f)] private float fps = 6f;

    [Tooltip("�������� ����-�������: 0,1,2,1,0...")]
    [SerializeField] private bool pingPong = true;

    [Tooltip("�������� � ���������� ����� � ��������, ����� ���������� ������� �� ������ ���������.")]
    [SerializeField] private bool randomizeStart = true;

    [Tooltip("������������ �� ����������� ������ (���� �� ����� � �������� ���������� ������).")]
    [SerializeField] private bool useUnscaledTime = false;

    [Tooltip("������������� ��������, ����� ������ ��� �����.")]
    [SerializeField] private bool animateOnlyWhenVisible = true;

    private SpriteRenderer _sr;
    private float _time;
    private int _lastFrameIndex = -1;
    private bool _isVisible;

    private void Awake() {
        _sr = GetComponent<SpriteRenderer>();

        if (frames != null && frames.Length > 0 && _sr.sprite == null)
            _sr.sprite = frames[0];
    }

    private void OnEnable() {
        if (randomizeStart && frames != null && frames.Length > 0 && fps > 0f) {
            int cycleFrames = 1;
            if (frames.Length == 1)
                cycleFrames = 1;
            else if (pingPong)
                cycleFrames = frames.Length * 2 - 2;
            else
                cycleFrames = frames.Length;

            float cycleDuration = cycleFrames / Mathf.Max(0.0001f, fps);
            _time = Random.value * cycleDuration;
        }
    }

    private void Update() {
        if (frames == null || frames.Length == 0 || fps <= 0f)
            return;

        if (animateOnlyWhenVisible && !_isVisible)
            return;

        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        _time += dt;

        int index = CalculateFrameIndex(_time);

        if (index != _lastFrameIndex) {
            _lastFrameIndex = index;
            _sr.sprite = frames[index];
        }
    }

    private int CalculateFrameIndex(float time) {
        if (frames.Length == 1)
            return 0;

        if (pingPong) {
            int period = frames.Length * 2 - 2;               // ������: 4 ����� => ������ 6: 0 1 2 3 2 1
            int t = Mathf.FloorToInt(time * fps) % period;
            return t < frames.Length ? t : period - t;
        }
        else {
            return Mathf.FloorToInt(time * fps) % frames.Length;
        }
    }

    // ��� �������� �����������, ����� ������ ��������/������� �� ��������� ����� ������.
    private void OnBecameVisible() { _isVisible = true; }
    private void OnBecameInvisible() { _isVisible = false; }

#if UNITY_EDITOR
    private void OnValidate() {
        if (fps < 0.1f) fps = 0.1f;
        if (_sr == null) _sr = GetComponent<SpriteRenderer>();
        if (_sr != null && (frames != null && frames.Length > 0)) {
            if (_sr.sprite == null)
                _sr.sprite = frames[0];
        }
    }
#endif
}

