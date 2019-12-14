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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using OurGameName.DoMain.Entity.RoleSpace;
using UniRx;

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
        public HexTileMap HexTileMap;
        /// <summary>
        /// 鼠标检测碰撞体
        /// </summary>
        public BoxCollider mouseDetectionCollider;
        public RoleManager RoleManager;

        /// <summary>
        /// 活动的Tile资源索引
        /// </summary>
        private string activeTileAssetName = "忽略";
        /// <summary>
        /// 笔刷大小
        /// </summary>
        private int brushSize = 0;
        /// <summary>
        /// 笔刷大小文本框
        /// </summary>
        public TextMeshProUGUI txtBrushSzieNumber;
        public TMP_Dropdown dropdownTileAsset;
        /// <summary>
        /// 生成地图按钮
        /// </summary>
        public Button btnMake;

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
            btnMake.interactable = false;
            dropdownTileAsset.interactable = false;
        }
        Vector2Int m_vector2Error;
        void Start()
        {
            m_vector2Error = Vector2Int.zero.Error();
            Distance = new Vector2Int[2]
            {
                m_vector2Error,
                m_vector2Error
            };

        }

        void Update()
        {
            DrawPrevuewCell();
        }


        private Vector2Int[] Distance;
        void OnMouseUpAsButton()
        {
            if (Input.GetMouseButtonUp(0) == false || EventSystem.current.IsPointerOverGameObject() == true) 
            {
                return;
            }
            if (Input.GetMouseButtonUp(0) == true)
            {

                Vector3 MouseWorldPosition = GetMouseWorldPosition();
                if (IsMouseWorldPositionError(MouseWorldPosition) == true) return;
                Vector2Int cellPosition = tilemapBackground.WorldToCell(MouseWorldPosition).ToVector2Int();

                EditCell(cellPosition);
                if (hexCellDebugTxtCanvas.TxtShowMode == HexCellTxtCanvas.TxtShowModeEnum.ShowDistance)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (Distance[i] == m_vector2Error)
                        {
                            Distance[i] = cellPosition;
                            break;
                        }
                    }
                    if (Distance[0] != m_vector2Error && Distance[1] != m_vector2Error)
                    {
                        CalculateDistance(Distance[0], Distance[1]);
                        Distance[0] = m_vector2Error;
                        Distance[1] = m_vector2Error;
                    }
                }
            }
        }

        void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 28;
            GUI.Label(new Rect(300, 100, 400, 50), $"Distance0:{Distance[0]} Distance1:{Distance[1]}", style);
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
            m_HexGrid = new HexGrid(args, GameDataCentre.TerrainThroughCostDict, GameDataCentre, HexTileMap);
            m_HexGrid.RefresAll();
            hexCellDebugTxtCanvas.BuildHexPosTxt(m_HexGrid.HexCells.GetLength(0), m_HexGrid.HexCells.GetLength(1));
            marginMeshTilemap.ResetSize(args.MapSize);
            RoleManager.hexGrid = m_HexGrid;

        }

        /// <summary>
        /// 从当前的Tielmap中重新初始化HexGrid
        /// </summary>
        public void ReIntiHexGrid()
        {
            Vector2Int mapSize = HexTileMetrics.GetMapSzie(tilemapBackground);
            HexCell[,] cells = new HexCell[mapSize.x, mapSize.y];
            m_HexGrid = new HexGrid(
                hexCells: cells,
                gameAssetData: GameDataCentre,
                terrainThroughCostDict: GameDataCentre.TerrainThroughCostDict,
                hexTileMap: HexTileMap);

            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    string assetName;
                    Vector2Int callPosition = new Vector2Int(x, y);
                    try { assetName = tilemapBackground.GetTile(callPosition.ToVector3Int()).name; }
                    catch (NullReferenceException) { assetName = "Void00"; }
                    cells[x, y] = new HexCell(m_HexGrid, assetName, callPosition, HexGrid.CalculateNeighbor(callPosition));
                }
            }
            RoleManager.hexGrid = m_HexGrid;

        }



        /// <summary>
        /// 清空背景地图
        /// </summary>
        public void ClearAllMap()
        {
            tilemapBackground.ClearAllTiles();
        }
        /// <summary>
        /// 刷新全部通过成本文本
        /// </summary>
        internal void ThroughCostRefresh()
        {
            foreach (var cell in m_HexGrid.HexCells)
            {
                ThroughCostRefresh(cell);
            }
        }
        /// <summary>
        /// 刷新输入的 cell 的通过成本
        /// </summary>
        /// <param name="cell"></param>
        internal void ThroughCostRefresh(HexCell cell)
        {
            hexCellDebugTxtCanvas.SetTxt(cell.CellPosition, cell.ThroughCost.ToString());
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
            if (e.AseetLoadStatus == AseetLoadStatusArgs.LoadStatus.Completed)
            {

                if (e.AseetType == typeof(TileBase) &&
                    e.AseetName == "TileAsset")
                {
                    TileBase tileBase = GameDataCentre.TileAssetDict["border"];

                    marginMeshTilemap.InitHexMeshTileMap(tilemapBackground.size, tileBase);
                    dropdownTileAsset.interactable = true;
                    GameDataCentre.TileAssetDict.Keys.Select(key => key.RemoveNumber()).ToList();
                    List<string> options = new List<string>();
                    foreach (string key in GameDataCentre.TileAssetDict.Keys.Select(key => key.RemoveNumber()))
                    {
                        if (options.Contains(key) == false)
                        {
                            options.Add(key);
                        }
                    }
                    dropdownTileAsset.AddOptions(options);
                }

                if (e.AseetType == typeof(Dictionary<string, int>) &&
                    e.AseetName == "TerrainThroughCostDict")
                {
                    btnMake.interactable = true;

                    ReIntiHexGrid();
                }
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
        /// 显示 range 范围内其他单元格到计算坐标的距离
        /// </summary>
        /// <param name="calculayePosition">计算坐标</param>
        /// <param name="range">计算范围</param>
        private void CalculateDistance(Vector2Int source, Vector2Int destination)
        {
            var resultDict = HexTileMetrics.ShortestPath(m_HexGrid, source, destination);
            hexCellDebugTxtCanvas.Clear();
            int count = 1;
            hexCellDebugTxtCanvas.
                SetTxts(resultDict, resultDict.Select(x => (count += m_HexGrid[x].ThroughCost).ToString()).ToArray());
        }

        /// <summary>
        /// 检查到鼠标左键点击单元格是编辑单元格
        /// </summary>
        private void EditCell(Vector2Int cellPosition)
        {
            TileBase cell = tilemapBackground.GetTile(cellPosition.ToVector3Int());
            if (cell != null)
            {
                EditCell(HexTileMetrics.GetCellInRange(cellPosition,brushSize));
                //Debug.Log($"get cell {cell.name}");
            }
        }

        /// <summary>
        /// 编辑单元格
        /// </summary>
        /// <param name="CellPosition">中心单元格位置</param>
        /// <param name="BrushSzie">笔刷大小</param>
        private void EditCell(Vector2Int[] cells)
        {
            foreach (var cell in cells)
            {
                try
                {
                    SetTile(m_HexGrid.HexCells.GetItem(cell));
                }
                catch (IndexOutOfRangeException)
                {
                    //超出地图范围的节点
                }
            }
        }

        /// <summary>
        /// 编辑单元格
        /// </summary>
        /// <param name="CellPosition">中心单元格位置</param>
        /// <param name="BrushSzie">笔刷大小</param>
        private void EditCell(HexCell[,] cells)
        {
            foreach (var cell in cells)
            {
                SetTile(cell);
            }
        }

        /// <summary>
        /// 设置单元格
        /// </summary>
        /// <param name="CellPosition"></param>
        private void SetTile(HexCell Cell)
        {
            if (activeTileAssetName != "忽略")
            {
                Cell.TileAssetName =
                    GameDataCentre.GetRandomTileAssetDict(activeTileAssetName).name;
            }
            Cell.Refres();
        }

        /// <summary>
        /// 绘制标识网格
        /// </summary>
        private void DrawPrevuewCell()
        {
            if (GameDataCentre.IsAssetLoadCompleted == false) return;
            Vector2Int MouseCellPosition = GetMouseCellPosition();
            if (CurrentUnderCell != MouseCellPosition)
            {
                CurrentUnderCell = MouseCellPosition;
                TileBase tileBase;
                if (activeTileAssetName == "忽略"){
                    tileBase = GameDataCentre.GetRandomTileAssetDict("border");
                }else{
                    tileBase = GameDataCentre.GetRandomTileAssetDict(activeTileAssetName);
                }

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
        public void SetActiveTileAssetIndex(TMP_Dropdown dropdown)
        {
            activeTileAssetName = dropdown.options[dropdown.value].text;
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

        public void Exit()
        {
            Application.Quit();
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
