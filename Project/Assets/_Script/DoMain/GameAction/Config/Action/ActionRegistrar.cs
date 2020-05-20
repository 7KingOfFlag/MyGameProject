namespace OurGameName.DoMain.GameAction.Config.Action
{
    using System;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Config.Action.ConditAction;
    using OurGameName.DoMain.GameAction.Config.Action.ExecuteAction;

    /// <summary>
    /// 游戏动作注册器
    /// </summary>
    public sealed class ActionRegistrar
    {
        /// <summary>
        /// 游戏动作注册器
        /// </summary>
        public ActionRegistrar()
        {
            var container = ActionContainer.Instance;
            this.RegisteredExecuteAction(container);
            this.RegisteredConditAction(container);
        }

        /// <summary>
        /// 注册条件类动作
        /// </summary>
        /// <param name="container">注册容器</param>
        private void RegisteredConditAction(ActionContainer container)
        {
            container.Registered<ActionPointConstraint>();
        }

        /// <summary>
        /// 注册执行类动作
        /// </summary>
        /// <param name="container">注册容器</param>
        private void RegisteredExecuteAction(ActionContainer container)
        {
            container.Registered<DamageAction>();
            container.Registered<HealingAction>();
        }
    }
}