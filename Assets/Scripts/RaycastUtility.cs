using UnityEngine;

public static class RaycastUtility
{
    // Проверка столкновения с объектом по лучу, направленному вниз, с учетом слоя и максимальной длины
    public static bool IsObjectBlocking(Vector3 origin, Vector3 direction, LayerMask blockLayer, float maxDistance)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance, blockLayer))
        {
            // Если луч попал в объект, возвращаем true
            return true;
        }
        return false;
    }
}
