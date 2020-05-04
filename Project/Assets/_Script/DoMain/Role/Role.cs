namespace OurGameName.DoMain.RoleSpace
{
    using System;

    /// <summary>
    /// 角色态度
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 中立
        /// </summary>
        neutrality,

        /// <summary>
        /// 盟友
        /// </summary>
        ally,

        /// <summary>
        /// 敌人
        /// </summary>
        enemy,

        /// <summary>
        /// 玩家
        /// </summary>
        player
    }

    /// <summary>
    /// 角色类
    /// </summary>
    public class Role
    {
        private static int IDCount = 0;

        /// <summary>
        /// 创建角色实例(详细)
        /// </summary>
        /// <param name="familyName">姓</param>
        /// <param name="name">名</param>
        /// <param name="birthday">生日</param>

        public Role(string familyName, string name, DateTime birthday)
        {
            this.ID = IDCount++;
            this.FamilyName = familyName;
            this.Name = name;
            this.Birthday = birthday;
        }

        /// <summary>
        /// 反序列化用构造函数
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// 出生时间 格式####.##.##
        /// </summary>
        public DateTime Birthday { get; private set; }

        /// <summary>
        /// 根骨
        /// </summary>
        public int Bone { get; set; }

        /// <summary>
        /// 魅力
        /// </summary>
        public int Charm { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 所属势力
        /// </summary>
        public string Forces { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// 气运
        /// </summary>
        public int Luck { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// 处事立场
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 气海
        /// </summary>
        public int Qi { get; set; }

        /// <summary>
        /// 角色立场
        /// </summary>
        public RoleType roleType { get; set; }

        /// <summary>
        /// 角色技能
        /// </summary>
        //  public List<Skill> Skills { get; set; }

        /// <summary>
        /// 势力地位
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 悟性
        /// </summary>
        public int Understanding { get; set; }

        /// <summary>
        /// 获取角色年龄
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public int Age(DateTime nowTime)
        {
            return this.Birthday.Year - nowTime.Year;
        }

        public string GetName()
        {
            return this.FamilyName + this.Name;
        }

        #region 战斗相关

        /// <summary>
        /// 当前生命值
        /// </summary>
        private int currentHP;

        /// <summary>
        /// 角色行动点
        /// </summary>
        public int ActionPoint { get; set; }

        /// <summary>
        /// 当前生命值
        /// </summary>
        public int CurrentHP
        {
            get
            {
                return this.currentHP;
            }
            set
            {
                if (value <= this.MaxHP)
                {
                    this.currentHP = value;
                }
                else if (value > this.MaxHP)
                {
                    this.currentHP = this.MaxHP;
                }
            }
        }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// 护盾
        /// </summary>
        public int Shield { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 战斗初始化
        /// </summary>
        public void CombatInit()
        {
            this.Power = this.Bone * 2;
            this.Speed = 100;
            this.ActionPoint = 3;
            this.Shield = (int)(this.Qi * 0.5);
        }

        #endregion 战斗相关
    }
}