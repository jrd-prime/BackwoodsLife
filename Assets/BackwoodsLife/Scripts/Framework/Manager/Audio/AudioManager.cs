using UnityEngine;

namespace BackwoodsLife.Scripts.Framework.Manager.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        public string Description => "AudioManager";
        public void ServiceInitialization()
        {
        }


        private void Awake()
        {
            // Debug.LogWarning("I'm here! AudioManager");
        }
    }
}