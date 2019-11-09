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
        /// 节点地图资源名
        /// </summary>
        private string m_tileAssetName;
        /// <summary>
        /// 节点位置
        /// </summary>
        private Vector2Int m_cellPosition;
        private int m_distance;
        /// <summary>
        /// 邻近的节点位置
        /// </summary>
        [SerializeField]
        public Vector2Int[] neighborsPosition;
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
            }
        }

        public void SetNeighbor(HexDirection dir, Vector2Int neighbor)
        {
            neighborsPosition[(int)dir] = neighbor;
        }

        /// <summary>
        /// 单元格位置
        /// </summary>
        public Vector2Int CellPosition { get { return m_cellPosition; } }

        public int Distance
        {
            get { return m_distance; }
            set
            {
                m_distance = value;
            }
        }
    }
}
