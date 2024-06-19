using UnityEngine;

namespace TopDownAdvent.Utils
{
    public static class Utils
    {
        public static Vector3 GetRandomDir()
        // ищем случайное направление для моба
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, -1f)).normalized;
        }

    }
}

