using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Extensions.Helpers
{
    public static class PlayerPrefsHelper
    {
        // TODO refactor
        public static string GetOrSetPlayerId(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                Debug.Log("GetOrSetPlayerId() - player id already exists. " + PlayerPrefs.GetString(key) +
                          " - player id");
                return PlayerPrefs.GetString(key);
            }

            var value = ChangeNameHelper.GenerateGuid();
            Debug.Log("GetOrSetPlayerId() - new player id: " + value);
            PlayerPrefs.SetString(key, value);
            return value;
        }
    }
}
