namespace OurGameName.DoMain.GameAction.Action
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.General.Extension;

    /// <summary>
    /// 游戏动作扩展方法
    /// </summary>
    internal static class ActionExtensions
    {
        public static IEnumerable<ActionConditResult> CheckConditAction(
                    this IEnumerable<IConditAction> actions,
                    IList<IReadonlyActionInputArgs> args)
        {
            Contract.Requires(actions.Count() == args.Count());

            return actions.Select((x, index) => x.CheckCondition(args[index]));
        }

        public static void ExecuteAction(this IEnumerable<IExecuteAction> actions, IList<IActionInputArgs> args)
        {
            Contract.Requires(actions.Count() == args.Count());

            actions.ForEach((x, index) => x.Execute(args[index]));
        }
    }
}