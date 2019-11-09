using System;
using System.Net;
using System.Linq;
using OurGameName.DoMain.Data;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.TileHexMap.UI
{
    /// <summary>
    /// 六边形tile地图编辑器
    /// </summary>
    internal class HexTileMapEditor : MonoBehaviour
    {
        public GameObject InputController;

        /// <summary>
        /// 六边形地图
        /// </summary>
        private HexGrid m_HexGrid;

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
        /// 主摄像头
        /// </summary>
        Camera mainCamera;
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
        /// <summary>
        /// 鼠标检测碰撞体
        /// </summary>
        public BoxCollider mouseDetectionCollider;

        /// <summary>
        /// 活动的Tile资源索引
        /// </summary>
        private string activeTileAssetName = "";
        /// <summary>
        /// 笔刷大小
        /// </summary>
        private int brushSize = 0;
        /// <summary>
        /// 笔刷大小文本框
        /// </summary>
        public TextMeshProUGUI txtBrushSzieNumber;

        /// <summary>
        /// 当前鼠标下面的单元格的位置信息
        /// </summary>
        private Vector2Int CurrentUnderCell = Vector2Int.zero;


        public AssetReference HexMapMakeBoxAsset;

        #region Unity实体方法
        void Awake()
        {
            mainCamera = Camera.main;
            GameDataCentre.AseetLoadStatusChang += GameDataCentre_AseetLoadStatusChang;
            MouseDetectionColliderInit();
        }

        void Start()
        {
            var mapSize = tilemapBackground.size;
            hexCellDebugTxtCanvas.BuildHexPosTxt(mapSize.x, mapSize.y);
            marginMeshTilemap.enabled = false;
        }

        void Update()
        {
            DrawPrevuewCell();
        }

        void OnMouseUpAsButton()
        {
            if (Input.GetMouseButtonUp(0) != true)
            {
                return;
            }
            Vector3 MouseWorldPosition = GetMouseWorldPosition();
            if (IsMouseWorldPositionError(MouseWorldPosition) == true)
            {
                return;
            }
            Vector2Int cellPosition = tilemapBackground.WorldToCell(MouseWorldPosition).ToVector2Int();

            EditCell(cellPosition);
            if (hexCellDebugTxtCanvas.TxtShowMode == HexCellTxtCanvas.TxtShowModeEnum.ShowDistance)
            {
                CalculateDistance(cellPosition);
            }
        }
        #endregion

        #region UI操作

        #endregion

        #region 地图操作
        /// <summary>
        /// 显示地图创建界面
        /// </summary>
        public void ShowHexMapMakeBox()
        {
            HexMapMakeBoxAsset.InstantiateAsync(parent: EditorCanvas.transform ).Completed += HexMapMakeBoxAsset_Completed;
        }

        private void HexMapMakeBoxAsset_Completed(AsyncOperationHandle<GameObject> obj)
        {
            var makeBox = obj.Result.GetComponent<HexMapMakeBox>();
            makeBox.context = this;
            var rect = obj.Result.GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 根据地图创建参数 新建地图
        /// </summary>
        /// <param name="args">地图创建参数</param>
        public void CreateHexMap(HexMapCreateArgs args)
        {
            ClearAllMap();
            Debug.Log($"x:{args.MapSize.x} y:{args.MapSize.y}");
            m_HexGrid = new HexGrid(args);
            Refresh();
            hexCellDebugTxtCanvas.BuildHexPosTxt(m_HexGrid.HexCells.GetLength(0), m_HexGrid.HexCells.GetLength(1));
            marginMeshTilemap.ResetSize(args.MapSize);
        }

        /// <summary>
        /// 清空背景地图
        /// </summary>
        public void ClearAllMap()
        {
            tilemapBackground.ClearAllTiles();
        }
        #endregion

        #region 刷新

        private void Refresh()
        {

            for (int x = 0; x < m_HexGrid.HexCells.GetLength(0); x++)
            {
                for (int y = 0; y < m_HexGrid.HexCells.GetLength(1); y++)
                {
                    SetTile(m_HexGrid.HexCells[x, y]);
                }
            }
        }
        #endregion

        #region 网格地图
        private void GameDataCentre_AseetLoadStatusChang(object sender, AseetLoadStatusArgs e)
        {
            if (e.AseetType == typeof(TileBase) &&
                e.AseetName == "TileAsset" &&
                e.AseetLoadStatus == AseetLoadStatusArgs.LoadStatus.Completed)
            {
                TileBase tileBase = GameDataCentre.TileAssetDate.GetTileBaseAsset("border");
                marginMeshTilemap.InitHexMeshTileMap(tilemapBackground.size, tileBase);
            }
        }
        public void SetEnabledOfMarginMeshTileMap(Toggle toggle)
        {
            marginMeshTilemap.enabled = toggle.isOn;
        }
        #endregion

        #region 鼠标位置获取与判断

        /// <summary>
        /// 鼠标世界位置是否处于异常值
        /// </summary>
        /// <param name="mouseWorldPosition"></param>
        /// <returns></returns>
        private bool IsMouseWorldPositionError(Vector3 mouseWorldPosition)
        {
            return mouseWorldPosition == Vector3.back;
        }

        /// <summary>
        /// 获取鼠标当前的世界坐标
        /// <para>基于当前脚本所在对象的碰撞体</para>
        /// <para>鼠标位置异常时返回（0,0,-1）</para>
        /// </summary>
        /// <returns></returns>
        public Vector2Int GetMouseCellPosition()
        {
            Vector3 MouseWorldPosition = GetMouseWorldPosition();
            if (IsMouseWorldPositionError(MouseWorldPosition) == false)
            {
                return tilemapBackground.WorldToCell(MouseWorldPosition).ToVector2Int();
            }
            else
            {
                return new Vector2Int().Error();
            }
        }

        /// <summary>
        /// 获取鼠标当前的世界坐标
        /// <para>基于当前脚本所在对象的碰撞体</para>
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) == true)
            {
                return hit.point;
            }
            //如果鼠标不在碰撞体之内则返回Vector3.back (0,0,-1) 因为碰撞体碰撞体 z = 0 所以不会有 z = -1 的情况
            return Vector3.back;
        }
        #endregion

        /// <summary>
        /// 检查到鼠标左键点击单元格是编辑单元格
        /// </summary>
        private void EditCell(Vector2Int cellPosition)
        {
            TileBase cell = tilemapBackground.GetTile(cellPosition.ToVector3Int());
            if (cell != null)
            {
                EditCell(cellPosition, brushSize);
                //Debug.Log($"get cell {cell.name}");
            }
        }

        /// <summary>
        /// 计算其他单元格到当前坐标的距离
        /// </summary>
        /// <param name="CalculayePosition"></param>
        private void CalculateDistance(Vector2Int calculayePosition)
        {
            Vector2Int[] claculatTrget = HexTileMetrics.GetCellInRange(calculayePosition, brushSize);
            string[] result = new string[claculatTrget.Length];
            for (int i = 0; i < claculatTrget.Length; i++)
            {
                result[i] = HexTileMetrics.CalculateDistance(calculayePosition, claculatTrget[i]).ToString();
            }
            hexCellDebugTxtCanvas.Clear();
            claculatTrget = m_HexGrid.FiltrationOutOfGridRangePosition(claculatTrget);
            hexCellDebugTxtCanvas.SetTxts(claculatTrget, result);
        }

        /// <summary>
        /// 编辑单元格
        /// </summary>
        /// <param name="CellPosition">中心单元格位置</param>
        /// <param name="BrushSzie">笔刷大小</param>
        private void EditCell(Vector2Int CellPosition, int BrushSzie)
        {
            Vector2Int[] brushEfectArrary = HexTileMetrics.GetCellInRange(CellPosition, BrushSzie);
            foreach (var item in brushEfectArrary)
            {
                SetTile(item);
            }
        }

        /// <summary>
        /// 设置并刷新单元格
        /// </summary>
        /// <param name="CellPosition"></param>
        private void SetTile(Vector2Int CellPosition)
        {
            if (m_HexGrid.IsPositionOutOfGridRange(CellPosition))
            {
                //Debug.Log("以屏蔽超出正界限的单元格设置");
                return;
            }
            SetTileAsset(CellPosition,activeTileAssetName);
            tilemapBackground.RefreshTile(CellPosition.ToVector3Int());
        }
        /// <summary>
        /// 设置并刷新单元格
        /// </summary>
        /// <param name="CellPosition"></param>
        private void SetTile(HexCell cell)
        {

            if (cell == null || m_HexGrid.IsPositionOutOfGridRange(cell.CellPosition) == true)
            {
                return;
            }
            SetTileAsset(cell.CellPosition, cell.TileAssetName);
            tilemapBackground.RefreshTile(cell.CellPosition.ToVector3Int());
        }

        /// <summary>
        /// 绘制标识网格
        /// </summary>
        private void DrawPrevuewCell()
        {
            Vector2Int MouseCellPosition = GetMouseCellPosition();
            if (CurrentUnderCell != MouseCellPosition)
            {
                CurrentUnderCell = MouseCellPosition;
                TileBase tileBase = GameDataCentre.TileAssetDate.GetTileBaseAsset("border");
                if (tileBase == null)
                {
                    Debug.LogWarning("TileAsset border didn't load");
                    return;
                }
                infoTileMap.DrawHexMeshTileCells(HexTileMetrics.GetCellInRange(CurrentUnderCell, brushSize).ToVector3Int(), tileBase);
            }
        }

        /// <summary>
        /// 设置活动Tile资源索引
        /// </summary>
        /// <param name="value"></param>
        public void SetActiveTileAssetIndex(string value)
        {
            activeTileAssetName = value;
        }
        /// <summary>
        /// 设置笔刷大小
        /// </summary>
        /// <param name="value"></param>
        public void SetBrushSize(Slider slider)
        {
            brushSize = (int)slider.value;
            txtBrushSzieNumber.SetText(((int)slider.value).ToString());
        }

        /// <summary>
        /// 设置指定坐标的tile资源
        /// </summary>
        /// <param name="postiton"></param>
        private void SetTileAsset(Vector2Int postiton,string TileAssetName)
        {
            if (TileAssetName == "" || GameDataCentre.TileAssetDate.IsAssetLoadCompleted == false)
            {
                return;
            }

            var asset = GameDataCentre.TileAssetDate.GetTileBaseAsset(TileAssetName);
            Vector3Int m_position = postiton.ToVector3Int();
            if (asset != null)
            {
                tilemapBackground.SetTile(m_position, asset);
            }
            else
            {
                tilemapBackground.SetTile(m_position, GameDataCentre.TileAssetDate.GetTileBaseAsset("Null"));
            }
        }

        /// <summary>
        /// 根据当前Tilemap大小设置用鼠标检查碰撞体
        /// </summary>
        private void MouseDetectionColliderInit()
        {
            if (mouseDetectionCollider != null)
            {
                Vector3 mapSzie = tilemapBackground.size;
                mouseDetectionCollider.size = new Vector3(mapSzie.x, mapSzie.y, 0f);
                mouseDetectionCollider.center = new Vector3(mapSzie.x / 2, mapSzie.y / 2, 0f);
            }
            else
            {
                Debug.LogError("mouseDetectionCollider is Null");
            }
        }

        /// <summary>
        /// 设置触发器
        /// </summary>
        enum OptionToggle
        {
            /// <summary>
            /// 忽略
            /// </summary>
            Ignore,
            /// <summary>
            /// 是
            /// </summary>
            Yes,
            /// <summary>
            /// 否
            /// </summary>
            No
        }
    }
}
