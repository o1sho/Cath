using UnityEngine;

public static class Utils {

    //public static Vector3 GetRandomDirection() {
    //    return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    //}

    public static Vector3 GetRandomDirection() {
        // �������� ��������� ��� (X ��� Y)
        bool moveAlongX = Random.value > 0.5f;
        // �������� �����������: 1 ��� -1
        float direction = Random.value > 0.5f ? 1f : -1f;
        // ���������� ������, ��� ���� ���������� 0, ������ 1 ��� -1
        return moveAlongX ? new Vector3(direction, 0, 0) : new Vector3(0, direction, 0);
    }
}
