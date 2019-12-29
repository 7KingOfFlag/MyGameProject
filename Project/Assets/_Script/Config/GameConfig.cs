using System;
using System.IO;
using OurGameName.DoMain.Attribute;
using JsonSerializer = System.Web.Script.Serialization.JavaScriptSerializer;
using System.Web.Script.Serialization;

namespace OurGameName.Config
{
    /// <summary>
    /// 游戏设置
    /// </summary>
    internal class GameConfig
    {
        /// <summary>
        /// 游戏设置文件名
        /// </summary>
        [ScriptIgnore]
        private const string configFileName = "Config.json";

        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private GameConfig(){ }

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
                    SerializaConfig(result,configFilePath);
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
                    ArchivePath = Environment.CurrentDirectory + "/Seve/",
                }
            };
            return result;
        }

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
        /// 地址设置
        /// </summary>
        public PathConfig PathConfig { get; set; }
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
}
