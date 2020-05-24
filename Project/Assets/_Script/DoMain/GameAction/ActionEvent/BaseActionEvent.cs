namespace OurGameName.DoMain.GameAction.ActionEvent
{
    using System;
    using System.Linq;
    using System.Diagnostics.Contracts;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Config.Action.ConditAction;

    /// <summary>
    /// 游戏动作事件基类
    /// </summary>
    internal class BaseActionEvent : IActionEvent
    {
        /// <summary>
        /// 游戏动作事件ID
        /// </summary>
        /// <param name="uid">游戏动作ID</param>
        /// <param name="conditionsActions">条件动作组</param>
        /// <param name="conditActionConfig">条件动作条件配置参数</param>
        /// <param name="executionActions">执行动作组</param>
        /// <param name="executionActionConfig">执行动作组配置参数</param>
        public BaseActionEvent(
            ActionEventID uid,
            List<IReadonlyActionInputArgs> conditActionConfig = null,
            List<IConditAction> conditionsActions = null,
            List<IActionInputArgs> executionActionConfig = null,
            List<IExecuteAction> executionActions = null)
        {
            this.UID = uid;
            this.ConditActionConfig = conditActionConfig ?? new List<IReadonlyActionInputArgs>();
            this.ConditionsActions = conditionsActions ?? new List<IConditAction>
            {
                new ActionPointConstraint(),
                new RangeConstraint(),
            };
            this.ExecutionActionConfig = executionActionConfig ?? new List<IActionInputArgs>();
            this.ExecutionActions = executionActions ?? new List<IExecuteAction>();
        }

        /// <summary>
        /// 校验动作条件配置参数
        /// </summary>
        public List<IReadonlyActionInputArgs> ConditActionConfig { get; set; }

        /// <summary>
        /// 条件动作组
        /// </summary>
        public List<IConditAction> ConditionsActions { get; protected set; }

        /// <summary>
        /// 执行动作组配置参数
        /// </summary>
        public List<IActionInputArgs> ExecutionActionConfig { get; set; }

        /// <summary>
        /// 执行动作组
        /// </summary>
        public List<IExecuteAction> ExecutionActions { get; protected set; }

        /// <summary>
        /// 动作事件唯一识别码
        /// </summary>
        public ActionEventID UID { get; }

        /// <summary>
        /// 校验动作条件组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns></returns>
        public List<ActionConditResult> CheckConditAction()
        {
            Contract.Requires<ArgumentNullException>(this.ConditActionConfig != null);
            return this.ConditionsActions.CheckConditAction(this.ConditActionConfig).ToList();
        }

        /// <summary>
        /// 执行动作组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public void ExecutionAction()
        {
            Contract.Requires<ArgumentNullException>(this.ExecutionActionConfig != null);
            this.ExecutionActions.ExecuteAction(this.ExecutionActionConfig);
        }
    }
}