using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    internal class InfoTileMap : MonoBehaviour
    {
        /// <summary>
        /// 信息地图
        /// </summary>
        private Tilemap tilemapInfo;

        public AssetReference PreviewCellAsset;
        private TileBase PreviewCellPrefab = null;

        void Awake()
        {
            tilemapInfo = GetComponent<Tilemap>();
            PreviewCellAsset.LoadAssetAsync<TileBase>().Completed += InfoTileMap_Completed;
        }

        private void InfoTileMap_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<TileBase> obj)
        {
            PreviewCellPrefab = obj.Result;
        }

        /// <summary>
        /// 在cells数组中的坐标上绘制标识网格
        /// </summary>
        /// <param name="cells"></param>
        public void DrawPreviewCell(Vector3Int[] cells)
        {
            if (cells == null || PreviewCellPrefab == null)
            {
                return;
            }
            tilemapInfo.ClearAllTiles();

            foreach (var cellPostiton in cells)
            {
                tilemapInfo.SetTile(cellPostiton, PreviewCellPrefab);
            }
            tilemapInfo.RefreshAllTiles();
        }
    }
}
