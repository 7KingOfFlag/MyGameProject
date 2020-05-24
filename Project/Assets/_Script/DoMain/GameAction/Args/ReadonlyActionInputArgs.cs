namespace OurGameName.DoMain.GameAction.Args
{
    using System;
    using System.Diagnostics.Contracts;
    using Boo.Lang;
    using OurGameName.DoMain.RoleSpace;

    /// <summary>
    /// 只读动作输入参数接口
    /// </summary>
    internal interface IReadonlyActionInputArgs
    {
        /// <summary>
        /// 动作目标
        /// </summary>
        List<Role> Targets { get; }

        /// <summary>
        /// 动作使用者
        /// </summary>
        Role User { get; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IReadonlyActionInputArgs<T> : IReadonlyActionInputArgs
    {
        /// <summary>
        /// 动作配置参数
        /// </summary>
        T ActionConfigArgs { get; }
    }

    /// <summary>
    /// 只读动作输入参数
    /// </summary>
    internal sealed class ReadonlyActionInputArgs<T> : IReadonlyActionInputArgs
    {
        /// <summary>
        /// 只读动作输入参数
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public ReadonlyActionInputArgs(ActionInputArgs<T> args)
        {
            this.User = args.User;
            this.Targets = args.Targets;
            this.ActionConfigArgs = args.ActionConifgArgs;
        }

        /// <summary>
        /// 只读动作输入参数
        /// </summary>
        /// <param name="user">动作使用者</param>
        /// <param name="targets">动作目标</param>
        /// <param name="actionConfigfArgs">动作配置参数</param>
        public ReadonlyActionInputArgs(
            Role user,
            List<Role> targets,
            T actionConfigfArgs)
        {
            this.User = user;
            this.Targets = targets;
            this.ActionConfigArgs = actionConfigfArgs;
        }

        /// <summary>
        /// 动作配置参数
        /// </summary>
        public T ActionConfigArgs { get; }

        /// <summary>
        /// 动作目标
        /// </summary>
        public List<Role> Targets { get; }

        /// <summary>
        /// 动作使用者
        /// </summary>
        public Role User { get; }
    }
}