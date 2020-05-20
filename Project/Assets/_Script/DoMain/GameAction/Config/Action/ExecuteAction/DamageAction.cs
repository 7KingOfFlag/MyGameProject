namespace OurGameName.DoMain.GameAction.Config.Action.ExecuteAction
{
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

        /// <summary>
        /// 对目标角色造成指定伤害
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public override void Execute(ActionInputArgs<int> args)
        {
            args.Targets.ForEach(x => x.HP.Value -= args.ActionConifArgs);
        }
    }
}