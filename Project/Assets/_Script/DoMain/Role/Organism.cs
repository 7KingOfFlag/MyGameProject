namespace OurGameName.DoMain.RoleSpace
{
    using UnityEngine;

    /// <summary>
    /// 生物类 所有生命体的基类
    /// </summary>
    internal class Organism
    {
        /// <summary>
        /// 当前生命值
        /// </summary>
        private int currentHP;

        /// <summary>
        /// 当前生命值
        /// </summary>
        public int HP
        {
            get { return this.currentHP; }
            set
            {
                if (value <= 0)
                {
                    this.currentHP = 0;
                }
                else if (value >= this.MaxHP)
                {
                    this.currentHP = this.MaxHP;
                }
                else
                {
                    this.currentHP = value;
                }
            }
        }

        /// <summary>
        /// 生物ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP { get; set; }

        /// <summary>
        /// 角色位置
        /// </summary>
        public Vector3Int Position { get; set; }
    }
}