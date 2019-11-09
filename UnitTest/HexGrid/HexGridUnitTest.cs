using Microsoft.VisualStudio.TestTools.UnitTesting;
using OurGameName.DoMain.Entity.TileHexMap;
using OurGameName.DoMain.Entity.TileHexMap.UI;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace UnitTest.HexGridTest
{
    [TestClass]
    public class HexGridUnitTest
    {
        /// <summary>
        /// 网格大小参数
        /// </summary>
        [TestMethod]
        public void GridSizeTest()
        {
            Vector2Int gridSize = new Vector2Int(36, 27);
            HexGrid hexGrid = new HexGrid(new HexMapCreateArgs(mapSize: gridSize));
            Assert.AreEqual(gridSize, hexGrid.GridSize);
            Assert.AreEqual(gridSize.x, hexGrid.GridSize.x);
            Assert.AreEqual(gridSize.y, hexGrid.GridSize.y);
        }

        /// <summary>
        /// 获取相邻节点测试 - 当中心点Y行是奇数的情况 相邻数组方向应与 HexDirection 枚举对应的方向一致
        /// </summary>
        [TestMethod]
        public void SetCellNeighborOddTest()
        {
            Vector2Int centreYisOdd = new Vector2Int(4, 1);
            Vector2Int[] centreYisOddNeigborTesetData =
            {
                new Vector2Int(5,2),
                new Vector2Int(5,1),
                new Vector2Int(5,0),
                new Vector2Int(4,0),
                new Vector2Int(3,1),
                new Vector2Int(4,2),
            };

            Vector2Int[] centreYisOddNeighbor = HexGrid.CalculateNeighbor(centreYisOdd);
            CollectionAssert.AreEqual(expected: centreYisOddNeigborTesetData, actual: centreYisOddNeighbor);
        }
        /// <summary>
        /// 获取相邻节点测试 - 当中心点Y行是偶数的情况 相邻数组方向应与 HexDirection 枚举对应的方向一致
        /// </summary>
        [TestMethod]
        public void SetCellNeighborEvenTest()
        {
            Vector2Int centreYisEven = new Vector2Int(1, 2);
            Vector2Int[] centreYisEvenNeigborTesetData =
            {
                new Vector2Int(1,3),
                new Vector2Int(2,2),
                new Vector2Int(1,1),
                new Vector2Int(0,1),
                new Vector2Int(0,2),
                new Vector2Int(0,3),
            };
            Vector2Int[] centreYisEvenNeighbor = HexGrid.CalculateNeighbor(centreYisEven);
            CollectionAssert.AreEqual(expected: centreYisEvenNeigborTesetData, actual: centreYisEvenNeighbor);
        }
    }
}
