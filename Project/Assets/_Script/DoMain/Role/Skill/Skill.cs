namespace OurGameName.DoMain.RoleSpace.SkillSpace
{
    using OurGameName.DoMain.GameAction.ActionEvent;

    /// <summary>
    /// 技能
    /// </summary>
    internal class Skill
    {
        /// <summary>
        /// 技能
        /// </summary>
        /// <param name="cost">行动点费用</param>
        /// <param name="name">技能名字</param>
        /// <param name="skillEffect">技能效果</param>
        /// <param name="skillRangeArgs">技能影响范围参数</param>
        public Skill(
            int cost,
            string name,
            IActionEvent skillEffect,
            SkillRangeArgs skillRangeArgs)
        {
            this.Cost = cost;
            this.Name = name;
            this.SkillEffect = skillEffect;
            this.SkillRangeArgs = skillRangeArgs;
        }

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
        public IActionEvent SkillEffect { get; set; }

        /// <summary>
        /// 技能影响范围参数
        /// </summary>
        public SkillRangeArgs SkillRangeArgs { get; set; }
    }
}