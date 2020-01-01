﻿using OurGameName.DoMain.Attribute;
using System;
using System.IO;
using System.Web.Script.Serialization;
using UnityEngine;
using JsonSerializer = System.Web.Script.Serialization.JavaScriptSerializer;

namespace OurGameName.Config
{
    /// <summary>
    /// 游戏设置
    /// </summary>
    internal class GameConfig
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private GameConfig() { }

        /// <summary>
        /// 获取游戏设置实例
        /// </summary>
        /// <returns>设置单例</returns>
        public static GameConfig Instance
        {
            get
            {
                string configFilePath = Environment.CurrentDirectory + "/" + configFileName;
                if (File.Exists(configFilePath) == true)
                {
                    return DeSerializaConfig(configFilePath);
                }
                else
                {
                    GameConfig result = InitGameConfig();
                    SerializaConfig(result, configFilePath);
                    return result;
                }
            }
            set
            {
                string configFilePath = Environment.CurrentDirectory + "/" + configFileName;
                SerializaConfig(value, configFilePath);
            }
        }

        /// <summary>
        /// 地址设置
        /// </summary>
        public PathConfig PathConfig { get; set; }

        /// <summary>
        /// 音频设置
        /// </summary>
        public AudioConfig AudioConfig { get; set; }

        /// <summary>
        /// 视频设置
        /// </summary>
        public VideoConfig VideoConfig { get; set; }

        #region 序列化方法

        /// <summary>
        /// 游戏设置文件名
        /// </summary>
        [ScriptIgnore]
        private const string configFileName = "Config.json";

        /// <summary>
        /// 反序列化游戏设置
        /// </summary>
        /// <param name="configFilePath">游戏设置地址</param>
        /// <returns></returns>
        private static GameConfig DeSerializaConfig(string configFilePath)
        {
            string configJson = FileHelper.ReadFileToString(configFilePath);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<GameConfig>(configJson);
        }

        /// <summary>
        /// 初始化游戏设置
        /// <para>未找到设置文件与重置设置时使用</para>
        /// </summary>
        /// <returns></returns>
        private static GameConfig InitGameConfig()
        {
            GameConfig result = new GameConfig()
            {
                PathConfig = new PathConfig()
                {
                    ArchivePath = Environment.CurrentDirectory + "/Save/",
                }
            };
            return result;
        }

        /// <summary>
        /// 序列化游戏设置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="configFilePath"></param>
        private static void SerializaConfig(GameConfig config, string configFilePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            string configJson = serializer.Serialize(config);
            FileHelper.SaveStringToFile(configFilePath, configJson);
        }

        #endregion 序列化方法
    }

    /// <summary>
    /// 地址设置
    /// </summary>
    internal struct PathConfig
    {
        /// <summary>
        /// 存档地址
        /// </summary>
        public string ArchivePath { get; set; }
    }

    /// <summary>
    /// 音频设置
    /// </summary>
    internal struct AudioConfig
    {
        /// <summary>
        /// 总音量
        /// </summary>
        public float GlobalVolume { get; set; }

        /// <summary>
        /// 音乐音量
        /// </summary>
        public float MusicVolume { get; set; }

        /// <summary>
        /// 效果音量
        /// </summary>
        public float EffectVolume { get; set; }

        /// <summary>
        /// 背景音量
        /// </summary>
        public float BackgroundVolume { get; set; }
    }

    /// <summary>
    /// 视频设置
    /// </summary>
    internal struct VideoConfig
    {
        /// <summary>
        /// 显示模式
        /// </summary>
        public FullScreenMode ScreenMode { get; set; }

        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int Width { get; set; }
    }
}