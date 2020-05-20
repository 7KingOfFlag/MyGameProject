namespace OurGameName.DoMain.Data
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using OurGameName.General.Extension;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Tilemaps;

    /// <summary>
    /// Tile资源载入器
    /// </summary>
    internal class TileAseetLoader : MonoBehaviour
    {
        /// <summary>
        /// TileMap资源标签
        /// </summary>
        public AssetLabelReference TileAseetLabel;

        /// <summary>
        /// 异步载入资源
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, List<TileBase>>> LoadAssetAsync()
        {
            var load = Addressables.LoadAssetsAsync<TileBase>(this.TileAseetLabel, null);
            var assertDict = new Dictionary<string, List<TileBase>>();

            var loadResult = await load.Task;

            string assertName = string.Empty;
            loadResult.ForEach(x =>
            {
                assertName = this.RemoveAssertNumber(x.name);
                if (assertDict.ContainsKey(assertName) == false)
                {
                    assertDict[assertName] = new List<TileBase>();
                }
                assertDict[assertName].Add(x);
            });

            return assertDict;
        }

        /// <summary>
        /// 移除资源名字后面的数字
        /// </summary>
        /// <param name="name">资源名字</param>
        /// <returns>hexBase02 => Base</returns>
        private string RemoveAssertNumber(string name)
        {
            string temp = Regex.Replace(name, @"hex", "");
            return Regex.Replace(temp, @"[0-9]{2}", "");
        }
    }
}