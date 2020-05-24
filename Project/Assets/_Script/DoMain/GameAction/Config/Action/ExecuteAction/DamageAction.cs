namespace OurGameName.DoMain.GameAction.Config.Action.ExecuteAction
{
    using System;
    using System.Diagnostics.Contracts;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.General.Extension;

    /// <summary>
    /// 伤害动作
    /// </summary>
    internal class DamageAction : BaseExecuteAction<int>
    {
        public DamageAction() : base(ActionRunType.Base, ExecuteActionName.Damage)
        { }

        public override void Execute(IActionInputArgs args)
        {
            Contract.Requires<ArgumentException>(args is IActionInputArgs<int> == true);
            args.Targets.ForEach(x => x.HP.Value -= args.GetActionConfigArgs<int>());
        }
    }
}