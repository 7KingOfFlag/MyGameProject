namespace OurGameName.DoMain.GameAction.ActionEvent
{
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;

    /// <summary>
    /// 游戏动作事件接口
    /// </summary>
    internal interface IActionEvent
    {
        /// <summary>
        /// 条件动作组
        /// </summary>
        List<IConditAction> ConditionsActions { get; }

        /// <summary>
        /// 执行动作组
        /// </summary>
        List<IExecuteAction> ExecutionActions { get; }

        /// <summary>
        /// 动作事件唯一识别码
        /// </summary>
        ActionEventID UID { get; }

        /// <summary>
        /// 校验动作条件组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns></returns>
        List<ActionConditResult> CheckConditAction(List<IReadonlyActionInputArgs> args);

        /// <summary>
        /// 执行动作组
        /// </summary>
        /// <param name="args">动作输入参数</param>
        void ExecutionAction(List<IActionInputArgs> args);
    }
}