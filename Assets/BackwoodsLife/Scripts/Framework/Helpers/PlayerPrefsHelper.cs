﻿using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Helpers
{
    public static class PlayerPrefsHelper
    {
        // TODO refactor
        public static string GetOrSetPlayerId(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                Debug.LogWarning("GetOrSetPlayerId() - player id already exists");
                Debug.LogWarning(PlayerPrefs.GetString(key) + " - player id");
                return PlayerPrefs.GetString(key);
            }

            var value = ChangeNameHelper.GenerateGuid();
            Debug.LogWarning("GetOrSetPlayerId() - new player id: " + value);
            PlayerPrefs.SetString(key, value);
            return value;
        }
    }
}