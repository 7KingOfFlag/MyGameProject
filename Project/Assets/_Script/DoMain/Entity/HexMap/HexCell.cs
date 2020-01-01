using UnityEngine;

namespace OurGameName.DoMain.Entity.HexMap
{
    /// <summary>
    /// 六边形地图单元格
    /// </summary>
    public class HexCell : MonoBehaviour
    {
        /// <summary>
        /// 六边形坐标结构，
        /// </summary>
        public HexCoordinates coordinates;

        /// <summary>
        /// 地块类型索引
        /// </summary>
        private Terrain terrainType;

        /// <summary>
        /// 邻近的单元格
        /// </summary>
        [SerializeField]
        public HexCell[] neighbors;

        /// <summary>
        /// 地形索引
        /// </summary>
        public Terrain TerrainTypeIndex
        {
            get => terrainType;
            set
            {
                if (terrainType != value)
                {
                    terrainType = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 城墙
        /// </summary>
        private bool wall;

        /// <summary>
        /// 城墙 设置单元格上是否出现城墙
        /// </summary>
        public bool Wall
        {
            get
            {
                return wall;
            }
            set
            {
                if (wall != value)
                {
                    wall = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 所属的地图块
        /// </summary>
        public HexCellMesh chunk;

        private void Awake()
        {
            neighbors = new HexCell[6];
        }

        /// <summary>
        /// 获取对应方向的相邻单元格
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public HexCell GetNeighbor(HexDirection dir)
        {
            return neighbors[(int)dir];
        }

        /// <summary>
        /// 设置单元格对应方向的相邻单元格
        /// </summary>
        /// <param name="dir">相邻的单元格方向</param>
        /// <param name="cell">相邻的单元格</param>
        public void SetNeighBor(HexDirection dir, HexCell cell)
        {
            neighbors[(int)dir] = cell;
            cell.neighbors[(int)dir.Opposite()] = this;
        }

        #region 刷新

        /// <summary>
        /// 刷新
        /// 刷新整个单元块
        /// 如果相邻的单元格是属于其他单元块会相邻单元块也会刷新
        /// </summary>
        private void Refresh()
        {
            if (chunk != null)
            {
                chunk.Refresh();
                for (int i = 0; i < neighbors.Length; i++)
                {
                    HexCell neighbor = neighbors[i];
                    if (neighbor != null && neighbor.chunk != chunk)
                    {
                        neighbor.chunk.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// 仅刷新单元块本身
        /// </summary>
        private void RefreshSelfOnly()
        {
            chunk.Refresh();
        }

        #endregion 刷新

        #region 特征物体

        /// <summary>
        /// 城市等级
        /// </summary>
        private int urbanLevel;

        /// <summary>
        /// 城市等级
        /// </summary>
        public int UrbanLevel
        {
            get
            {
                return urbanLevel;
            }
            set
            {
                if (urbanLevel != value)
                {
                    urbanLevel = value;
                    RefreshSelfOnly();
                }
            }
        }

        #endregion 特征物体
    }
}