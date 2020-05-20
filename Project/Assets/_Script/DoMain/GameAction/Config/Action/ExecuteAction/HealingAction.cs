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

        /// <summary>
        /// 恢复目标角色生命值 不会超过最大生值
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public override void Execute(ActionInputArgs<int> args)
        {
            args.Targets.ForEach(x => x.HP.Value += args.ActionConifArgs);
        }
    }
}