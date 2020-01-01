using OurGameName.DoMain.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace OurGameName.DoMain.Data
{
    /// <summary>
    /// 游戏数据中心
    /// </summary>
    internal class GameAssetDataHelper : MonoBehaviour
    {
        /// <summary>
        /// 资源加载发生变化事件
        /// </summary>
        public event EventHandler<AseetLoadStatusArgs> AseetLoadStatusChang;

        public AssetLabelReference TileAssetLabel;
        private Random m_random;

        /// <summary>
        /// Tile资源字典
        /// </summary>
        public Dictionary<string, TileBase> TileAssetDict { get; private set; }

        /// <summary>
        /// 随机从字典中符合 assetName 的项中选一项返回
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public TileBase GetRandomTileAssetDict(string assetName)
        {
            var tileAsset = TileAssetDict.
                Where(item => Regex.IsMatch(item.Key, assetName)).
                Select(item => item.Value).ToArray();
            return tileAsset[m_random.Next(0, tileAsset.Count() - 1)];
        }

        /// <summary>
        /// Tile通过费用字典
        /// </summary>
        public Dictionary<string, int> TerrainThroughCostDict { get; private set; }

        /// <summary>
        /// Tile通过费用字典Json文件的地址
        /// </summary>
        private string m_terrainThroughCostDictJsonFilePath;

        /// <summary>
        /// Tiel资源是否全部加载完成
        /// </summary>
        public bool IsAssetLoadCompleted { get; private set; }

        private void Awake()
        {
            m_random = new Random((int)DateTime.Now.Ticks);
            m_terrainThroughCostDictJsonFilePath = Application.dataPath + @"/Data/Save/TileThroughCostDict.Json";
            TileAssetLoadInit();
        }

        private void Start()
        {
            TerrainThroughCostDict = LoadTileThrougCostDictOnJson(m_terrainThroughCostDictJsonFilePath);
            OnAseetLoadStatusChang(
                new AseetLoadStatusArgs(typeof(Dictionary<string, int>), "TerrainThroughCostDict", AseetLoadStatusArgs.LoadStatus.Completed));
        }

        /// <summary>
        /// Tile资源加载初始化
        /// </summary>
        private void TileAssetLoadInit()
        {
            IsAssetLoadCompleted = false;
            TileAssetDict = new Dictionary<string, TileBase>();
            Addressables.LoadAssetsAsync<TileBase>(TileAssetLabel, TileAseetLoadCompleted).
                Completed += TileAssetAllLoadCompleted;
        }

        /// <summary>
        /// Tile单个资源加载完成
        /// </summary>
        /// <param name="aseet"></param>
        public void TileAseetLoadCompleted(TileBase aseet)
        {
            TileAssetDict.Add(aseet.name, aseet);
        }

        /// <summary>
        /// Tiel全部资源加载完成
        /// </summary>
        /// <param name="obj"></param>
        private void TileAssetAllLoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<TileBase>> obj)
        {
            IsAssetLoadCompleted = true;
            //Debug.Log($"tileAsset load completed");
            OnAseetLoadStatusChang(
                new AseetLoadStatusArgs(typeof(TileBase), "TileAsset", AseetLoadStatusArgs.LoadStatus.Completed));
            //TileThroughCostDictInit(TileAssetDict);
        }

        /// <summary>
        /// Tile通过费用字典初始化
        /// </summary>
        private void TileThroughCostDictInit(Dictionary<string, TileBase> tileAssetDict)
        {
            if (TerrainThroughCostDict == null)
            {
                TerrainThroughCostDict = new Dictionary<string, int>();
            }
            else
            {
                TerrainThroughCostDict.Clear();
            }

            foreach (var key in tileAssetDict.Keys)
            {
                string patternCost2 = @"Forest|Hills|SnouField|Palms|Dunes|Highland|Swamp|Woodlands";
                string patternCost5 = @"Mountain|MesaLarge|Volcano";

                if (Regex.IsMatch(key, pattern: patternCost2) == true)
                {
                    TerrainThroughCostDict.Add(key, 2);
                }
                else if (Regex.IsMatch(key, pattern: patternCost5) == true)
                {
                    TerrainThroughCostDict.Add(key, 5);
                }
                else
                {
                    TerrainThroughCostDict.Add(key, 1);
                }
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(TerrainThroughCostDict);
            FileHelper.SaveStringToFile(m_terrainThroughCostDictJsonFilePath, json);
        }

        private Dictionary<string, int> LoadTileThrougCostDictOnJson(string jsonFilePath)
        {
            string json = FileHelper.ReadFileToString(jsonFilePath);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, int>>(json);
        }

        /// <summary>
        /// 当资源载入完成时向数据中心报告并分发该信息
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnAseetLoadStatusChang(AseetLoadStatusArgs e)
        {
            e.Raise(this, ref AseetLoadStatusChang);
        }
    }
}