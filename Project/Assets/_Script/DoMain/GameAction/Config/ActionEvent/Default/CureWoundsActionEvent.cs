namespace OurGameName.DoMain.GameAction.Config.ActionEvent.Default
{
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.ActionEvent;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.DoMain.GameAction.Config.Action.ConditAction;
    using OurGameName.DoMain.GameAction.Config.Action.ExecuteAction;

    /// <summary>
    /// 疗伤术
    /// </summary>
    internal sealed class CureWoundsActionEvent : BaseActionEvent
    {
        /// <summary>
        /// 疗伤术
        /// </summary>
        public CureWoundsActionEvent() : base(new ActionEventID(DefaultActionEventName.CureWounds))
        {
            this.ExecutionActions = new List<IExecuteAction>
            {
                new HealingAction(),
            };
        }
    }
}