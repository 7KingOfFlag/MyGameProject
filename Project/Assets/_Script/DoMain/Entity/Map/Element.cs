using UnityEngine;

namespace OurGameName.DoMain.Entity.Map
{
    /// <summary>
    /// 地图单元
    /// </summary>
    internal class Element
    {
        /// <summary>
        /// 单元位置
        /// </summary>
        public Vector2Int Position { get; set; }

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
            neighbors = new Element[6];
        }

        /// <summary>
        /// 邻近的单元格
        /// </summary>
        [UnityEngine.SerializeField]
        public Element[] neighbors;

        /// <summary>
        /// 地形索引
        /// </summary>
        public Terrain TerrainTypeIndex
        {
            get => terrainType;
            set
            {
                terrainType = value;
            }
        }

        /// <summary>
        /// 获取对应方向的相邻单元格
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public Element GetNeighbor(HexDirection dir)
        {
            return neighbors[(int)dir];
        }

        /// <summary>
        /// 设置单元格对应方向的相邻单元格
        /// </summary>
        /// <param name="dir">相邻的单元格方向</param>
        /// <param name="cell">相邻的单元格</param>
        public void SetNeighBor(HexDirection dir, Element element)
        {
            neighbors[(int)dir] = element;
            element.neighbors[(int)dir.Opposite()] = this;
        }
    }
}