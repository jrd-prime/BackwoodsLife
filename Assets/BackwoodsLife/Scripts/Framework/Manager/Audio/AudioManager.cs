using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        public void ServiceInitialization()
        {
        }

        public string Description => "AudioManager";

        private void Awake()
        {
            Debug.LogWarning("I'm here! AudioManager");
        }
    }
}