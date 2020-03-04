using UnityEngine;

namespace OurGameName.DoMain.Entity.GameWorld
{
    /// <summary>
    /// 游戏世界 : 数据类保存当前整个游戏数据的数据
    /// </summary>
    internal class World : MonoBehaviour
    {
        /// <summary>
        /// 世界哈希值
        /// </summary>
        public int WorldHash { get; set; }

        private void Awake()
        {
            WorldHash = GetHashCode();
        }
    }
}