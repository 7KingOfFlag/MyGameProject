using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace OurGameName.DoMain.Entity.Map
{
    /// <summary>
    /// 游戏地图
    /// </summary>
    internal class Map
    {
        /// <summary>
        /// 地图大小
        /// </summary>
        public Vector2Int MapSzie { get; set; }

        /// <summary>
        /// 地图单元
        /// </summary>
        public List<Element> Elements { get; set; }

        /// <summary>
        /// 游戏地图
        /// </summary>
        /// <param name="mapSzie">地图大小</param>
        public Map(Vector2Int mapSzie)
        {
            MapSzie = mapSzie;
            Elements = InitElements(MapSzie);
        }

        /// <summary>
        /// 初始化地图单元
        /// </summary>
        /// <param name="mapSzie">地图大小</param>
        /// <returns></returns>
        private List<Element> InitElements(Vector2Int mapSzie)
        {
            List<Element> result = new List<Element>(mapSzie.x * mapSzie.y);
            for (int x = 0; x < mapSzie.x; x++)
            {
                for (int y = 0; y < mapSzie.y; y++)
                {
                    result.Add(new Element(new Vector2Int(x, y)));
                }
            }
        }

        /// <summary>
        /// 地图单元
        /// </summary>
        /// <param name="x">地图单元X轴坐标</param>
        /// <param name="y">地图单元Y轴坐标</param>
        /// <returns>对应位置上的地图单元</returns>
        public Element this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x + MapSzie.x * y > MapSzie.x * MapSzie.y)
                {
                    Debug.LogError($"地图索引({x},{y})错误,索引越界!");
                    return null;
                }
                return Elements[x + MapSzie.x * y];
            }
        }
    }
}