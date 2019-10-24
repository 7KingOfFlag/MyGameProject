using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurGameName.DoMain.Entity.HexMap
{
    /// <summary>
    /// 六边形地形类
    /// </summary>
    public class HexTerrain
    {
        /// <summary>
        /// 地形名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地形会从什么条件的单元格上生成的断言
        /// True    可以生成
        /// False   不会生成
        /// </summary>
        public Predicate<HexCell> CanPlace { get; set; }

        /// <summary>
        /// 地形所在的图像层 0是最低层 255是最高层
        /// </summary>
        public byte Layer { get; set; }

        /// <summary>
        /// 单个地形增殖块的标准大小
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        /// 单个核芯增殖数量的上下波动值
        /// </summary>
        public int Wave { get; set; }

        /// <summary>
        /// 单个增殖块的大小的最大值
        /// </summary>
        public int MaxSize { get; set; }
        /// <summary>
        /// 单个增殖块的大小的最大值
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// 地形颜色
        /// </summary>
        public Terrain Terrain { get; set; }

        public HexTerrain()
        {
            
        }

        public HexTerrain(string name, Terrain terrain, Predicate<HexCell> canPlace = null, byte layer = 0, int chunkSize = 12, int wave = 0, int maxSize = 0, int minSize = 0)
        {
            Name = name;
            if (canPlace == null)
            {
                CanPlace = x => true;
            }
            else
            {
                CanPlace = canPlace;
            }
            Layer = layer;
            ChunkSize = chunkSize;
            Wave = wave;
            MaxSize = maxSize;
            MinSize = minSize;
            this.Terrain = terrain;
        }
    }

    /// <summary>
    /// 地形枚举
    /// </summary>
    public enum Terrain
    {
        /// <summary>
        /// 海岸 近海
        /// </summary>
        coast,
        /// <summary>
        /// 草原
        /// </summary>
        grassland,
        /// <summary>
        /// 山脉
        /// </summary>
        mountains,
        /// <summary>
        /// 沙漠
        /// </summary>
        desert
    }
}
