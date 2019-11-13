using System;
using UnityEngine;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    internal class HexCell
    {
        /// <summary>
        /// 单元格所属的六边形网格
        /// </summary>
        private HexGrid m_HexGrid;
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
        private Vector2Int m_position;
        private int m_distance;
        private int m_throughCost;
        /// <summary>
        /// 邻近的单元格位置
        /// </summary>
        [SerializeField]
        private Vector2Int[] m_neighborsPosition;

        public HexCell(HexGrid hexGrid, string tileAssetName, Vector2Int cellPosition, Vector2Int[] neighborsPosition)
        {
            m_HexGrid = hexGrid;
            m_position = cellPosition;
            m_neighborsPosition = neighborsPosition;
            TileAssetName = tileAssetName;
        }

        /// <summary>
        /// Tile单元格资源名称 
        /// <para>赋值时会附带将通行成本修改为符合Tile资源的值</para>
        /// </summary>
        public string TileAssetName
        {
            get
            {
                return m_tileAssetName;
            }
            set
            {
                if (value  == m_tileAssetName)
                {
                    return;
                }
                m_tileAssetName = value;
                NeedRefres |= NeedRefresCode.Asset;
                try
                {
                    ThroughCost = m_HexGrid.TerrainThroughCostDict[m_tileAssetName];
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e.Message);
                    ThroughCost = 1;

                }
            }
        }
        /// <summary>
        /// 相邻单元格位置数组 
        /// <para>以 HexDirection 枚举对应值所代表的方向排序</para>
        /// </summary>
        public Vector2Int[] NeighborsPosition
        {
            get
            {
                return m_neighborsPosition;
            }
            private set
            {
                m_neighborsPosition = value;
            }
        }

        /// <summary>
        /// 单元格位置
        /// </summary>
        public Vector2Int CellPosition { get { return m_position; } }
        /// <summary>
        /// 通过成本
        /// </summary>
        public int ThroughCost
        {
            get { return m_throughCost; }
            private set
            {
                if (value == m_throughCost)
                {
                    return;
                }
                m_throughCost = value;
                NeedRefres |= NeedRefresCode.ThrougCost;
            }
        }

        /// <summary>
        /// 表明单元格各个组件是否需要刷新的位标志
        /// </summary>
        public NeedRefresCode NeedRefres { get; private set; }

        public void Refres()
        {
            m_HexGrid.Refres(this);
        }

        [Flags]
        public enum NeedRefresCode
        {
            None        = 0x0000,
            Asset       = 0x0001,
            ThrougCost  = 0x0002,
        }
    }
}
