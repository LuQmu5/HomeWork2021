using UnityEngine;

public static class RaycastUtility
{
    public static bool IsObjectBlockingWithBoxCast(Transform[] checkPoints, Vector3 boxSize, LayerMask blockLayer, float maxDistance)
    {
        bool isBlocked = false;

        foreach (Transform point in checkPoints)
        {
            if (point != null)
            {
                // Проверка BoxCast с использованием точки и направления
                RaycastHit hit;
                if (Physics.BoxCast(point.position, boxSize / 2, -point.up, out hit, Quaternion.identity, maxDistance, blockLayer))
                {
                    isBlocked = true; // Объект найден
                }
            }
        }

        return isBlocked;
    }
}
