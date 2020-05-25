namespace OurGameName.DoMain.GameAction.Config.ActionEvent
{
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.ActionEvent;
    using OurGameName.DoMain.GameAction.Config.Action.ConditAction;
    using OurGameName.DoMain.GameAction.Config.Action.ExecuteAction;
    using OurGameName.DoMain.GameAction.Config.ActionEvent.Default;
    using OurGameName.DoMain.RoleSpace.SkillSpace;

    /// <summary>
    ///  游戏动作事件注册器
    /// </summary>
    internal class ActionEventRegistrar
    {
        /// <summary>
        /// 游戏动作事件注册器
        /// </summary>
        internal ActionEventRegistrar()
        {
        }

        /// <summary>
        /// 获取默认技能
        /// </summary>
        /// <returns></returns>
        public static List<Skill> GetDefaultSkill()
        {
            List<IConditAction> defaultCondit = new List<IConditAction>()
            {
                new ActionPointConstraint(){ ActionConfig = 1},
                new RangeConstraint(){ ActionConfig = 2},
            };

            List<IExecuteAction> chop = new List<IExecuteAction>()
            {
                new DamageAction(){ ActionConfig = 10 },
            };

            List<IExecuteAction> cureWounds = new List<IExecuteAction>()
            {
                new HealingAction(){ ActionConfig = 10},
            };

            return new List<Skill>
            {
                new Skill("斩击", defaultCondit,chop ),
                new Skill("治疗术", defaultCondit, cureWounds)
            };
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void Registered()
        {
            var container = ActionEventContainer.Instance;
            container.Registered<ChopActionEvent>();
            container.Registered<CureWoundsActionEvent>();
        }
    }
}