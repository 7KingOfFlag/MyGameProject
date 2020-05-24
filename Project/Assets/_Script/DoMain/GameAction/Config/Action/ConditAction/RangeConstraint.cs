namespace OurGameName.DoMain.GameAction.Config.Action.ConditAction
{
    using System.Diagnostics.Contracts;
    using System.Linq;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.GameAction.Action;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.DoMain.Map.Extensions;
    using OurGameName.DoMain.RoleSpace;

    /// <summary>
    /// 范围约束
    /// </summary>
    internal sealed class RangeConstraint : BaseConditAction<int>
    {
        /// <summary>
        /// 范围约束
        /// </summary>
        public RangeConstraint() : base(ActionRunType.Base, ConditActionName.Range)
        {
        }

        /// <summary>
        /// 行动点约束
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns>行动点是否足够</returns>
        public override ActionConditResult CheckCondition(IReadonlyActionInputArgs args)
        {
            Contract.Requires(args.User != null);
            Contract.Requires(args.Targets.Count > 0);
            Contract.Requires(args.Targets.All(x => x != null));

            var range = args.User.Position.Value.GetCellInRange(args.GetActionConfigArgs<int>()).ToVector3Int();
            var result = args.Targets
                .Select(x => (canExecute: range.Contains(x.Position.Value), role: x))
                .Select(x => (x.canExecute, msg: x.canExecute ? this.GetTrueMsg(x.role) : this.GetFalseMsg(x.role)))
                .Select(x => new ActionConditResult(x.canExecute, x.msg));

            return new ActionConditResult(result.All(x => x.CanExecute == true), childs: result);
        }

        /// <summary>
        /// 角色在射程内时的信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GetFalseMsg(Role role)
        {
            return $"{role.FullName}在射程内";
        }

        /// <summary>
        ///角色在射程外时的信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GetTrueMsg(Role role)
        {
            return $"{role.FullName}在射程外";
        }
    }
}