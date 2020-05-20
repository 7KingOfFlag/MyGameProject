namespace OurGameName.DoMain.GameAction.Args
{
    using Boo.Lang;
    using OurGameName.DoMain.RoleSpace;

    internal interface IActionInputArgs
    {
        /// <summary>
        /// 动作目标
        /// </summary>
        List<Role> Targets { get; set; }

        /// <summary>
        /// 动作使用者
        /// </summary>
        Role User { get; set; }
    }

    /// <summary>
    /// 动作输入参数
    /// </summary>
    internal class ActionInputArgs<T> : IActionInputArgs
    {
        /// <summary>
        /// 动作输入参数
        /// </summary>
        /// <param name="user">动作使用者</param>
        /// <param name="targets">动作目标</param>
        /// <param name="actionConifArgs">动作配置参数</param>
        public ActionInputArgs(
            Role user,
            List<Role> targets,
            T actionConifArgs)
        {
            this.User = user;
            this.Targets = targets;
            this.ActionConifArgs = actionConifArgs;
        }

        /// <summary>
        /// 动作配置参数
        /// </summary>
        public T ActionConifArgs { get; private set; }

        /// <summary>
        /// 动作目标
        /// </summary>
        public List<Role> Targets { get; set; }

        /// <summary>
        /// 动作使用者
        /// </summary>
        public Role User { get; set; }
    }

    /// <summary>
    /// 只读动作输入参数
    /// </summary>
    internal sealed class ReadonlyActionInputArgs<T>
    {
        /// <summary>
        /// 只读动作输入参数
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public ReadonlyActionInputArgs(ActionInputArgs<T> args)
        {
            this.User = args.User;
            this.Targets = args.Targets;
            this.ActionConfigArgs = args.ActionConifArgs;
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