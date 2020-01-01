namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// 六边形方向
    /// </summary>
    internal enum HexDirection
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

    /// <summary>
    /// 六边形枚举扩展方法
    /// </summary>
    internal static class HexDirectionEnumExtensions
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
        /// 返回上一个方向上顺时针方向上的上一个方向
        /// </summary>
        public static HexDirection Previous(this HexDirection dir)
        {
            return dir == HexDirection.NE ? HexDirection.NW : (dir - 1);
        }

        /// <summary>
        /// 返回上一个方向顺时针方向上的下一个方向
        /// </summary>
        public static HexDirection Next(this HexDirection dir)
        {
            return dir == HexDirection.NW ? HexDirection.NE : (dir + 1);
        }
    }
}