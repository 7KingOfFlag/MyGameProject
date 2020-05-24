namespace OurGameName.DoMain.GameAction.Config.Action.ExecuteAction
{
    using System;
    using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentException>(args is IActionInputArgs<int> == true);

            args.Targets.ForEach(x => x.HP.Value += args.GetActionConfigArgs<int>());
        }
    }
}