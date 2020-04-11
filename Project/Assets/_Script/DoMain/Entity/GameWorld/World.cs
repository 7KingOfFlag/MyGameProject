namespace OurGameName.DoMain.Entity.GameWorld
{
    using UnityEngine;
    using OurGameName.DoMain.Entity.Map;
    using System;
    using OurGameName.DoMain.Attribute;

    /// <summary>
    /// 游戏世界 : 数据类保存当前整个游戏数据的数据
    /// </summary>
    internal class World : MonoBehaviour
    {
        /// <summary>
        /// 初始化完成事件
        /// </summary>
        public event EventHandler<EventArgs> InitComplete;

        /// <summary>
        /// 游戏地图
        /// </summary>
        public GameMap GameMap { get; set; }

        /// <summary>
        /// 地图大小
        /// </summary>
        public Vector2Int MapSzie => this.GameMap.MapSzie;

        /// <summary>
        /// 世界哈希值
        /// </summary>
        public int WorldHash { get; set; }

        /// <summary>
        /// 触发地图更新事件
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnInitComplete(EventArgs e)
        {
            e.Raise(this, ref InitComplete);
        }

        private void Start()
        {
            this.WorldHash = this.GetHashCode();
            this.GameMap = new GameMap(new Vector2Int(20, 20));
            this.OnInitComplete(new EventArgs());
        }
    }
}