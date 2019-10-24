using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using System.Reflection;

namespace OurGameName.DoMain.Data
{
    /// <summary>
    /// tile资源数据
    /// </summary>
    internal class TileAssetDate : MonoBehaviour
    {
        public AssetLabelReference TileAssetLabel;

        private Dictionary<string, TileBase> TileAsset;
        public bool AssetLoadCompleted { get; private set; }

        public TileBase GetTileBaseAsset(string AssetName)
        {
            if (AssetLoadCompleted == false || TileAsset.ContainsKey(AssetName) == false)
            {
                return null;
            }
            else
            {
                return TileAsset[AssetName];
            }
        }

        void Awake()
        {
            AssetLoadCompleted = false;
            TileAsset = new Dictionary<string, TileBase>();
            Addressables.LoadAssetsAsync<TileBase>(TileAssetLabel, null).Completed += TileAssetDate_Completed;
        }

        private void TileAssetDate_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<TileBase>> obj)
        {
            var LoadResult = obj.Result;
            foreach (var item in LoadResult)
            {
                TileAsset.Add(item.name, item);
            }
            //Debug.Log($"tileAsset load completed loadsize {TileAsset.Count}");
            AssetLoadCompleted = true;
        }
    }
}


