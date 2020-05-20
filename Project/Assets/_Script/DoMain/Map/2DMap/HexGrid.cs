namespace OurGameName.DoMain.Map._2DMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OurGameName.DoMain.Data;
    using OurGameName.DoMain.GameWorld;
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using OurGameName.General.Extension;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.Map.Extensions;
    using UnityEngine.AddressableAssets;
    using OurGameName.Interface;
    using TMPro;

    /// <summary>
    /// 六边形地图
    /// </summary>
    internal class HexGrid : MonoBehaviour, ICoordinateConverter
    {
        /// <summary>
        /// 背景地图
        /// </summary>
        public Tilemap BackgroundTilemap;

        /// <summary>
        /// 游戏世界
        /// </summary>
        public World GameWorld;

        /// <summary>
        /// Tile资源载入器
        /// </summary>
        public TileAseetLoader Loader;

        /// <summary>
        /// 地图碰撞体
        /// </summary>
        public BoxCollider MapCollider;

        /// <summary>
        /// 地图标识点
        /// </summary>
        public Canvas MapMarkPoint;

        /// <summary>
        /// 地图标识点预制体
        /// </summary>
        public AssetReference MapMarkPointReference;

        /// <summary>
        /// 测试地图
        /// </summary>
        public Tilemap TestTileMap;

        /// <summary>
        /// 主摄像头
        /// </summary>
        private Camera mainCamera;

        /// <summary>
        /// Tile资源
        /// </summary>
        private Dictionary<string, List<TileBase>> TileMapAseets;

        /// <summary>
        /// 将地图坐标转化为世界坐标
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        public Vector3 CellToWorld(Vector3Int cellPosition)
        {
            return this.BackgroundTilemap.CellToWorld(cellPosition);
        }

        /// <summary>
        /// 获取鼠标当前的世界坐标
        /// <para>基于当前脚本所在对象的碰撞体</para>
        /// <para>鼠标位置异常时返回（0,0,-1）</para>
        /// </summary>
        /// <returns></returns>
        public Vector2Int GetMouseCellPosition()
        {
            Vector3 MouseWorldPosition = this.GetMouseWorldPosition();
            if (this.IsMouseWorldPositionError(MouseWorldPosition) == false)
            {
                return this.BackgroundTilemap.WorldToCell(MouseWorldPosition).ToVector2Int();
            }
            else
            {
                return new Vector2Int().Error();
            }
        }

        /// <summary>
        /// 设置测试地图
        /// </summary>
        /// <param name="list"></param>
        public void SetTestMap(List<Vector2Int> list)
        {
            this.TestTileMap.ClearAllTiles();
            var tileAseet = this.TileMapAseets["border"].GetRandomItem();
            foreach (var item in list)
            {
                this.TestTileMap.SetTile(item.ToVector3Int(), tileAseet);
            }
        }

        private void Awake()
        {
            this.mainCamera = Camera.main;

            this.GameWorld.InitComplete += async (sender, e) => await this.InitMapAsync();

            this.TestTileMapInit();
        }

        /// <summary>
        /// 获取鼠标当前的世界坐标
        /// <para>基于当前脚本所在对象的碰撞体</para>
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) == true)
            {
                return hit.point;
            }
            //如果鼠标不在碰撞体之内则返回Vector3.back (0,0,-1) 因为碰撞体碰撞体 z = 0 所以不会有 z = -1 的情况
            return Vector3.back;
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        private async Task InitMapAsync()
        {
            this.TileMapAseets = await this.Loader.LoadAssetAsync();
            this.GameWorld.GameMap.ForEach(x => this.SetMap(x));
            this.GameWorld.GameMap.Update += (sender, e) => e.UpdateElement.ForEach(x => this.SetMap(x));

            this.InitMapCollider(this.GameWorld.MapSize);
            this.InitMapMarkPoint(this.GameWorld.MapSize);
        }

        /// <summary>
        /// 初始化地图碰撞体
        /// </summary>
        private void InitMapCollider(Vector2Int mapSize)
        {
            var (ColliderWidth, ColliderHeight) = MapMetrics.GetMapColliderSize(mapSize);
            this.MapCollider.size = new Vector3(ColliderWidth, ColliderHeight, 0.1f);
            this.MapCollider.center = new Vector3(ColliderWidth * 0.5f - 0.5f, ColliderHeight * 0.5f - 0.5f);
        }

        /// <summary>
        /// 初始化地图标识点
        /// </summary>
        /// <param name="mapSize">地图大小</param>
        private void InitMapMarkPoint(Vector2Int mapSize)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    this.SetMapMarkPoint(x, y);
                }
            }
        }

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
        /// 设置地图的指定单元格
        /// </summary>
        /// <param name="item">地图单元</param>
        private void SetMap(Element item)
        {
            string terrainName = item.Terrain.ToString();
            if (this.TileMapAseets.TryGetValue(terrainName, out var tileAseet))
            {
                this.BackgroundTilemap.SetTile(item.Position.ToVector3Int(), tileAseet.GetRandomItem());
            }
            else
            {
                this.BackgroundTilemap.SetTile(item.Position.ToVector3Int(), this.TileMapAseets["Void"].First());
            }
        }

        /// <summary>
        /// 设置地图标识点
        /// </summary>
        private void SetMapMarkPoint(int x, int y)
        {
            var worldSpace = this.CellToWorld(new Vector3Int(x, y, 0));

            this.MapMarkPointReference.InstantiateAsync(worldSpace, Metrics.ZeroQuaternion, this.MapMarkPoint.transform).Completed += obj =>
            {
                TextMeshProUGUI text = obj.Result.GetComponent<TextMeshProUGUI>();
                text.text = $"({x},{y})";
            };
        }

        private void TestTileMapInit()
        {
            this.TestTileMap.ClearAllTiles();
        }
    }
}