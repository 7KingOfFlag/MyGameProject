using OurGameName.Audio;
using OurGameName.Config;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OurGameName.View.OptionView
{
    internal class OptionUI : MonoBehaviour
    {
        /// <summary>
        /// 全局音量滑动条
        /// </summary>
        public Slider GlobalVolumeSlider;

        /// <summary>
        /// 音乐音量滑动条
        /// </summary>
        public Slider MusicVolumeSlider;

        /// <summary>
        /// 效果音量滑动条
        /// </summary>
        public Slider EffectVolumeSlider;

        /// <summary>
        /// 音频控制器
        /// </summary>
        private AudioController audioController;

        /// <summary>
        /// 游戏设置
        /// </summary>
        private GameConfig gameConfig;

        private void OnEnable()
        {
            audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
            gameConfig = GameConfig.Instance;
            InitConfig(gameConfig);
        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        /// <param name="config"></param>
        private void InitConfig(GameConfig config)
        {
            InitAudioConfig(config.AudioConfig);
        }

        /// <summary>
        /// 恢复设置
        /// <para>将游戏设置恢复到进入设置界面之前的样子</para>
        /// </summary>
        private void RestoreConfig()
        {
            GameConfig config = GameConfig.Instance;
            RestoreAudioConfig(config.AudioConfig);
        }

        /// <summary>
        /// 保存并返回原界面 - 确认按钮
        /// </summary>
        public void SaveCofnigAndExit()
        {
            if (IsSetAudioConfig == true) { gameConfig.AudioConfig = GetCurrentAudioCofnig(); }
            GameConfig.Instance = gameConfig;
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// 返回并不保存游戏设置 - 退出按钮
        /// <para>退出设置界面并将游戏设置恢复到设置之前的样子</para>
        /// </summary>
        public void ExitAndDontSave()
        {
            RestoreConfig();
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        #region 音频设置

        /// <summary>
        /// 是否设置过音频设置
        /// </summary>
        private bool IsSetAudioConfig { get; set; }

        /// <summary>
        /// 初始化音频设置
        /// </summary>
        /// <param name="audioConfig"></param>
        private void InitAudioConfig(AudioConfig audioConfig)
        {
            IsSetAudioConfig = false;
            GlobalVolumeSlider.value = audioConfig.GlobalVolume;
            MusicVolumeSlider.value = audioConfig.MusicVolume;
            EffectVolumeSlider.value = audioConfig.EffectVolume;
        }

        /// <summary>
        /// 恢复音频设置
        /// </summary>
        /// <param name="audioConfig"></param>
        private void RestoreAudioConfig(AudioConfig audioConfig)
        {
            if (IsSetAudioConfig == false) return;
            audioController.MusicVolume = audioConfig.MusicVolume;
            audioController.GlobalVolume = audioConfig.GlobalVolume;
        }

        /// <summary>
        /// 获取当前音频设置
        /// <para>即修改后的音频设置</para>
        /// </summary>
        /// <returns></returns>
        private AudioConfig GetCurrentAudioCofnig()
        {
            return new AudioConfig()
            {
                GlobalVolume = audioController.GlobalVolume,
                MusicVolume = audioController.MusicVolume,
            };
        }

        /// <summary>
        /// 设置背景音量
        /// </summary>
        /// <param name="slider"></param>
        public void SetMusicVolume(Slider slider)
        {
            audioController.MusicVolume = slider.value;
            IsSetAudioConfig = true;
        }

        /// <summary>
        /// 设置总音量
        /// </summary>
        /// <param name="slider"></param>
        public void SetGlobalVolume(Slider slider)
        {
            audioController.GlobalVolume = slider.value;
            IsSetAudioConfig = true;
        }

        #endregion 音频设置
    }
}