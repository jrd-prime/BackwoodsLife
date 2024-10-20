﻿namespace BackwoodsLife.Scripts.Data.Const
{
    public static class SOPathName
    {
        // Names
        public const string MainMenu = "Backwoods Life SO/";
        public const string Config = "Settings/";
        public const string Items = "Items/";
        public const string GameItem = "Game Item/";
        public const string WorldItem = "World Item/";
        public const string RecipeItem = "Recipe/";

        // Paths
        public const string ConfigPath = MainMenu + Config;
        public const string ItemsConfigPath = ConfigPath + Items;
        public const string GameItemPath = MainMenu + Items + GameItem;
        public const string WorldItemPath = MainMenu + Items + WorldItem;
        public const string RecipeItemPath = MainMenu + Items + RecipeItem;
    }
}
