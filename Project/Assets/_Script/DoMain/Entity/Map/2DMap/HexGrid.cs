using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurGameName.DoMain.Entity.GameWorld;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OurGameName.DoMain.Entity.Map._2DMap
{
    internal class HexGrid : MonoBehaviour
    {
        /// <summary>
        /// 游戏世界
        /// </summary>
        public World GameWorld;

        /// <summary>
        /// 背景地图
        /// </summary>
        public Tilemap tilemapBackground;

        private void Awake()
        {
            this.GameWorld.InitComplete += () => this.InitMap();
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        private void InitMap()
        {
        }
    }
}