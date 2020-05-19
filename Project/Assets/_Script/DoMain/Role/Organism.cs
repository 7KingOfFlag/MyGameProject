namespace OurGameName.DoMain.RoleSpace
{
    using UnityEngine;
    using UniRx;

    /// <summary>
    /// 生物类 所有生命体的基类
    /// </summary>
    internal class Organism
    {
        /// <summary>
        /// 当前生命值
        /// </summary>
        private readonly ReactiveProperty<int> currentHP = new ReactiveProperty<int>();

        /// <summary>
        /// 当前生命值
        /// </summary>
        public ReactiveProperty<int> HP
        {
            get { return this.currentHP; }
            set
            {
                if (value.Value <= 0)
                {
                    this.currentHP.Value = 0;
                }
                else if (value.Value >= this.MaxHP.Value)
                {
                    this.currentHP.Value = this.MaxHP.Value;
                }
                else
                {
                    this.currentHP.Value = value.Value;
                }
            }
        }

        /// <summary>
        /// 生物ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public ReactiveProperty<int> MaxHP { get; set; }

        /// <summary>
        /// 角色位置
        /// </summary>
        public ReactiveProperty<Vector3Int> Position { get; set; }
    }
}