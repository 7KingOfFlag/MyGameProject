namespace OurGameName.DoMain.Entity.HexMap
{
    /// <summary>
    /// W = 西
    /// E = 东
    /// NW = 西北
    /// NE = 东北
    /// SW = 西南
    /// SE = 东南
    /// </summary>
    public enum HexDirection
    {
       NE,E,SE,SW,W,NW
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
        /// 返回上一个方向上的节点
        /// </summary>
        public static HexDirection Previous(this HexDirection dir)
        {
            return dir == HexDirection.NE ? HexDirection.NW : (dir - 1);
        }
        /// <summary>
        /// 返回上一个方向下的节点
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
