using System;
using UnityEngine;
using OurGameName.DoMain.Attribute;
using UnityEditor.Experimental.GraphView;
using OurGameName.DoMain.Entity.TileHexMap.AStar;

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
        /// A*节点
        /// </summary>
        private AstartNode m_AStarNode;
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
        public HexCell(string tileAssetName, Vector2Int cellPosition, int throughCost)
        {
            m_AStarNode = new AstartNode(cellPosition, throughCost);
            m_tileAssetName = tileAssetName;
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
        public Vector2Int CellPosition { get { return m_AStarNode.CellPosition; } }
        /// <summary>
        /// 通过成本
        /// </summary>
        public int ThroughCost { get { return m_AStarNode.ThroughCost; } }

        public void RefreshTileAsset()
        {

        }
    }
}
