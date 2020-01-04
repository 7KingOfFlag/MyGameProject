using OurGameName.Audio;
using OurGameName.Config;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OurGameName.View.OptionView
{
    internal class OptionUI : MonoBehaviour
    {
        #region 游戏视图上的实例

        /// <summary>
        /// 全局音量滑动条
        /// </summary>
        public Slider GlobalVolume;

        /// <summary>
        /// 音乐音量滑动条
        /// </summary>
        public Slider MusicVolume;

        /// <summary>
        /// 效果音量滑动条
        /// </summary>
        public Slider EffectVolume;

        /// <summary>
        /// 音频控制器
        /// </summary>
        private AudioController audioController;

        /// <summary>
        /// 音频设置页
        /// </summary>
        public CanvasGroup AudioConfigPage;

        /// <summary>
        /// 显示方式
        /// </summary>
        public TMP_Dropdown ScreenMode;

        /// <summary>
        /// 分辨率
        /// </summary>
        public TMP_Dropdown Resolution;

        /// <summary>
        /// 视频设置页
        /// </summary>
        public CanvasGroup VideoConfigPage;

        #endregion 游戏视图上的实例

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
            InitVideoConfig(config.VideoConfig);
            ActiveConfigPage = config.ConfigPageOfExitConfigScreen;
        }

        /// <summary>
        /// 恢复设置
        /// <para>将游戏设置恢复到进入设置界面之前的样子</para>
        /// </summary>
        private void RestoreConfig()
        {
            GameConfig config = GameConfig.Instance;
            config.ConfigPageOfExitConfigScreen = ActiveConfigPage;
            GameConfig.Instance = config;

            RestoreAudioConfig(config.AudioConfig);
            RestoreVideoConfig(config.VideoConfig);
        }

        /// <summary>
        /// 保存并返回原界面 - 确认按钮
        /// </summary>
        public void SaveCofnigAndExit()
        {
            if (IsSetAudioConfig == true) { gameConfig.AudioConfig = GetCurrentAudioCofnig(); }
            if (IsSetVideoConfig == true) { gameConfig.VideoConfig = GetVideoConfig(); }

            gameConfig.ConfigPageOfExitConfigScreen = ActiveConfigPage;
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

        /// <summary>
        /// 当前活动设置页
        /// </summary>
        private ConfigPage activeConfigPage;

        /// <summary>
        /// 当前活动设置页
        /// </summary>
        private ConfigPage ActiveConfigPage
        {
            get
            {
                return activeConfigPage;
            }
            set
            {
                activeConfigPage = value;
                switch (value)
                {
                    case ConfigPage.AudioConfigPage:
                        AudioConfigPage.alpha = 1;
                        VideoConfigPage.alpha = 0;
                        break;

                    case ConfigPage.VideoConfigPage:
                        AudioConfigPage.alpha = 0;
                        VideoConfigPage.alpha = 1;
                        break;

                    default:
                        AudioConfigPage.alpha = 1;
                        VideoConfigPage.alpha = 0;
                        break;
                }
            }
        }

        public enum ConfigPage
        {
            /// <summary>
            /// 音频设置页
            /// </summary>
            AudioConfigPage,

            /// <summary>
            /// 视频设置页
            /// </summary>
            VideoConfigPage
        }

        #region 音频设置

        /// <summary>
        /// 显示音频设置页
        /// </summary>
        public void ShowAudioPage()
        {
            ActiveConfigPage = ConfigPage.AudioConfigPage;
        }

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
            GlobalVolume.value = audioConfig.GlobalVolume;
            MusicVolume.value = audioConfig.MusicVolume;
            EffectVolume.value = audioConfig.EffectVolume;
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

        #region 视频设置

        /// <summary>
        /// 显示视频设置页
        /// </summary>
        public void ShowVideoConfigPage()
        {
            ActiveConfigPage = ConfigPage.VideoConfigPage;
        }

        /// <summary>
        /// 是否设置过视频设置
        /// </summary>
        private bool IsSetVideoConfig { get; set; }

        /// <summary>
        /// 初始化视频设置
        /// </summary>
        /// <param name="config">视频设置</param>
        private void InitVideoConfig(VideoConfig config)
        {
            IsSetVideoConfig = false;

            ScreenMode.value = (int)config.ScreenMode;
            var AllResolution = Screen.resolutions;
            List<string> resolutionOptions = new List<string>();
            foreach (var item in AllResolution)
            {
                resolutionOptions.Add(item.ToString());
            }
            Resolution.AddOptions(resolutionOptions);
            int currentResolutionIndex = resolutionOptions.IndexOf(config.Resolution.ToString());
            if (currentResolutionIndex != -1)
            {
                Resolution.value = currentResolutionIndex;
            }
            else
            {
                currentResolutionIndex = resolutionOptions.IndexOf(Screen.currentResolution.ToString());
                if (currentResolutionIndex != -1)
                {
                    Resolution.value = currentResolutionIndex;
                }
                else
                {
                    Resolution.value = 0;
                }
            }
        }

        /// <summary>
        /// 恢复视频设置
        /// </summary>
        /// <param name="config"></param>
        public void RestoreVideoConfig(VideoConfig config)
        {
            if (IsSetAudioConfig == false) return;
            Screen.SetResolution(config.Resolution.width, config.Resolution.height, config.ScreenMode, config.Resolution.refreshRate);
        }

        /// <summary>
        /// 获取当前视频设置
        /// <para>即修改后的视频设置</para>
        /// </summary>
        /// <returns></returns>
        public VideoConfig GetVideoConfig()
        {
            return new VideoConfig()
            {
                Resolution = Screen.currentResolution,
                ScreenMode = Screen.fullScreenMode
            };
        }

        /// <summary>
        /// 设置显示方式
        /// </summary>
        /// <param name="drop"></param>
        public void SetScreenMode(TMP_Dropdown drop)
        {
            Screen.fullScreenMode = (FullScreenMode)drop.value;
            IsSetVideoConfig = true;
        }

        /// <summary>
        /// 设置刷新率
        /// </summary>
        /// <param name="drop"></param>
        public void SetResolution(TMP_Dropdown drop)
        {
            try
            {
                var resolution = VideoConfig.ToResolution(drop.options[drop.value].text);
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);
                IsSetVideoConfig = true;
            }
            catch (ArgumentException e)
            {
                Debug.LogError($"设置分辨率非法:{e.Message} ");
                return;
            }
        }

        #endregion 视频设置
    }
}