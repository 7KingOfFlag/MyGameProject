using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace OurGameName.DoMain.Data
{
    /// <summary>
    /// tile资源数据
    /// </summary>
    internal class TileAssetData : MonoBehaviour
    {
        public AssetLabelReference TileAssetLabel;
        public AssetLabelReference UiPrefabAssetLabel;
        public GameAssetDataHelper context;

        private Dictionary<string, TileBase> TileAssetDict;

        /// <summary>
        /// 资源已全部加载完成
        /// </summary>
        public bool IsAssetLoadCompleted { get; private set; }

        /// <summary>
        /// 获取对应名字的Tile资源
        /// </summary>
        /// <param name="AssetName"></param>
        /// <returns></returns>
        public TileBase GetTileBaseAsset(string AssetName)
        {
            if (IsAssetLoadCompleted == false || TileAssetDict.ContainsKey(AssetName) == false)
            {
                return null;
            }
            else
            {
                return TileAssetDict[AssetName];
            }
        }

        void Awake()
        {
            IsAssetLoadCompleted = false;
            TileAssetDict = new Dictionary<string, TileBase>();
            Addressables.LoadAssetsAsync<TileBase>(TileAssetLabel, TileAseetCompleted).Completed += TileAssetDate_Completed;
        }

        public void TileAseetCompleted(TileBase aseet)
        {
            TileAssetDict.Add(aseet.name, aseet);
        }

        private void TileAssetDate_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<TileBase>> obj)
        {
            /*
            var LoadResult = obj.Result;
            foreach (var item in LoadResult)
            {
                TileAsset.Add(item.name, item);
            }
            */
            //Debug.Log($"tileAsset load completed loadsize {TileAsset.Count}");
            IsAssetLoadCompleted = true;
            Debug.Log($"tileAsset load completed");
            context.OnAseetLoadStatusChang(
                new AseetLoadStatusArgs(typeof(TileBase), "TileAsset", AseetLoadStatusArgs.LoadStatus.Completed));
        }

    }
}


