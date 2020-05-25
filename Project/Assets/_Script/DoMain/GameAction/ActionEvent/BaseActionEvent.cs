namespace OurGameName.DoMain.GameAction.ActionEvent
{
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
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
        /// <param name="executionActions">执行动作组</param>
        public BaseActionEvent(
            ActionEventID uid,
            List<IConditAction> conditionsActions = null,
            List<IExecuteAction> executionActions = null)
        {
            this.UID = uid;
            this.ConditionsActions = conditionsActions ?? new List<IConditAction>
            {
                new ActionPointConstraint(){ ActionConfig = 2},
                new RangeConstraint(){ ActionConfig = 2},
            };
            this.ExecutionActions = executionActions ?? new List<IExecuteAction>();
        }

        /// <summary>
        /// 条件动作组
        /// </summary>
        public List<IConditAction> ConditionsActions { get; protected set; }

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
        public List<ActionConditResult> CheckConditAction(List<IReadonlyActionInputArgs> args)
        {
            return this.ConditionsActions.CheckConditAction(args).ToList();
        }

        /// <summary>
        /// 执行动作组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public void ExecutionAction(List<IActionInputArgs> args)
        {
            this.ExecutionActions.ExecuteAction(args);
        }
    }
}