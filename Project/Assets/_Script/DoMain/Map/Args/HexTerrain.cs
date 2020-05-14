namespace OurGameName.DoMain.Map.Args
{
    using System;

    /// <summary>
    /// 地形枚举
    /// </summary>
    public enum Terrain
    {
        /// <summary>
        /// 基础
        /// </summary>
        Base,

        /// <summary>
        /// 沙漠沙丘
        /// </summary>
        DesertDunes,

        /// <summary>
        /// 泥地
        /// </summary>
        Dirt,

        /// <summary>
        /// 阔叶林
        /// </summary>
        ForestBroadleaf,

        /// <summary>
        /// 海洋
        /// </summary>
        Ocean,

        /// <summary>
        /// 山脉
        /// </summary>
        Mountain
    }

    /// <summary>
    /// 六边形地形类
    /// </summary>
    internal class HexTerrain
    {
        public HexTerrain()
        {
        }

        public HexTerrain(
                    string name,
                    Terrain terrain,
                    Predicate<Element> canPlace = null,
                    byte layer = 0,
                    int chunkSize = 12,
                    int wave = 0,
                    int maxSize = 0,
                    int minSize = 0)
        {
            this.Name = name;
            if (canPlace == null)
            {
                this.CanPlace = x => true;
            }
            else
            {
                this.CanPlace = canPlace;
            }
            this.Layer = layer;
            this.ChunkSize = chunkSize;
            this.Wave = wave;
            this.MaxSize = maxSize;
            this.MinSize = minSize;
            this.Terrain = terrain;
        }

        /// <summary>
        /// 地形会从什么条件的单元格上生成的断言
        /// True    可以生成
        /// False   不会生成
        /// </summary>
        public Predicate<Element> CanPlace { get; set; }

        /// <summary>
        /// 单个地形增殖块的标准大小
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        /// 地形所在的图像层 0是最低层 255是最高层
        /// </summary>
        public byte Layer { get; set; }

        /// <summary>
        /// 单个增殖块的大小的最大值
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 单个增殖块的大小的最大值
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// 地形名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地形颜色
        /// </summary>
        public Terrain Terrain { get; set; }

        /// <summary>
        /// 单个核芯增殖数量的上下波动值
        /// </summary>
        public int Wave { get; set; }
    }
}