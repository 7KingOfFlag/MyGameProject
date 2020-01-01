using OurGameName.DoMain.Attribute;
using OurGameName.DoMain.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using static OurGameName.DoMain.Entity.TileHexMap.HexCell;

namespace OurGameName.DoMain.Entity.TileHexMap.UI
{
    internal class HexTileMap : MonoBehaviour
    {
        /// <summary>
        /// 背景地图
        /// </summary>
        public Tilemap tilemapBackground;

        /// <summary>
        /// 网格地图
        /// </summary>
        public HexMeshTileMap marginMeshTilemap;

        /// <summary>
        /// 信息地图
        /// </summary>
        public HexMeshTileMap infoTileMap;

        /// <summary>
        /// 游戏数据中心
        /// </summary>
        public GameAssetDataHelper GameDataCentre;

        /// <summary>
        /// 编辑器所在的画布
        /// </summary>
        public Canvas EditorCanvas;

        /// <summary>
        /// 六边形单元格表面画布
        /// </summary>
        public HexCellTxtCanvas hexCellDebugTxtCanvas;

        private void Awake()
        {
        }

        private void Start()
        {
            var mapSize = HexTileMetrics.GetMapSzie(tilemapBackground);
            hexCellDebugTxtCanvas.BuildHexPosTxt(mapSize.x, mapSize.y);
            marginMeshTilemap.enabled = false;
        }

        #region 资源设置

        public void Refresh(HexCell cell)
        {
            if ((cell.NeedRefres & NeedRefresCode.Asset) == NeedRefresCode.Asset)
            {
                SetTielAsset(cell);
            }
            if ((cell.NeedRefres & NeedRefresCode.ThrougCost) == NeedRefresCode.ThrougCost &&
                hexCellDebugTxtCanvas.TxtShowMode == HexCellTxtCanvas.TxtShowModeEnum.ShowThroughCost)
            {
                hexCellDebugTxtCanvas.SetTxt(cell.CellPosition, cell.ThroughCost.ToString());
            }
        }

        private void SetTielAsset(HexCell cell)
        {
            var tile = GameDataCentre.TileAssetDict[cell.TileAssetName];
            var position = cell.CellPosition.ToVector3Int();
            tilemapBackground.SetTile(position, tile);
            tilemapBackground.RefreshTile(position);
        }

        #endregion 资源设置
    }
}