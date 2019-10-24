using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OurGameName.DoMain.Entity.HexMap
{
    /// <summary>
    /// 用来在HexCell类序列化中间过度
    /// </summary>
    public class HexCellSerialization
    {
        /// <summary>
        /// 地图节点坐标
        /// </summary>
        public int X, Z;

        /// <summary>
        ///地形索引
        /// </summary>
        public int TerrainTypeIndex;

        public HexCellSerialization(HexCell cell)
        {
            Serialization(cell);
        }

        public HexCellSerialization()
        {

        }

        public void Serialization(HexCell cell)
        {
            X = cell.coordinates.ToListIndex().X;
            Z = cell.coordinates.ToListIndex().Y;

            TerrainTypeIndex = (int)cell.TerrainTypeIndex;
        }

        /// <summary>
        /// 将HexCell实例 转化成用来序列化的 HexCellSerialization实例
        /// </summary>
        /// <param name="cells">HexCell实例数组</param>
        /// <returns>HexCellSerialization实例数组</returns>
        public static HexCellSerialization[] ShiftCells(HexCell[,] cells)
        {
            HexCellSerialization[] result = new HexCellSerialization[cells.GetLength(0)*cells.GetLength(1)];
            int i = 0;
            for (int z = 0; z < cells.GetLength(1); z++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x, z] != null)
                    {
                        result[i] = new HexCellSerialization(cells[x, z]);
                        i++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 载入地图
        /// 实现：用序列化地图文件覆盖原本的地图
        /// </summary>
        /// <param name="cells">游戏场景中使用的地图的实例</param>
        /// <param name="serializations">需要载入的序列化地图</param>
        public static void CoverageHexMap(HexCell[,] cells,HexCellSerialization[] serializations)
        {
            for (int z = 0,i = 0; z < cells.GetLength(1); z++)
            {
                for (int x = 0; x < cells.GetLength(0); x++,i++)
                {
                    try
                    {
                        CoverageHexMap(cells[x, z], serializations[i]);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(string.Format("数组下标越界\n序列化数组大小{1} 下标:{0}\n", i, serializations.Length));
                        builder.Append(string.Format("地图节点数组大小({0},{1}) 下标:({2},{3})", cells.GetLength(0), cells.GetLength(1), x, z));
                        Debug.Log(builder.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 用序列化地图的中的节点覆盖游戏场景中的地图节点
        /// </summary>
        /// <param name="hexCell">游戏场景中的地图节点</param>
        /// <param name="hexCellSerialization">序列化地图的节点</param>
        private static void CoverageHexMap(HexCell hexCell, HexCellSerialization serializationCell)
        {
            hexCell.TerrainTypeIndex = (Terrain)serializationCell.TerrainTypeIndex;
        }
    }
}
