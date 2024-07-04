using System;

namespace BackwoodsLife.Scripts.Framework.Helpers
{
    public static class ChangeNameHelper
    {
        public static string GenerateGuid() => Guid.NewGuid().ToString();
    }
}