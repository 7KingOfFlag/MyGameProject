using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OurGameName.DoMain.Entity.TileHexMap.AStar
{
    internal class AstartNode
    {
        /// <summary>
        /// 单元格位置
        /// </summary>
        public Vector2Int CellPosition { get; private set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public AstartNode Parent { get; set; }

        /// <summary>
        /// 障碍物
        /// </summary>
        public bool Obstacle { get; set; }

        /// <summary>
        /// 通过成本
        /// </summary>
        public int ThroughCost { get; set; }
    }
}
