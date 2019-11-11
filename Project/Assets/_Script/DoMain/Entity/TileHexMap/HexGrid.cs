﻿using OurGameName.DoMain.Entity.TileHexMap.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OurGameName.DoMain.Attribute;
using System.Runtime.CompilerServices;
using OurGameName.DoMain.Entity.HexMap;
using OurGameName.DoMain.Data;
using UnityEngine.Tilemaps;
using System.Web.Script.Serialization;
using System.IO;

[assembly:InternalsVisibleTo("UnitTest")]
namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// 六边形地图
    /// </summary>
    internal class HexGrid
    {
        /// <summary>
        /// 地图单元格数组 地图上数据的存储对象
        /// </summary>
        public HexCell[,] HexCells { get; private set; }
        /// <summary>
        /// Tile通过费用字典
        /// </summary>
        [ScriptIgnore]
        private Dictionary<string, int> m_terrainThroughCostDict;
        /// <summary>
        /// 游戏数据中心
        /// </summary>
        private GameAssetDataHelper m_gameAssetData;
        public HexGrid()
        {}

        /// <summary>
        /// 六边形地图
        /// </summary>
        /// <param name="agrs">六边形地图创建参数</param>
        /// <param name="terrainThroughCostDict">地形通过费用</param>
        public HexGrid(HexMapCreateArgs agrs, Dictionary<string, int> terrainThroughCostDict, GameAssetDataHelper gameAssetData)
        {
            m_terrainThroughCostDict = terrainThroughCostDict;
            m_gameAssetData = gameAssetData;
            Init(agrs.MapSize.x, agrs.MapSize.y);
        }

        public void Init(int sizeX, int sizeY)
        {
            HexCells = new HexCell[sizeX, sizeY];
            Debug.Log($"{typeof(HexCell[,])}");
            CreateCells(sizeX, sizeY);
        }

        /// <summary>
        /// 新建地图单元格
        /// </summary>
        private void CreateCells(int sizeX, int SizeY)
        {
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    TileBase tileBase = m_gameAssetData.GetRandomTileAssetDict("Ocean");
                    HexCells[x, y] = new HexCell(tileBase.name, new Vector2Int(x, y), throughCost: 1);
                    HexCells[x, y].NeighborsPosition =
                       FiltrationOutOfGridRangePosition(CalculateNeighbor(HexCells[x, y].CellPosition));
                }
            }
        }

        /// <summary>
        /// 根据中心单元格位置获取中心点相邻单元格的数组
        /// <para>数组中单元格排序依照 HexDirection 枚举中表示的方向的顺序排列</para>
        /// </summary>
        /// <param name="centrePosition">所求的相邻单元格数组的中心单元格位置</param>
        /// <remarks>
        /// 六边形单元格偶数行修正原理
        /// 因为六边形网格是奇数行与偶数行是交错排列的
        /// 所以偶数行会比奇数行落后一格
        /// 这导致在中心点的上下四个相邻的单元格偶数行会比奇数行单元格整体向 x 轴负方向移动一个单位
        /// </remarks>
        public static Vector2Int[] CalculateNeighbor(Vector2Int centrePosition)
        {
            Vector2Int[] result = new Vector2Int[6];
            int correction = 0;
            if (centrePosition.y % 2 == 0) //如果中心点在偶数行 增加修正值
            {   
                correction = -1; 
            }
            result[(int)HexDirection.NE] = new Vector2Int(centrePosition.x + 1 + correction, centrePosition.y + 1);
            result[(int)HexDirection.E] = new Vector2Int(centrePosition.x + 1, centrePosition.y);
            result[(int)HexDirection.SE] = new Vector2Int(centrePosition.x + 1 + correction, centrePosition.y - 1);
            result[(int)HexDirection.SW] = new Vector2Int(centrePosition.x + correction, centrePosition.y - 1);
            result[(int)HexDirection.W] = new Vector2Int(centrePosition.x - 1, centrePosition.y);
            result[(int)HexDirection.NW] = new Vector2Int(centrePosition.x + correction, centrePosition.y + 1);

            return result;
        }
        #region 工具方法
        /// <summary>
        /// 网格大小
        /// </summary>
        public Vector2Int GridSize { get { return new Vector2Int(HexCells.GetLength(0), HexCells.GetLength(1)); } }
        /// <summary>
        /// 网格列数
        /// </summary>
        public int GridSizeX { get { return HexCells.GetLength(0); } }
        /// <summary>
        /// 网格行数
        /// </summary>
        public int GridSizeY { get { return HexCells.GetLength(1); } }

        /// <summary>
        /// 单元格坐标位置是否超出网格范围
        /// <para>超出返回true 反之返回false</para>
        /// </summary>
        /// <param name="cellPosition"></param>
        public bool IsPositionOutOfGridRange(Vector2Int cellPosition)
        {
            return cellPosition.x < 0 || cellPosition.y < 0 || cellPosition.x >= GridSizeX || cellPosition.y >= GridSizeY;
        }
        
        /// <summary>
        /// 过滤超出网格坐标的单元格
        /// </summary>
        /// <param name="Positions"></param>
        /// <returns></returns>
        public Vector2Int FiltrationOutOfGridRangePosition(Vector2Int Position)
        {
            return IsPositionOutOfGridRange(Position) ? Position.Error() : Position;
        }

        /// <summary>
        /// 过滤超出网格坐标的单元格
        /// </summary>
        /// <param name="Positions"></param>
        /// <returns></returns>
        public Vector2Int[] FiltrationOutOfGridRangePosition(Vector2Int[] Positions)
        {
            return Positions.Select(position => FiltrationOutOfGridRangePosition(position)).ToArray();
        }
        #endregion
    }
}
