using UnityEngine;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// HexTile 常量与辅助类
    /// </summary>
    public static class HexTileMetrics
    {
        /// <summary>
        /// 返回以 CenterCellPosition 坐标为中心 range 范围内的所有单元格的位置
        /// </summary>
        /// <param name="CenterCellPosition">中心单元格的坐标</param>
        /// <param name="range">范围</param>
        /// <returns></returns>
        /// <remarks>
        /// 首先获取中心点 然后为获取每行的首个单元格坐标
        /// 因为是六边形网格单元格排序并不是像方形单元格一样完全规则的
        /// 所以以起始点的Y轴奇偶方便修正行坐标
        /// </remarks>
        public static Vector3Int[] GetCellInRange(Vector3Int CenterCellPosition, int range)
        {
            Vector3Int[] cellTargetArrary = new Vector3Int[GetRangeEffectNumber(range)];
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
                        cellTargetArrary[count++] = new Vector3Int(startX + x, centerY, 0);
                    }
                    else
                    {
                        int rangeOfCenter = range - i;
                        if (centerY % 2 == 0)
                        {
                            cellTargetArrary[count++] =
                                new Vector3Int(startX + x - ((rangeOfCenter - 1) / 2 + 1), centerY + rangeOfCenter, 0);
                            cellTargetArrary[count++] =
                                new Vector3Int(startX + x - ((rangeOfCenter - 1) / 2 + 1), centerY - rangeOfCenter, 0);
                        }
                        else
                        {
                            cellTargetArrary[count++] =
                                new Vector3Int(startX + x - rangeOfCenter / 2, centerY + rangeOfCenter, 0);
                            cellTargetArrary[count++] =
                                new Vector3Int(startX + x - rangeOfCenter / 2, centerY - rangeOfCenter, 0);
                        }
                    }
                }

                lineSize--;

            }
            return cellTargetArrary;
        }

        /// <summary>
        /// 获取范围大小对应的影响单元格数量
        /// </summary>
        /// <param name="brushSize"></param>
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
