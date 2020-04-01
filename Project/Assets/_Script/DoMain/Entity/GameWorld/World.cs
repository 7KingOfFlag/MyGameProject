using UnityEngine;
using OurGameName.DoMain.Entity.Map;
using System;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.GameWorld
{
    /// <summary>
    /// 游戏世界 : 数据类保存当前整个游戏数据的数据
    /// </summary>
    internal class World : MonoBehaviour
    {
        /// <summary>
        /// 初始化完成事件
        /// </summary>
        public event Action InitComplete;

        /// <summary>
        /// 游戏地图
        /// </summary>
        public GameMap GameMap { get; set; }

        /// <summary>
        /// 世界哈希值
        /// </summary>
        public int WorldHash { get; set; }

        private void Awake()
        {
            this.WorldHash = this.GetHashCode();
            this.GameMap = new GameMap(new Vector2Int(20, 20));
            InitComplete();
        }
    }
}