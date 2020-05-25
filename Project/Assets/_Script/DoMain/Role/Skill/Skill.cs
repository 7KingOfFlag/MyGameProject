namespace OurGameName.DoMain.RoleSpace.SkillSpace
{
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;

    /// <summary>
    /// 技能
    /// </summary>
    internal class Skill
    {
        /// <summary>
        /// 技能
        /// </summary>
        /// <param name="name">技能名字</param>
        public Skill(string name, List<IConditAction> conditionsActions, List<IExecuteAction> executionActions)
        {
            this.Name = name;
            this.ConditionsActions = conditionsActions;
            this.ExecutionActions = executionActions;
        }

        /// <summary>
        /// 技能名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 条件动作组
        /// </summary>
        private List<IConditAction> ConditionsActions { get; }

        /// <summary>
        /// 执行动作组
        /// </summary>
        private List<IExecuteAction> ExecutionActions { get; }

        /// <summary>
        /// 校验动作条件组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns></returns>
        public List<ActionConditResult> CheckConditAction(IReadonlyActionInputArgs args)
        {
            return this.ConditionsActions.Select(x => x.CheckCondition(args)).ToList();
        }

        /// <summary>
        /// 执行动作组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public void ExecutionAction(IActionInputArgs args)
        {
            this.ExecutionActions.ForEach(x => x.Execute(args));
        }
    }
}