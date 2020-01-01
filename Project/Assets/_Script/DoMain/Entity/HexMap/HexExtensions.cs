namespace OurGameName.DoMain.Entity.HexMap
{
    /// <summary>
    /// 六边形方向
    /// </summary>
    public enum HexDirection
    {
        /// <summary>
        /// 东北 ↗
        /// </summary>
        NE,

        /// <summary>
        /// 东 →
        /// </summary>
        E,

        /// <summary>
        /// 东南 ↘
        /// </summary>
        SE,

        /// <summary>
        /// 西南 ↙
        /// </summary>
        SW,

        /// <summary>
        /// 西 ←
        /// </summary>
        W,

        /// <summary>
        /// 西北 ↖
        /// </summary>
        NW
    }

    public static class HexExtensions
    {
        /// <summary>
        /// 返回相对的方向
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static HexDirection Opposite(this HexDirection dir)
        {
            return (int)dir < 3 ? (dir + 3) : (dir - 3);
        }

        /// <summary>
        /// 返回上一个方向上的单元格
        /// </summary>
        public static HexDirection Previous(this HexDirection dir)
        {
            return dir == HexDirection.NE ? HexDirection.NW : (dir - 1);
        }

        /// <summary>
        /// 返回上一个方向顺时针方向下一个方向
        /// </summary>
        public static HexDirection Next(this HexDirection dir)
        {
            return dir == HexDirection.NW ? HexDirection.NE : (dir + 1);
        }

        public static string BeString(this Terrain terrain)
        {
            string result;
            switch (terrain)
            {
                case Terrain.coast:
                    result = "海洋";
                    break;

                case Terrain.grassland:
                    result = "草原";
                    break;

                case Terrain.mountains:
                    result = "山脉";
                    break;

                case Terrain.desert:
                    result = "沙漠";
                    break;

                default:
                    result = "特殊";
                    break;
            }

            return result;
        }
    }
}