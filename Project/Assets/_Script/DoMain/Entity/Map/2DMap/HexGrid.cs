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
        /// Tile资源
        /// </summary>
        private Dictionary<string, List<TileBase>> TileMapAseets;

        private void Awake()
        {
            this.GameWorld.InitComplete += async (sender, e) => await this.InitMapAsync();
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