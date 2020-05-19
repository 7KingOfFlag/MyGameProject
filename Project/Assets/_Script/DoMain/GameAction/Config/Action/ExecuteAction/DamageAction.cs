namespace OurGameName.DoMain.GameAction.Config.Action.ExecuteAction
{
    using System.Diagnostics.Contracts;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.Extension;

    /// <summary>
    /// 伤害动作
    /// </summary>
    internal class DamageAction : BaseExecuteAction
    {
        public DamageAction() : base(0, 0)
        { }

        /// <summary>
        /// 对目标角色造成指定伤害
        /// <para>只取第一个配置参数的值</para>
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public override void Execute(ActionInputArgs args)
        {
            Contract.Requires(args.ActionConifArgs.Length == 1);

            args.Targets.ForEach(x => x.HP.Value -= args.ActionConifArgs[0]);
        }
    }
}