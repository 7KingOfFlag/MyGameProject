namespace OurGameName.DoMain.Map.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using UnityEngine;

    /// <summary>
    /// 地图辅助类
    /// </summary>
    internal static class MapMetrics
    {
        /// <summary>
        /// 六边形内外径的比值
        /// </summary>
        /// 0.8660254038f = 二分根号3
        public const float innerRadiusRatio = 0.8660254038f;

        /// <summary>
        /// 返回以 CenterCellPosition 坐标为中心 range 范围内的所有单元格的位置
        /// </summary>
        /// <param name="CenterCellPosition">中心单元格的坐标</param>
        /// <param name="range">范围</param>
        public static Vector2Int[] GetCellInRange(this Vector3Int CenterCellPosition, int range)
        {
            var v2 = new Vector2Int(CenterCellPosition.x, CenterCellPosition.y);
            return v2.GetCellInRange(range);
        }

        /// <summary>
        /// 返回以 CenterCellPosition 坐标为中心 range 范围内的所有单元格的位置
        /// </summary>
        /// <param name="CenterCellPosition">中心单元格的坐标</param>
        /// <param name="range">范围</param>
        /// <remarks>
        /// 首先获取中心点 然后为获取每行的首个单元格坐标
        /// 因为是六边形网格单元格排序并不是像方形单元格一样完全规则的
        /// 所以以起始点的Y轴奇偶方便修正行坐标
        /// </remarks>
        public static Vector2Int[] GetCellInRange(this Vector2Int CenterCellPosition, int range)
        {
            if (range == 0) return new Vector2Int[] { CenterCellPosition };

            Vector2Int[] cellTargetArrary = new Vector2Int[GetRangeEffectNumber(range)];
            int count = 0;

            int centerX = CenterCellPosition.x;
            int centerY = CenterCellPosition.y;
            int lineSize = range * 2 + 1; //每行受影响的单元格数
            for (int i = range; i >= 0; i--)
            {
                int startX = centerX - i;
                for (int x = 0; x < lineSize; x++)
                {
                    if (i == range)
                    {
                        cellTargetArrary[count++] = new Vector2Int(startX + x, centerY);
                    }
                    else
                    {
                        int rangeOfCenter = range - i;
                        if (centerY % 2 == 0)
                        {
                            cellTargetArrary[count++] =
                                new Vector2Int(startX + x - ((rangeOfCenter - 1) / 2 + 1), centerY + rangeOfCenter);
                            cellTargetArrary[count++] =
                                new Vector2Int(startX + x - ((rangeOfCenter - 1) / 2 + 1), centerY - rangeOfCenter);
                        }
                        else
                        {
                            cellTargetArrary[count++] =
                                new Vector2Int(startX + x - rangeOfCenter / 2, centerY + rangeOfCenter);
                            cellTargetArrary[count++] =
                                new Vector2Int(startX + x - rangeOfCenter / 2, centerY - rangeOfCenter);
                        }
                    }
                }

                lineSize--;
            }
            return cellTargetArrary;
        }

        /// <summary>
        /// 计算地图碰撞体大小
        /// </summary>
        /// <param name="mapSize">地图大小</param>
        /// <returns>地图碰撞大小</returns>
        public static (float Width, float Height) GetMapColliderSize(Vector2Int mapSize)
        {
            float yCorrection = 0.75f; // Y轴单元格交错修正
            float topCorrection = 0.25f; // 顶部单元格露出的修正
            float xCorrection = 0.5f; // X轴单元格交错修正
            return ((mapSize.x + xCorrection) * 1.0f, mapSize.y * yCorrection + topCorrection);
        }

        /// <summary>
        /// 获取相邻的单元格
        /// </summary>
        /// <param name="CenterPosition">中心单元格</param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> GetNeighborCell(this Vector2Int CenterPosition)
        {
            int correct = 0;
            if (CenterPosition.y % 2 == 0)
            {
                correct = -1;
            }

            var result = new List<Vector2Int>()
            {
                new Vector2Int(CenterPosition.x - 1, CenterPosition.y), // ←
                new Vector2Int(CenterPosition.x + 1, CenterPosition.y), // →
                new Vector2Int(CenterPosition.x + correct, CenterPosition.y + 1), // ↖
                new Vector2Int(CenterPosition.x + 1 + correct, CenterPosition.y + 1), // ↗
                new Vector2Int(CenterPosition.x + correct, CenterPosition.y - 1), // ↙
                new Vector2Int(CenterPosition.x + 1 + correct, CenterPosition.y - 1), // ↘
            };

            return result.Where(position => position.x >= 0 && position.y >= 0);
        }

        /// <summary>
        /// 获取范围大小对应的影响单元格数量
        /// </summary>
        /// <param name="brushSize">笔刷大小</param>
        /// <returns></returns>
        public static int GetRangeEffectNumber(int brushSize)
        {
            int sun, item;
            sun = item = brushSize * 2 + 1;
            for (int i = 0; i < brushSize; i++)
            {
                item--;
                sun += item * 2;
            }
            return sun;
        }
    }
}