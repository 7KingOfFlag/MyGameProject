﻿using UnityEngine;

namespace OurGameName.DoMain.Entity.RoleSpace
{
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
        /// <see cref="Organism"/>
        /// </summary>
        /// <param name="id">生物ID</param>
        /// <param name="maxHP">生物最大生命值</param>
        /// <param name="position">位置</param>
        public Organism(long id, int maxHP, Vector3Int position)
        {
            ID = id;
            MaxHP = maxHP;
            currentHP = MaxHP;
            Position = position;
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
        /// 当前生命值
        /// </summary>
        public int HP
        {
            get { return currentHP; }
            set
            {
                if (value <= 0)
                {
                    currentHP = 0;
                }
                else if (value >= MaxHP)
                {
                    currentHP = MaxHP;
                }
                else
                {
                    currentHP = value;
                }
            }
        }

        /// <summary>
        /// 位置
        /// </summary>
        public Vector3Int Position { get; set; }
    }
}