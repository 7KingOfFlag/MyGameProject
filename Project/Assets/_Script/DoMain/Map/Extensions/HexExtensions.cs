namespace OurGameName.DoMain.Map.Extensions
{
    using OurGameName.DoMain.Map.Args;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using UnityEngine;
    using Terrain = Args.Terrain;

    public static class HexExtensions
    {
        static HexExtensions()
        {
            CostDict = ConfigCostDict(CostList);
        }

        /// <summary>
        /// 地形通过费用字典
        /// </summary>
        public static Dictionary<Terrain, int> CostDict { get; private set; }

        /// <summary>
        /// 地形通过费用列表 - 配置用
        /// </summary>
        private static Dictionary<int, List<Terrain>> CostList { get; } = new Dictionary<int, List<Terrain>>
        {
            { 1, new List<Terrain>{ Terrain.Base, Terrain.Ocean} },
            { 2, new List<Terrain>{ Terrain.DesertDunes, Terrain.Dirt, Terrain.ForestBroadleaf} },
            { 3, new List<Terrain>{ Terrain.Mountain} },
        };

        /// <summary>
        /// ForEach循环
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this T[,] array, Action<T> action)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    action(array[x, y]);
                }
            }
        }

        /// <summary>
        /// 获取该地形的通过费用
        /// </summary>
        /// <param name="terrain"></param>
        /// <returns></returns>
        public static int GetCost(this Terrain terrain)
        {
            if (CostDict.TryGetValue(terrain, out int result) == false)
            {
                Debug.LogError($"存在未配置的地形类型:{terrain}");
                throw new ArgumentException($"存在未配置的地形类型:{terrain}");
            }

            return result;
        }

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

        /// <summary>
        /// 配置地形通过费用字典
        /// </summary>
        private static Dictionary<Terrain, int> ConfigCostDict(Dictionary<int, List<Terrain>> costList)
        {
            var result = new Dictionary<Terrain, int>();
            foreach (var item in costList)
            {
                item.Value.ForEach(x => result.Add(x, item.Key));
            }

            return result;
        }
    }
}