namespace OurGameName.DoMain.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Script.Serialization;
    using OurGameName.DoMain.Attribute;
    using OurGameName.General.Extension;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Tilemaps;
    using Random = System.Random;

    /// <summary>
    /// 游戏数据中心
    /// </summary>
    internal class GameAssetDataHelper : MonoBehaviour
    {
        public AssetLabelReference TileAssetLabel;

        private Random random;

        /// <summary>
        /// Tile通过费用字典Json文件的地址
        /// </summary>
        private string terrainThroughCostDictJsonFilePath;

        /// <summary>
        /// 资源加载发生变化事件
        /// </summary>
        public event EventHandler<AseetLoadStatusArgs> AseetLoadStatusChang;

        /// <summary>
        /// Tiel资源是否全部加载完成
        /// </summary>
        public bool IsAssetLoadCompleted { get; private set; }

        /// <summary>
        /// Tile通过费用字典
        /// </summary>
        public Dictionary<string, int> TerrainThroughCostDict { get; private set; }

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
            var tileAsset = this.TileAssetDict.
                Where(item => Regex.IsMatch(item.Key, assetName)).
                Select(item => item.Value).ToArray();
            return tileAsset[this.random.Next(0, tileAsset.Count() - 1)];
        }

        /// <summary>
        /// 当资源载入完成时向数据中心报告并分发该信息
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnAseetLoadStatusChang(AseetLoadStatusArgs e)
        {
            e.Raise(this, ref AseetLoadStatusChang);
        }

        /// <summary>
        /// Tile单个资源加载完成
        /// </summary>
        /// <param name="aseet"></param>
        public void TileAseetLoadCompleted(TileBase aseet)
        {
            this.TileAssetDict.Add(aseet.name, aseet);
        }

        private void Awake()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
            this.terrainThroughCostDictJsonFilePath = Application.dataPath + @"/Data/Save/TileThroughCostDict.Json";
            this.TileAssetLoadInit();
        }

        private Dictionary<string, int> LoadTileThrougCostDictOnJson(string jsonFilePath)
        {
            string json = FileHelper.ReadFileToString(jsonFilePath);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, int>>(json);
        }

        private void Start()
        {
            this.TerrainThroughCostDict = this.LoadTileThrougCostDictOnJson(this.terrainThroughCostDictJsonFilePath);
            this.OnAseetLoadStatusChang(
                new AseetLoadStatusArgs(typeof(Dictionary<string, int>), "TerrainThroughCostDict", AseetLoadStatusArgs.LoadStatus.Completed));
        }

        /// <summary>
        /// Tiel全部资源加载完成
        /// </summary>
        /// <param name="obj"></param>
        private void TileAssetAllLoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<TileBase>> obj)
        {
            this.IsAssetLoadCompleted = true;
            //Debug.Log($"tileAsset load completed");
            this.OnAseetLoadStatusChang(
                new AseetLoadStatusArgs(typeof(TileBase), "TileAsset", AseetLoadStatusArgs.LoadStatus.Completed));
            //TileThroughCostDictInit(TileAssetDict);
        }

        /// <summary>
        /// Tile资源加载初始化
        /// </summary>
        private void TileAssetLoadInit()
        {
            this.IsAssetLoadCompleted = false;
            this.TileAssetDict = new Dictionary<string, TileBase>();
            Addressables.LoadAssetsAsync<TileBase>(this.TileAssetLabel, this.TileAseetLoadCompleted).
                Completed += this.TileAssetAllLoadCompleted;
        }

        /// <summary>
        /// Tile通过费用字典初始化
        /// </summary>
        private void TileThroughCostDictInit(Dictionary<string, TileBase> tileAssetDict)
        {
            if (this.TerrainThroughCostDict == null)
            {
                this.TerrainThroughCostDict = new Dictionary<string, int>();
            }
            else
            {
                this.TerrainThroughCostDict.Clear();
            }

            foreach (var key in tileAssetDict.Keys)
            {
                string patternCost2 = @"Forest|Hills|SnouField|Palms|Dunes|Highland|Swamp|Woodlands";
                string patternCost5 = @"Mountain|MesaLarge|Volcano";

                if (Regex.IsMatch(key, pattern: patternCost2) == true)
                {
                    this.TerrainThroughCostDict.Add(key, 2);
                }
                else if (Regex.IsMatch(key, pattern: patternCost5) == true)
                {
                    this.TerrainThroughCostDict.Add(key, 5);
                }
                else
                {
                    this.TerrainThroughCostDict.Add(key, 1);
                }
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(this.TerrainThroughCostDict);
            FileHelper.SaveStringToFile(this.terrainThroughCostDictJsonFilePath, json);
        }
    }
}