using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// ������������ ������� PolygonCollider2D � Editor.
/// ������ �� ������ � PolygonCollider2D. ������ ������ Gizmos � Scene/Game.
/// </summary>
[ExecuteAlways]
[DisallowMultipleComponent]
[RequireComponent(typeof(PolygonCollider2D))]
[AddComponentMenu("Gizmos/Polygon Collider 2D Gizmo")]
public class PolygonCollider2DGizmo : MonoBehaviour {
    [Header("���")]
    public Color lineColor = new Color(0f, 1f, 0f, 0.95f);
    [Min(0f)] public float lineThickness = 2f;

    [Header("�����")]
    [Tooltip("���� �������� � ������ �������� ������ ����� ������ �������.")]
    public bool onlyWhenSelected = false;

    private PolygonCollider2D _poly;

    private void Awake() {
        if (_poly == null) _poly = GetComponent<PolygonCollider2D>();
    }

    private void OnValidate() {
        if (_poly == null) _poly = GetComponent<PolygonCollider2D>();
        if (lineThickness < 0f) lineThickness = 0f;
    }

    private void OnDrawGizmos() {
        if (onlyWhenSelected) return;
        DrawCollider();
    }

    private void OnDrawGizmosSelected() {
        if (!onlyWhenSelected) return;
        DrawCollider();
    }

    private void DrawCollider() {
        if (_poly == null || !_poly.enabled || _poly.pathCount == 0) return;

        Transform t = _poly.transform;
        Gizmos.color = lineColor;

#if UNITY_EDITOR
        // ������ �������� ����������� ������� � Scene/Game, ���� �������� Gizmos
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
        Handles.color = lineColor;
#endif

        for (int p = 0; p < _poly.pathCount; p++) {
            var path = _poly.GetPath(p); // ��������� �����
            int n = path.Length;
            if (n < 2) continue;

#if UNITY_EDITOR
            // ������� ��������� ������ � ������� �����������
            Vector3[] world = new Vector3[n + 1];
            for (int i = 0; i < n; i++)
                world[i] = t.TransformPoint(path[i]);
            world[n] = world[0];

            // ������� �������� ����� Handles.* (� ����� ��� ���������� #if UNITY_EDITOR)
#if UNITY_2020_1_OR_NEWER
            Handles.DrawAAPolyLine(lineThickness, world);
#else
            Handles.DrawAAPolyLine(world);
#endif
#else
            // ������� �� ������� Gizmos-����� (��� �������)
            Vector3 prev = t.TransformPoint(path[0]);
            for (int i = 1; i < n; i++)
            {
                Vector3 cur = t.TransformPoint(path[i]);
                Gizmos.DrawLine(prev, cur);
                prev = cur;
            }
            Gizmos.DrawLine(prev, t.TransformPoint(path[0]));
#endif
        }
    }
}
