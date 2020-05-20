namespace OurGameName.DoMain.GameAction.ActionEvent
{
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;

    /// <summary>
    /// 游戏动作事件基类
    /// </summary>
    internal abstract class BaseActionEvent : IActionEvent
    {
        /// <summary>
        /// 游戏动作事件ID
        /// </summary>
        /// <param name="id">游戏动作ID</param>
        /// <param name="conditionsActions">条件动作组</param>
        /// <param name="executionACtions">执行动作组</param>
        public BaseActionEvent(
            ActionEventID uid,
            IConditAction[] conditionsActions,
            IExecuteAction[] executionACtions)
        {
            this.UID = uid;
            this.ConditionsActions = conditionsActions;
            this.ExecutionActions = executionACtions;
        }

        /// <summary>
        /// 条件动作组
        /// </summary>
        public IConditAction[] ConditionsActions { get; }

        /// <summary>
        /// 执行动作组
        /// </summary>
        public IExecuteAction[] ExecutionActions { get; }

        /// <summary>
        /// 动作事件唯一识别码
        /// </summary>
        public ActionEventID UID { get; }

        /// <summary>
        /// 校验动作条件组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns></returns>
        public abstract ActionConditResult[] CheckConditAction(IActionInputArgs[] args);

        /// <summary>
        /// 执行动作组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public abstract void ExecutionAction(IActionInputArgs[] args);
    }
}