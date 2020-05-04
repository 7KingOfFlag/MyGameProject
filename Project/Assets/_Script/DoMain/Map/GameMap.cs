namespace OurGameName.DoMain.Map
{
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using System.Linq;
    using OurGameName.Extension;
    using System.Collections;
    using OurGameName.DoMain.Map.Args;
    using Terrain = Args.Terrain;

    /// <summary>
    /// 游戏地图
    /// </summary>
    internal class GameMap : IEnumerable<Element>
    {
        /// <summary>
        /// 游戏地图
        /// </summary>
        /// <param name="mapSzie">地图大小</param>
        public GameMap(Vector2Int mapSzie)
        {
            this.MapSzie = mapSzie;
            this.Elements = this.InitElements(this.MapSzie);
            this.Elements = this.GenerateTerrain(this.Elements);
        }

        /// <summary>
        /// 地图更新事件
        /// </summary>
        public event EventHandler<MapUpdateArgs> Update;

        /// <summary>
        /// 地图单元
        /// </summary>
        public List<Element> Elements { get; private set; }

        /// <summary>
        /// 地图大小
        /// </summary>
        public Vector2Int MapSzie { get; set; }

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
                if (x < 0 || y < 0 || x + this.MapSzie.x * y > this.MapSzie.x * this.MapSzie.y)
                {
                    Debug.LogError($"地图索引({x},{y})错误,索引越界!");
                    return null;
                }
                return this.Elements[x + this.MapSzie.x * y];
            }
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        public IEnumerator<Element> GetEnumerator() => this.Elements.GetEnumerator();

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => this.Elements.GetEnumerator();

        /// <summary>
        /// 触发地图更新事件
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnUpdate(MapUpdateArgs e)
        {
            e.Raise(this, ref Update);
        }

        private List<Element> GenerateTerrain(List<Element> elements)
        {
            System.Random random = new System.Random();
            elements.ForEach(x => x.Terrain = (Terrain)random.Next(0, 3));
            return elements;
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
            return result;
        }
    }
}