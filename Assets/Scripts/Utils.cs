using UnityEngine;

public static class Utils {

    //public static Vector3 GetRandomDirection() {
    //    return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    //}

    public static Vector3 GetRandomDirection() {
        // Выбираем случайную ось (X или Y)
        bool moveAlongX = Random.value > 0.5f;
        // Выбираем направление: 1 или -1
        float direction = Random.value > 0.5f ? 1f : -1f;
        // Возвращаем вектор, где одна координата 0, другая 1 или -1
        return moveAlongX ? new Vector3(direction, 0, 0) : new Vector3(0, direction, 0);
    }
}
