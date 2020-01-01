using OurGameName.Config;
using UnityEngine;

namespace OurGameName.Audio
{
    internal class AudioController : MonoBehaviour
    {
        /// <summary>
        /// 背景音乐
        /// </summary>
        public AudioSource MusicAudio;

        private void Awake()
        {
            GameConfig gameConfig = GameConfig.Instance;
            AudioListener.volume = gameConfig.AudioConfig.GlobalVolume;
            MusicAudio.volume = gameConfig.AudioConfig.MusicVolume;
        }

        /// <summary>
        /// 音乐音量
        /// </summary>
        public float MusicVolume
        {
            get { return MusicAudio.volume; }
            set { MusicAudio.volume = value; }
        }

        public float GlobalVolume
        {
            get { return AudioListener.volume; }
            set { AudioListener.volume = value; }
        }
    }
}