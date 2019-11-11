using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
        [ScriptIgnore]
        public AstartNode Parent { get; set; }

        /// <summary>
        /// 通过成本
        /// </summary>
        public int ThroughCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellPosition">单元格位置</param>
        /// <param name="throughCost">通过成本</param>
        public AstartNode(Vector2Int cellPosition, int throughCost)
        {
            CellPosition = cellPosition;
            ThroughCost = throughCost;
            Parent = null;
        }
    }
}
