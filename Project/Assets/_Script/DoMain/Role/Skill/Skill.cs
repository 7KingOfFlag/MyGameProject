namespace OurGameName.DoMain.RoleSpace.Skill
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OurGameName.DoMain.Entity;
    using OurGameName.DoMain.GameAction.ActionEvent;

    internal class Skill
    {
        /// <summary>
        /// 行动点费用
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// 技能名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 技能效果
        /// </summary>
        public ActionEvent SkillEffect { get; set; }

        /// <summary>
        /// 技能影响范围参数
        /// </summary>
        public SkillRangeArgs SkillRangeArgs { get; set; }
    }
}