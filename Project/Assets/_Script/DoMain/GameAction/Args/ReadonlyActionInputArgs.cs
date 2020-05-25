namespace OurGameName.DoMain.GameAction.Args
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
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
    /// 只读动作输入参数
    /// </summary>
    internal sealed class ReadonlyActionInputArgs : IReadonlyActionInputArgs
    {
        /// <summary>
        /// 只读动作输入参数
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public ReadonlyActionInputArgs(ActionInputArgs args)
        {
            this.User = args.User;
            this.Targets = args.Targets;
        }

        /// <summary>
        /// 只读动作输入参数
        /// </summary>
        /// <param name="user">动作使用者</param>
        /// <param name="targets">动作目标</param>
        /// <param name="actionConfigfArgs">动作配置参数</param>
        public ReadonlyActionInputArgs(Role user, List<Role> targets)
        {
            this.User = user;
            this.Targets = targets;
        }

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