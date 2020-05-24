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
            Contract.Requires(actions
                .Select((x, index) => x.GetType().GenericParametersIsSame(args[index].GetType()))
                .All(x => x == true));

            return actions.Select((x, index) => x.CheckCondition(args[index]));
        }

        public static void ExecuteAction(this IEnumerable<IExecuteAction> actions, IList<IActionInputArgs> args)
        {
            Contract.Requires(actions.Count() == args.Count());
            Contract.Requires(actions
                .Select((x, index) => x.GetType().GenericParametersIsSame(args[index].GetType()))
                .All(x => x == true));

            actions.ForEach((x, index) => x.Execute(args[index]));
        }

        /// <summary>
        /// 获取动作配置参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        public static TArgs GetActionConfigArgs<TArgs>(this IReadonlyActionInputArgs args)
        {
            Contract.Requires<ArgumentException>(args is IReadonlyActionInputArgs<TArgs> == true);
            return ((IReadonlyActionInputArgs<TArgs>)args).ActionConfigArgs;
        }

        /// <summary>
        /// 获取动作配置参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        public static TArgs GetActionConfigArgs<TArgs>(this IActionInputArgs args)
        {
            Contract.Requires<ArgumentException>(args is IActionInputArgs<TArgs> == true);
            return ((IActionInputArgs<TArgs>)args).ActionConifgArgs;
        }
    }
}