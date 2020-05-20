namespace OurGameName.DoMain.GameAction.Config.Action.ConditAction
{
    using System.Diagnostics.Contracts;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;

    /// <summary>
    /// 行动点约束
    /// </summary>
    internal sealed class ActionPointConstraint : BaseConditAction<int>
    {
        /// <summary>
        /// 行动点约束
        /// </summary>
        public ActionPointConstraint() : base(ActionRunType.Base, ConditActionName.ActionPoint)
        {
        }

        /// <summary>
        /// 行动点约束
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns>行动点是否足够</returns>
        public override ActionConditResult CheckCondition(ReadonlyActionInputArgs<int> args)
        {
            Contract.Requires(args.User != null);

            int cost = args.ActionConfigArgs;
            bool canExecute = args.User.ActionPoint >= cost;

            return new ActionConditResult(
                canExecute,
                canExecute ? string.Empty : $"角色至少需要{cost}行动点,目前仅有{args.User.ActionPoint}");
        }
    }
}