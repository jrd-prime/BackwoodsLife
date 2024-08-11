using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Helpers
{
    public static class RandomCollector
    {
        public static int GetRandom(int min, int max)
        {
            return Random.Range(min, max);
        }
    }
}
