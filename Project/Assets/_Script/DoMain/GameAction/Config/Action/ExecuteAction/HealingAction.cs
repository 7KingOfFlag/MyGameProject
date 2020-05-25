namespace OurGameName.DoMain.GameAction.Config.Action.ExecuteAction
{
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.General.Extension;

    /// <summary>
    /// 恢复动作
    /// </summary>
    internal sealed class HealingAction : BaseExecuteAction<int>
    {
        /// <summary>
        /// 恢复动作
        /// </summary>
        public HealingAction() : base(0, ExecuteActionName.Healing)
        { }

        public override void Execute(IActionInputArgs args)
        {
            args.Targets.ForEach(x => x.HP.Value += this.ActionConfig);
        }
    }
}