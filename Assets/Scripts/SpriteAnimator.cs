using UnityEngine;


// Простая анимация через смену спрайтов, которая работает только когда объект виден камерой.
// Бросить на объект с SpriteRenderer, указать кадры и FPS.
// Есть режим "пинг-понг".

[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public class SpriteAnimator : MonoBehaviour {
    [Header("Кадры анимации")]
    [SerializeField] private Sprite[] frames;

    [Header("Настройки")]
    [Tooltip("Кадров в секунду.")]
    [SerializeField, Min(0.1f)] private float fps = 6f;

    [Tooltip("Анимация туда-обратно: 0,1,2,1,0...")]
    [SerializeField] private bool pingPong = true;

    [Tooltip("Начинать с случайного места в анимации, чтобы одинаковые объекты не мигали синхронно.")]
    [SerializeField] private bool randomizeStart = true;

    [Tooltip("Использовать не скейлящийся таймер (игра на паузе — анимация продолжает тикать).")]
    [SerializeField] private bool useUnscaledTime = false;

    [Tooltip("Останавливать анимацию, когда объект вне кадра.")]
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
            int period = frames.Length * 2 - 2;               // пример: 4 кадра => период 6: 0 1 2 3 2 1
            int t = Mathf.FloorToInt(time * fps) % period;
            return t < frames.Length ? t : period - t;
        }
        else {
            return Mathf.FloorToInt(time * fps) % frames.Length;
        }
    }

    // Эти коллбеки срабатывают, когда объект попадает/выходит из видимости любой камеры.
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

