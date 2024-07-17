namespace BackwoodsLife.Scripts.Data.Common.Scriptable
{
    public static class SOPathName
    {
        // Names
        public const string MainMenu = "Backwoods Life SO/";
        public const string Config = "Settings/";
        public const string Items = "Items/";
        public const string GameItem = "Game Item/";
        public const string WorldItem = "World Item/";

        // Paths
        public const string ConfigPath = MainMenu + Config;
        public const string ItemsConfigPath = ConfigPath + Items;
        public const string GameItemPath = MainMenu + Items + GameItem;
        public const string WorldItemPath = MainMenu + Items + WorldItem;
    }
}
