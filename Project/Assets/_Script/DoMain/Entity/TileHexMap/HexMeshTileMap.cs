using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    internal class HexMeshTileMap : MonoBehaviour
    {
        private Tilemap m_meshTilemap;
        
        void Awake()
        {
            m_meshTilemap = GetComponent<Tilemap>();
            enabled = true;
        }

        /// <summary>
        /// 初始化网格地图
        /// </summary>
        /// <param name="mapSize">地图大小</param>
        /// <param name="tileAsset">Tile资源</param>
        public void InitHexMeshTileMap(Vector3Int mapSize, TileBase tileAsset)
        {
            m_meshTilemap.ClearAllTiles();

            if (m_meshTilemap == null)
            {
                Debug.LogError("marginMeshTilemap is null");
                return;
            }
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    m_meshTilemap.SetTile(new Vector3Int(x, y, 0), tileAsset);
                }
            }
            m_meshTilemap.RefreshAllTiles();
        }

        /// <summary>
        /// 在cells数组中的坐标上绘制标识网格
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="tileAsset">要绘制的tile素材</param>
        public void DrawHexMeshTileCells(Vector3Int[] cells, TileBase tileAsset)
        {
            if (cells == null || tileAsset == null)
            {
                Debug.LogWarning("cells == null or TileAsset == null");
                return;
            }
            m_meshTilemap.ClearAllTiles();

            foreach (var cellPostiton in cells)
            {
                m_meshTilemap.SetTile(cellPostiton, tileAsset);
            }
            m_meshTilemap.RefreshAllTiles();
        }

        private bool m_enabled = false;
        public new bool enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                if (value == m_enabled)
                {
                    return;
                }
                m_enabled = value;

                var color = m_meshTilemap.color;
                if (value == true)
                {
                    color.a = 1f;
                }
                else
                {
                    color.a = 0f;
                }

                m_meshTilemap.color = color;
            }
        }
    }
    
}
