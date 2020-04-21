namespace OurGameName.DoMain.Entity.Map.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// 地图辅助类
    /// </summary>
    internal static class MapMetrics
    {
        /// <summary>
        /// 六边形内外径的比值
        /// </summary>
        /// 0.8660254038f = 二分根号3 是
        public const float innerRadiusRatio = 0.8660254038f;

        /// <summary>
        /// 计算地图碰撞体大小
        /// </summary>
        /// <param name="mapSize">地图大小</param>
        /// <returns>地图碰撞大小</returns>
        public static (float Width, float Height) GetMapColliderSize(Vector2Int mapSize)
        {
            return (mapSize.x * 1.0f, mapSize.y * innerRadiusRatio);
        }
    }
}