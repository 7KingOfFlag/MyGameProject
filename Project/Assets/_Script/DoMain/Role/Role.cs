namespace OurGameName.DoMain.RoleSpace
{
    using System;

    /// <summary>
    /// 角色类
    /// </summary>
    internal class Role : Organism
    {
        private static int IDCount = 0;

        /// <summary>
        /// 创建角色实例(详细)
        /// </summary>
        /// <param name="familyName">姓</param>
        /// <param name="name">名</param>
        /// <param name="birthday">生日</param>
        /// <param name="actionPoint"></param>
        /// <param name="hp"></param>
        /// <param name="armor">护甲</param>
        /// <param name="shield"></param>
        public Role(
            string familyName,
            string name,
            DateTime birthday,
            int actionPoint,
            int hp,
            int armor = 0,
            int shield = 0)
        {
            this.ID = IDCount++;
            this.FamilyName = familyName;
            this.Name = name;
            this.Birthday = birthday;
            this.Speed = 100;
            this.MaxHP = hp;
            this.HP = hp;
            this.ActionPoint = actionPoint;
            this.Armor = armor;
            this.Shield = shield;
        }

        /// <summary>
        /// 反序列化用构造函数
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// 出生时间
        /// </summary>
        public DateTime Birthday { get; private set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色技能
        /// </summary>
        //  public List<Skill> Skills { get; set; }

        /// <summary>
        /// 获取角色年龄
        /// </summary>
        /// <param name="nowTime">当前时间</param>
        /// <returns></returns>
        public int Age(DateTime nowTime)
        {
            return this.Birthday.Year - nowTime.Year;
        }

        /// <summary>
        /// 获取角色名字
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.FamilyName + this.Name;
        }

        #region 战斗相关

        /// <summary>
        /// 角色行动点
        /// </summary>
        public int ActionPoint { get; set; }

        /// <summary>
        /// 护甲
        /// </summary>
        public int Armor { get; set; }

        /// <summary>
        /// 护盾
        /// </summary>
        public int Shield { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed { get; set; }

        #endregion 战斗相关
    }
}