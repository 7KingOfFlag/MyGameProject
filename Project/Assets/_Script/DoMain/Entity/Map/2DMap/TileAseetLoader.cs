using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace OurGameName.DoMain.Data
{
    internal class TileAseetLoader : MonoBehaviour
    {
        /// <summary>
        /// TileMap资源标签
        /// </summary>
        public AssetLabelReference TileAseetLabel;

        public Task<IList<TileBase>> LoadAssetAsync()
        {
            var load = Addressables.LoadAssetsAsync<TileBase>(this.TileAseetLabel, null);
            return load.Task;
        }
    }
}