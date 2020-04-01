using System;
using System.Collections.Generic;
using OurGameName.DoMain.Attribute;
using UnityEngine;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// HexTile 常量与辅助类
    /// </summary>
    internal static class HexTileMetrics
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
        public static Vector2Int[] GetCellInRange(Vector2Int CenterCellPosition, int range)
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

        /// <summary>
        /// 获取 tilemap大小
        /// <para>仅在 tilemap 为实心矩形时有效</para>
        /// </summary>
        /// <param name="tilemap"></param>
        /// <returns></returns>
        public static Vector2Int GetMapSzie(UnityEngine.Tilemaps.Tilemap tilemap)
        {
            var result = new Vector2Int();
            for (int x = 0; x < tilemap.size.x; x++)
            {
                if (tilemap.GetTile(new Vector3Int(x, 0, 0)) == null)
                {
                    result.x = x;
                    break;
                }
            }
            for (int y = 0; y < tilemap.size.x; y++)
            {
                if (tilemap.GetTile(new Vector3Int(0, y, 0)) == null)
                {
                    result.y = y;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 计算起点到终点的曼哈顿距离
        /// </summary>
        /// <param name="startPoint">起点坐标</param>
        /// <param name="endPoint">终点坐标</param>
        /// <returns></returns>
        public static int CalculateDistance(Vector2Int startPoint, Vector2Int endPoint)
        {
            int resultX = startPoint.x - endPoint.x;
            int resultY = startPoint.y - endPoint.y;
            return Mathf.Abs(resultX) + Mathf.Abs(resultY);
        }

        /// <summary>
        /// 计算 range 范围内其他单元格到计算的中心坐标的距离
        /// <para>返回一个 单元格位置 与 位置距离计算的中心坐标的距离 组成的字典</para>
        /// </summary>
        /// <param name="hexGrid">单元格所在的六边形地图</param>
        /// <param name="calculayePosition">需要计算的中心坐标</param>
        /// <param name="range">计算范围</param>
        /// <returns></returns>
        public static Dictionary<Vector2Int, int> CalculateDistance(HexGrid hexGrid, Vector2Int calculayePosition, int range)
        {
            //参数初始化
            Dictionary<Vector2Int, int> result = new Dictionary<Vector2Int, int>();
            result.Add(calculayePosition, 0);
            bool[,] doneArray = new bool[hexGrid.GridSizeX, hexGrid.GridSizeY];

            doneArray[calculayePosition.x, calculayePosition.y] = true;
            List<Vector2Int>[] calculateList = new List<Vector2Int>[2]
            {
                new List<Vector2Int>(){calculayePosition},
                new List<Vector2Int>()
            };
            int currentClaculatIndex = 0, nextClaculatIndex = 1;
            //开始计算
            for (int i = 0; i < range; i++)
            {
                foreach (var item in calculateList[currentClaculatIndex])
                {
                    if (hexGrid.IsPositionOutOfGridRange(item)) continue; //排除超出网格范围的单元格
                    Vector2Int[] neighbors = hexGrid.HexCells.GetItem(item).NeighborsPosition;
                    calculateList[nextClaculatIndex].AddRange(neighbors);
                    foreach (var neighbor in neighbors)
                    {
                        if (hexGrid.IsPositionOutOfGridRange(neighbor)) continue; //排除超出网格范围的单元格
                        if (doneArray.GetItem(neighbor) == true) continue;

                        doneArray.SetItem(neighbor, true);
                        int minThroughCostOfNeighbor =
                            GetMinThroughCostOfNeighbor(hexGrid.HexCells.GetItem(neighbor).NeighborsPosition, doneArray, result);
                        result.Add(neighbor, minThroughCostOfNeighbor + hexGrid.ThroughCost(neighbor));
                    }
                }
                Extension.Swop(ref currentClaculatIndex, ref nextClaculatIndex); //交换双缓存数组
                calculateList[nextClaculatIndex].Clear();
            }
            return result;
        }

        /// <summary>
        /// 求在 hexGrid 中起点到终点的最短路径
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="source">起点</param>
        /// <param name="destination">终点</param>
        /// <returns></returns>
        public static Vector2Int[] ShortestPath(HexGrid hexGrid, Vector2Int source, Vector2Int destination)
        {
            //初始化
            int[,] dist = Extension.Init(hexGrid.GridSize, int.MaxValue);
            Vector2Int[,] path = Extension.Init(hexGrid.GridSize, Vector2Int.down);
            bool[,] doneArray = new bool[hexGrid.GridSizeX, hexGrid.GridSizeY];
            dist.SetItem(source, 0);
            Vector2Int Error = Vector2Int.zero.Error();
            Vector2Int minUndonePosition;
            //计算
            while (true)
            {
                minUndonePosition = GetMinPositionAbsentArray(dist, doneArray);
                if (minUndonePosition == Error) break;
                doneArray.SetItem(minUndonePosition, true);
                foreach (var neighbor in hexGrid[minUndonePosition].NeighborsPosition)
                {
                    if (hexGrid.IsPositionOutOfGridRange(neighbor)) continue; //排除超出网格范围的单元格
                    int throughCost = dist.GetItem(minUndonePosition) + hexGrid[neighbor].ThroughCost;
                    if (throughCost < dist.GetItem(neighbor))
                    {
                        dist.SetItem(neighbor, throughCost);
                        path.SetItem(neighbor, minUndonePosition);
                    }
                }
                bool isOver = true;
                foreach (var item in hexGrid[destination].NeighborsPosition)
                {
                    isOver &= doneArray.GetItem(item);
                }
                if (isOver == true)
                {
                    break;
                }
            }
            //准备返回
            Stack<Vector2Int> resultStack = new Stack<Vector2Int>();
            resultStack.Push(destination);
            Vector2Int index = path.GetItem(destination);
            while (path.GetItem(index) != Vector2Int.down)
            {
                resultStack.Push(index);
                index = path.GetItem(index);
            }
            Vector2Int[] result = new Vector2Int[resultStack.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = resultStack.Pop();
            }
            return result;
        }

        private static Vector2Int GetMinPositionAbsentArray(int[,] dist, bool[,] doneArray)
        {
            if (dist.GetLength(0) != doneArray.GetLength(0) || dist.GetLength(1) != doneArray.GetLength(1))
            {
                Debug.LogError("两个参数数组大小不相等");
            }
            int min = int.MaxValue;
            Vector2Int minIndex = Vector2Int.zero.Error();
            for (int x = 0; x < dist.GetLength(0); x++)
            {
                for (int y = 0; y < dist.GetLength(1); y++)
                {
                    if (doneArray[x, y] == false && dist[x, y] < min)
                    {
                        min = dist[x, y];
                        minIndex = new Vector2Int(x, y);
                    }
                }
            }
            return minIndex;
        }

        /// <summary>
        /// 从相邻节点获取以计算的最小通行费用
        /// </summary>
        /// <param name="neighbors">相邻节点数组</param>
        /// <param name="doneArray">以计算节点数组</param>
        /// <returns></returns>
        private static int GetMinThroughCostOfNeighbor(Vector2Int[] neighbors, bool[,] doneArray, Dictionary<Vector2Int, int> doneThroughCost)
        {
            int min = int.MaxValue;
            for (int i = 0; i < neighbors.Length; i++)
            {
                try
                {
                    if (doneArray.GetItem(neighbors[i]) == true)
                    {
                        min = Mathf.Min(min, doneThroughCost[neighbors[i]]);
                    }
                }
                catch (IndexOutOfRangeException) { }//无视越界的单元格
            }
            return min;
        }
    }
}