using System.Collections.Generic;

namespace OurGameName.DoMain.Entity.Combat
{
    public class Skill
    {
        /// <summary>
        /// 技能名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 技能CD 冷却时间
        /// </summary>
        public int CD { get; set; }

        /// <summary>
        /// 技能伤害 正值伤害 负值回复
        /// </summary>
        public int DMG { get; set; }

        /// <summary>
        /// 技能需要的行动点
        /// </summary>
        public byte ActionPoints { get; set; }

        /// <summary>
        /// 技能效果
        /// </summary>
        public List<string> Effect;

        /// <summary>
        /// 构建技能
        /// </summary>
        /// <param name="name">技能名</param>
        /// <param name="dmg">技能伤害 正值伤害 负值回复</param>
        /// <param name="cd">技能CD</param>
        /// <param name="actionPoints">技能需要的行动点</param>
        /// <param name="effect">技能效果</param>
        public Skill(string name, int dmg = 50, int cd = 2, byte actionPoints = 2, List<string> effect = null)
        {
            Name = name;
            CD = cd;
            DMG = dmg;
            ActionPoints = actionPoints;
            Effect = effect;
        }
    }
}