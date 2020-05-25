namespace OurGameName.DoMain.GameAction.Config.ActionEvent.Default
{
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.ActionEvent;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.DoMain.GameAction.Config.Action.ExecuteAction;
    using OurGameName.DoMain.GameAction.Config.ActionEvent;

    /// <summary>
    /// 斩击动作事件
    /// </summary>
    internal sealed class ChopActionEvent : BaseActionEvent
    {
        /// <summary>
        /// 斩击动作事件
        /// </summary>
        public ChopActionEvent()
            : base(new ActionEventID(DefaultActionEventName.Chop))
        {
            this.ExecutionActions = new List<IExecuteAction>
            {
                new DamageAction(){ ActionConfig = 10},
            };
        }
    }
}