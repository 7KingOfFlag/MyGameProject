using OurGameName.DoMain.Entity.TileHexMap.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// 六边形地图
    /// </summary>
    internal class HexGrid
    {
        /// <summary>
        /// 地图节点数组 地图上数据的存储对象
        /// </summary>
        public HexCell[,] HexCells { get; private set; }

        public HexGrid()
        {}

        /// <summary>
        /// 六边形地图
        /// </summary>
        /// <param name="sizeX">地图X轴大小</param>
        /// <param name="sizeY">地图Y轴大小</param>
        public HexGrid(HexMapCreateArgs agrs)
        {
            Init(agrs.MapSize.x, agrs.MapSize.y);
        }

        public void Init(int sizeX, int sizeY)
        {
            HexCells = new HexCell[sizeX, sizeY];
            CreateCells(sizeX, sizeY);
        }

        /// <summary>
        /// 新建地图节点
        /// </summary>
        private void CreateCells(int sizeX, int SizeY)
        {
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    HexCells[x, y] = new HexCell("hexPlains00", new Vector2Int(x, y));
                }
            }
            
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                { 
                    SetCellNeighbor(HexCells[x, y]);
                }
            }

        }

        /// <summary>
        /// 设置相邻节点
        /// </summary>
        /// <param name="cell"></param>
        private void SetCellNeighbor(HexCell cell)
        {
            var neighborArray = HexTileMetrics.GetCellInRange(cell.CellPosition, 1);
            neighborArray = neighborArray.Select(item =>
            {
                if (item.x < 0 || item.y < 0 || item.x > MapSizeX || item.y > MapSizeY)
                {
                    item = item.Error();
                }
                return item;
            }).ToArray();
            cell.neighborsPosition = RemapNeighbors(neighborArray);
        }

        /// <summary>
        /// 重新映射相邻节点位置使之符合枚举排序
        /// </summary>
        /// <param name="neighborArray"></param>
        /// <returns></returns>
        private Vector2Int[] RemapNeighbors(Vector2Int[] neighborArray)
        {
            Vector2Int[] result = new Vector2Int[6];
            result[(int)HexDirection.NE] = neighborArray[5];
            result[(int)HexDirection.E] = neighborArray[2];
            result[(int)HexDirection.SE] = neighborArray[6];
            result[(int)HexDirection.SW] = neighborArray[4];
            result[(int)HexDirection.W] = neighborArray[0];
            result[(int)HexDirection.NW] = neighborArray[3];

            return result;
        }

        #region 工具方法
        public Vector2Int MapSize
        {
            get { return new Vector2Int(HexCells.GetLength(0), HexCells.GetLength(1)); }
        }

        public int MapSizeX { get { return HexCells.GetLength(0); } }
        public int MapSizeY { get { return HexCells.GetLength(1); } }
        #endregion
    }
}
