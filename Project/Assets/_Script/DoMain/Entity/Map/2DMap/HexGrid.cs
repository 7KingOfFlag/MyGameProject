namespace OurGameName.DoMain.Entity.Map._2DMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OurGameName.DoMain.Data;
    using OurGameName.DoMain.Entity.GameWorld;
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.Entity.Map.Extensions;

    /// <summary>
    /// 六边形地图
    /// </summary>
    internal class HexGrid : MonoBehaviour
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
        /// 主摄像头
        /// </summary>
        private Camera mainCamera;

        /// <summary>
        /// Tile资源
        /// </summary>
        private Dictionary<string, List<TileBase>> TileMapAseets;

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
                return BackgroundTilemap.WorldToCell(MouseWorldPosition).ToVector2Int();
            }
            else
            {
                return new Vector2Int().Error();
            }
        }

        private void Awake()
        {
            mainCamera = Camera.main;

            this.GameWorld.InitComplete += async (sender, e) => await this.InitMapAsync();

            this.InitMapCollider();
        }

        /// <summary>
        /// 获取鼠标当前的世界坐标
        /// <para>基于当前脚本所在对象的碰撞体</para>
        /// </summary>
        /// <returns></returns>
        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
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
        }

        /// <summary>
        /// 初始化地图碰撞体
        /// </summary>
        private void InitMapCollider()
        {
            var mapColliderSize = MapMetrics.GetMapColliderSize(this.GameWorld.MapSzie);
            this.MapCollider.size = new Vector3(mapColliderSize.Height, mapColliderSize.Width, 0.1f);
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
    }
}