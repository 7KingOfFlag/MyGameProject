using OurGameName.DoMain.Attribute;
using OurGameName.DoMain.Entity.Combat;
using System.Collections.Generic;
using System;

namespace OurGameName.DoMain.Entity.RoleSpace
{
    /// <summary>
    /// 角色类
    /// </summary>
    public class Role
    {
        private static int IDCount = 0;

        public int ID { get; private set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出生时间 格式####.##.##
        /// </summary>
        public string Birthday { get; private set; }
        /// <summary>
        /// 气海
        /// </summary>
        public int Qi { get; set; }
        /// <summary>
        /// 根骨
        /// </summary>
        public int Bone { get; set; }

        /// <summary>
        /// 悟性
        /// </summary>
        public int Understanding { get; set; }

        /// <summary>
        /// 魅力
        /// </summary>
        public int Charm { get; set; }

        /// <summary>
        /// 气运
        /// </summary>
        public int Luck { get; set; }

        /// <summary>
        /// 处事立场
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 所属势力
        /// </summary>
        public string Forces { get; set; }

        /// <summary>
        /// 势力地位
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// 角色技能
        /// </summary>
        public List<Skill> Skills { get; set; }

        /// <summary>
        /// 角色立场
        /// </summary>
        public RoleType roleType { get; set; }

        /// <summary>
        /// 创建角色实例(详细)
        /// </summary>
        /// <param name="familyName">姓</param>
        /// <param name="name">名</param>
        /// <param name="birthday">生日 格式 ####.##.##</param>
        /// <param name="qi">气海</param>
        /// <param name="bone">根骨</param>
        /// <param name="understanding">悟性</param>
        /// <param name="charm">魅力</param>
        /// <param name="luck">气运</param>
        /// <param name="position">处事立场 0名门正派 500 中庸 1000邪门歪道</param>
        /// <param name="forces">所属势力</param>
        /// <param name="status">势力地位</param>
        /// <param name="PicturePath">角色肖像图片相对地址，基于Assets文件夹</param>
        public Role(string familyName, string name, string birthday, int qi, int bone, int understanding, int charm,
                    int luck, int position, string forces, string status, string PicturePath = null ,RoleType roleType = RoleType.neutrality)
        {
            ID = IDCount++;
            FamilyName = familyName;
            Name = name;
            Birthday = birthday;
            Qi = qi;
            Bone = bone;
            Understanding = understanding;
            Charm = charm;
            Luck = luck;
            Position = position;
            Forces = forces;
            Status = status;
            this.PicturePath = PicturePath;
            this.roleType = roleType;
        }

        /// <summary>
        /// 测试用快速建立人物
        /// </summary>
        /// <param name="familyName">姓</param>
        /// <param name="name">名</param>
        /// <param name="PicturePath">角色头像</param>
        public Role(string familyName, string name, string PicturePath = null, RoleType roleType = RoleType.neutrality)
        {
            ID = IDCount++;
            FamilyName = familyName;
            Name = name;
            Birthday = "2018.01.01";
            Qi = 10;
            Bone = 10;
            Understanding = 100;
            Charm = 50;
            Luck = 50;
            Position = 0;
            Forces = "无";
            Status = "无";
            this.PicturePath = PicturePath;
            this.roleType = roleType;
        }

        /// <summary>
        /// 反序列化用构造函数
        /// </summary>
        public Role()
        {

        }

        /// <summary>
        /// 获取角色年龄
        /// </summary>
        /// <param name="nowYear">现在的年份</param>
        /// <returns></returns>
        public int Age(int nowYear)
        {
            int BirthYear = int.Parse(Birthday.Split(new char[] { '.' })[0]);
            return BirthYear - nowYear;
        }

        public string GetName()
        {
            return FamilyName + Name;
        }

        #region 战斗相关
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHP { get; set; }

        private int currentHP;
        /// <summary>
        /// 当前生命值
        /// </summary>
        public int CurrentHP
        {
            get
            {
                return currentHP;
            }
            set
            {
                if ( value <= MaxHP )
                {
                    currentHP = value;
                }
                else if(value > MaxHP)
                {
                    currentHP = MaxHP;
                }
            }
        }

        /// <summary>
        /// 力量
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 角色行动点
        /// </summary>
        public int ActionPoint { get; set; }

        /// <summary>
        /// 护盾
        /// </summary>
        public int Shield { get; set; }

        /// <summary>
        /// 战斗初始化
        /// </summary>
        public void CombatInit()
        {
            Power = Bone * 2;
            Speed = 100;
            ActionPoint = 3;
            Shield = (int)(Qi * 0.5);
        }


        #endregion
    }

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
}