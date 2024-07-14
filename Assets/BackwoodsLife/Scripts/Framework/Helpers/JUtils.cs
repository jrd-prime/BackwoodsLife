using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BackwoodsLife.Scripts.Framework.Helpers
{
    public static class JUtils
    {
        /// <summary>
        /// Checks a structure consisting of list fields for the presence of at least one element in any list
        /// </summary>
        public static bool CheckStructWithListsForItems<T>(T structInstance) where T : struct
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(field => field.FieldType.IsGenericType &&
                                field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                .Any(field => field.GetValue(structInstance) is IList { Count: > 0 });
        }
    }
}
