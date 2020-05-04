namespace OurGameName.DoMain.Map
{
    using OurGameName.DoMain.Map.Args;
    using UnityEngine;
    using Terrain = Args.Terrain;

    /// <summary>
    /// 地图单元
    /// </summary>
    internal class Element
    {
        /// <summary>
        /// 邻近的单元格
        /// </summary>
        [SerializeField]
        public Vector2Int[] neighbors;

        /// <summary>
        /// 地块类型索引
        /// </summary>
        private Terrain terrainType;

        /// <summary>
        /// 地图单元
        /// </summary>
        /// <param name="position">单元位置</param>
        public Element(Vector2Int position)
        {
            Position = position;
            neighbors = CalculateNeighbor(Position);
        }

        /// <summary>
        /// 单元位置
        /// </summary>
        public Vector2Int Position { get; set; }

        /// <summary>
        /// 地形
        /// </summary>
        public Terrain Terrain
        {
            get => terrainType;
            set
            {
                terrainType = value;
            }
        }

        /// <summary>
        /// 获取对应方向的相邻单元格的位置
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Vector2Int GetNeighbor(HexDirection dir)
        {
            return neighbors[(int)dir];
        }

        /// <summary>
        /// 根据中心单元格位置获取中心点相邻单元格的数组
        /// <para>数组中单元格排序依照 HexDirection 枚举中表示的方向的顺序排列</para>
        /// </summary>
        /// <param name="centrePosition">所求的相邻单元格数组的中心单元格位置</param>
        /// <remarks>
        /// 六边形单元格偶数行修正原理
        /// 因为六边形网格是奇数行与偶数行是交错排列的
        /// 所以偶数行会比奇数行落后一格
        /// 这导致在中心点的上下四个相邻的单元格偶数行会比奇数行单元格整体向 x 轴负方向移动一个单位
        /// </remarks>
        private static Vector2Int[] CalculateNeighbor(Vector2Int centrePosition)
        {
            Vector2Int[] result = new Vector2Int[6];
            int correction = 0;
            if (centrePosition.y % 2 == 0) //如果中心点在偶数行 增加修正值
            {
                correction = -1;
            }
            result[(int)HexDirection.NE] = new Vector2Int(centrePosition.x + 1 + correction, centrePosition.y + 1);
            result[(int)HexDirection.E] = new Vector2Int(centrePosition.x + 1, centrePosition.y);
            result[(int)HexDirection.SE] = new Vector2Int(centrePosition.x + 1 + correction, centrePosition.y - 1);
            result[(int)HexDirection.SW] = new Vector2Int(centrePosition.x + correction, centrePosition.y - 1);
            result[(int)HexDirection.W] = new Vector2Int(centrePosition.x - 1, centrePosition.y);
            result[(int)HexDirection.NW] = new Vector2Int(centrePosition.x + correction, centrePosition.y + 1);

            return result;
        }
    }
}