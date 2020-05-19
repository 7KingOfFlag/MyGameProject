namespace OurGameName.DoMain.GameAction.Config.Action.ExecuteAction
{
    using System.Diagnostics.Contracts;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.Extension;

    /// <summary>
    /// 恢复动作
    /// </summary>
    internal sealed class HealingAction : BaseExecuteAction
    {
        /// <summary>
        /// 恢复动作
        /// </summary>
        public HealingAction() : base(1, 0)
        { }

        /// <summary>
        /// 恢复目标角色生命值 不会超过最大生值
        /// <para>只取第一个配置参数的值</para>
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public override void Execute(ActionInputArgs args)
        {
            Contract.Requires(args.ActionConifArgs.Length == 1);

            args.Targets.ForEach(x => x.HP.Value += args.ActionConifArgs[0]);
        }
    }
}