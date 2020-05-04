namespace OurGameName.DoMain.Map.Extensions
{
    using OurGameName.DoMain.Map.Args;

    public static class HexExtensions
    {
        /// <summary>
        /// 返回上一个方向顺时针方向下一个方向
        /// </summary>
        public static HexDirection Next(this HexDirection dir)
        {
            return dir == HexDirection.NW ? HexDirection.NE : (dir + 1);
        }

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
    }
}