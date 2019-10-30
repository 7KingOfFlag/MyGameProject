using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using OurGameName.DoMain.Data;
using OurGameName.DoMain.Entity.HexMap;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// 六边形tile地图编辑器
    /// </summary>
    internal class HexTileMapEditor : MonoBehaviour
    {
        public GameObject InputController;

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
        public GameData GameDataCentre;
        /// <summary>
        /// 六边形节点表面画布
        /// </summary>
        public HexCellDebugTxtCanvas hexCellDebugTxtCanvas;
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
        private Vector3Int CurrentUnderCell = Vector3Int.zero;

        #region Unity实体方法
        void Awake()
        {
            mainCamera = Camera.main;
            savePath = Application.dataPath + "/Data/Save/Save1.json";
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
            EditCell();
        }
        #endregion

        #region UI操作

        #endregion

        #region 地图操作
        private string savePath;

        /// <summary>
        /// 保存地图数据
        /// </summary>
        public void SaveMapData()
        {
            Vector3Int mapsize = tilemapBackground.size;
            HexTileMapData data = new HexTileMapData(mapsize.x, mapsize.y);
            for (int x = 0; x < mapsize.x; x++)
            {
                for (int y = 0; y < mapsize.y; y++)
                {
                    var cellPostiton = new Vector3Int(x, y, 0);
                    if (tilemapBackground.HasTile(cellPostiton) == true)
                    {
                        data.HexTileCells[x + y * mapsize.y] =
                            new HexTileCell(tilemapBackground.GetTile(cellPostiton).name, cellPostiton);
                    }
                }
            }
            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "//Data//Save");
            }
            data.SaveToFile(savePath);
        }

        /// <summary>
        /// 创建地图
        /// </summary>
        /// <param name="data">地图shuju</param>
        public void CreateMap(HexTileMapData data)
        {
            ClearTilemapBackground();

            for (int i = 0; i < data.HexTileCells.Length; i++)
            {
                SetTile(data.HexTileCells[i]);
            }
            MouseDetectionColliderInit();
        }

        /// <summary>
        /// 载入地图
        /// </summary>
        public void LoadMap()
        {
            var data = LoadMapData(savePath);
            CreateMap(data);
        }

        /// <summary>
        /// 载入地图数据
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        private HexTileMapData LoadMapData(string dataPath)
        {
            HexTileMapData data = HexTileMapData.LoadForFile(dataPath);
            return data;
        }

        /// <summary>
        /// 清空背景地图
        /// </summary>
        public void ClearTilemapBackground()
        {
            tilemapBackground.ClearAllTiles();
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
        public Vector3Int GetMouseCellPosition()
        {
            Vector3 MouseWorldPosition = GetMouseWorldPosition();
            if (IsMouseWorldPositionError(MouseWorldPosition) == false)
            {
                return tilemapBackground.WorldToCell(MouseWorldPosition);
            }
            else
            {
                return new Vector3Int(0, 0, -1);
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

        /// <summary>
        /// 检查到鼠标左键点击单元格是编辑单元格
        /// </summary>
        private void EditCell()
        {
            if (Input.GetMouseButtonUp(0) == true)
            {
                Vector3 MouseWorldPosition = GetMouseWorldPosition();
                if (IsMouseWorldPositionError(MouseWorldPosition) == false)
                {
                    Vector3Int cellPosition = tilemapBackground.WorldToCell(MouseWorldPosition);
                    TileBase cell = tilemapBackground.GetTile(cellPosition);
                    if (cell != null)
                    {
                        EditCell(cellPosition, brushSize);
                        //Debug.Log($"get cell {cell.name}");
                    }
                }
            }
        }

        /// <summary>
        /// 编辑单元格
        /// </summary>
        /// <param name="CellPosition">中心单元格位置</param>
        /// <param name="BrushSzie">笔刷大小</param>
        private void EditCell(Vector3Int CellPosition, int BrushSzie)
        {
            Vector3Int[] brushEfectArrary = HexTileMetrics.GetCellInRange(CellPosition, BrushSzie);
            foreach (var item in brushEfectArrary)
            {
                SetTile(item);
            }
        }

        /// <summary>
        /// 设置并刷新单元格
        /// </summary>
        /// <param name="CellPosition"></param>
        private void SetTile(Vector3Int CellPosition)
        {
            if (CellPosition.x<0||CellPosition.y<0)
            {
                Debug.Log("以屏蔽超出正界限的单元格设置");
                return;
            }
            SetTileAsset(CellPosition,activeTileAssetName);
            tilemapBackground.RefreshTile(CellPosition);
        }
        /// <summary>
        /// 设置并刷新单元格
        /// </summary>
        /// <param name="CellPosition"></param>
        private void SetTile(HexTileCell cell)
        {
            if (cell == null)
            {
                //Debug.Log("Cell is null");
                return;
            }
            if (cell.CeellPosition.x < 0 || cell.CeellPosition.y < 0)
            {
                Debug.Log("以屏蔽超出正界限的单元格设置");
                return;
            }
            SetTileAsset(cell.CeellPosition, cell.TileAssetName);
            tilemapBackground.RefreshTile(cell.CeellPosition);
        }

        /// <summary>
        /// 绘制标识网格
        /// </summary>
        private void DrawPrevuewCell()
        {
            Vector3Int MouseCellPosition = GetMouseCellPosition();
            if (CurrentUnderCell != MouseCellPosition)
            {
                CurrentUnderCell = MouseCellPosition;
                TileBase tileBase = GameDataCentre.TileAssetDate.GetTileBaseAsset("border");
                if (tileBase == null)
                {
                    Debug.LogWarning("TileAsset border didn't load");
                    return;
                }
                infoTileMap.DrawHexMeshTileCells(HexTileMetrics.GetCellInRange(CurrentUnderCell, brushSize), tileBase);
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
        private void SetTileAsset(Vector3Int postiton,string TileAssetName)
        {
            if (TileAssetName == "" || GameDataCentre.TileAssetDate.IsAssetLoadCompleted == false)
            {
                return;
            }

            var asset = GameDataCentre.TileAssetDate.GetTileBaseAsset(TileAssetName);
            if (asset != null)
            {
                tilemapBackground.SetTile(postiton, asset);
            }
            else
            {
                tilemapBackground.SetTile(postiton, GameDataCentre.TileAssetDate.GetTileBaseAsset("Null"));
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
