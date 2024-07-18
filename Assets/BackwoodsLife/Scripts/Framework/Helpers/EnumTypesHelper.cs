using System;
using System.Collections.Generic;
using BackwoodsLife.Scripts.Data.Common.Enums;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;

namespace BackwoodsLife.Scripts.Framework.Helpers
{
    public static class EnumTypesHelper
    {
        private static readonly List<Type> List = new() { typeof(EResource), typeof(EFood), typeof(ETool) };

        public static List<string> GetNamesForInventory()
        {
            var list = new List<string>();
            foreach (var type in List)
            {
                list.AddRange(Enum.GetNames(type));
            }

            return list;
        }
    }
}
