using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject uiManagerPrefab;

    private void Awake() {
        if (GameManager.Instance == null) {
            Instantiate(gameManagerPrefab);
        }

        if (UIManager.Instance == null) {
            Instantiate(uiManagerPrefab);
        }

        // ����� ����� ���������������� ������ ��������� (�����, ����������� � �.�.)
    }
}
