using System;
using UnityEngine;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    internal class HexCell
    {
        /// <summary>
        /// 序列化用构造方法
        /// </summary>
        public HexCell() { }
        /// <summary>
        /// 单元格地图资源名
        /// </summary>
        private string m_tileAssetName;
        /// <summary>
        /// 单元格位置
        /// </summary>
        private Vector2Int m_cellPosition;
        private int m_distance;
        /// <summary>
        /// 邻近的单元格位置
        /// </summary>
        [SerializeField]
        private Vector2Int[] m_neighborsPosition;
        public HexCell(string TileAssetName, Vector2Int CellPosition)
        {
            m_tileAssetName = TileAssetName;
            m_cellPosition = CellPosition;
        }

        /// <summary>
        /// Tile单元格资源名称
        /// </summary>
        public string TileAssetName
        {
            get
            {
                return m_tileAssetName;
            }
            set
            {
                m_tileAssetName = value;
                RefreshTileAsset();
            }
        }

        public Vector2Int[] NeighborsPosition
        {
            get
            {
                return m_neighborsPosition;
            }
            set
            {
                m_neighborsPosition = value;
            }
        }


        /// <summary>
        /// 单元格位置
        /// </summary>
        public Vector2Int CellPosition { get { return m_cellPosition; } }

        public void RefreshTileAsset()
        {

        }
    }
}
