using System;
using System.Collections.Generic;
using System.Linq;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.Game;
using BackwoodsLife.Scripts.Data.Common.Enums.Items.World;

namespace BackwoodsLife.Scripts.Data.Common.Enums
{
    public static class EnumHelper
    {
        public static int GameItemCategoryCount = Enum.GetNames(typeof(EGameItem)).Length;
        public static int WorldItemCategoryCount = Enum.GetNames(typeof(EWorldItem)).Length;

        public static List<Type> GameItemEnums = new()
        {
            typeof(EFood),
            typeof(EResource),
            typeof(ESkill),
            typeof(ETool),
            typeof(EWear)
        };

        public static List<Type> WorldItemEnums = new()
        {
            typeof(EBuilding),
            typeof(ECollectable)
        };

        public static int GetGameItemsCount()
        {
            return GameItemEnums.Sum(type => Enum.GetNames(type).Length);
        }

        public static List<string> GetGameItemsNames()
        {
            return GameItemEnums.SelectMany(type => Enum.GetNames(type).ToList()).ToList();
        }

        public static List<string> GetWorldItemsNames()
        {
            return WorldItemEnums.SelectMany(type => Enum.GetNames(type).ToList()).ToList();
        }

        public static int GetWorldItemsCount()
        {
            return WorldItemEnums.Sum(type => Enum.GetNames(type).Length);
        }
    }
}
