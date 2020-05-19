namespace OurGameName.DoMain.GameAction.ActionEvent
{
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.Extension;
    using System.Linq;

    /// <summary>
    /// 游戏动作事件
    /// </summary>
    internal class ActionEvent
    {
        /// <summary>
        /// 游戏动作事件ID
        /// </summary>
        /// <param name="id">游戏动作ID</param>
        /// <param name="conditionsActions">条件动作组</param>
        /// <param name="executionACtions">执行动作组</param>
        public ActionEvent(
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
        /// <param name="args"></param>
        /// <returns></returns>
        public ActionConditResult[] CheckConditAction(ReadonlyActionInputArgs args)
        {
            return this.ConditionsActions.Select(x => x.CheckCondition(args)).ToArray();
        }

        /// <summary>
        /// 执行动作组
        /// </summary>
        /// <param name="args"></param>
        public void ExecutionAction(ActionInputArgs args)
        {
            this.ExecutionActions.ForEach(x => x.Execute(args));
        }
    }
}