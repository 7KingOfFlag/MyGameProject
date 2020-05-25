namespace OurGameName.DoMain.GameAction.Args
{
    using System.Collections.Generic;
    using OurGameName.DoMain.RoleSpace;

    /// <summary>
    /// 动作输入参数接口
    /// </summary>
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
    internal class ActionInputArgs : IActionInputArgs
    {
        /// <summary>
        /// 动作输入参数
        /// </summary>
        /// <param name="user">动作使用者</param>
        /// <param name="targets">动作目标</param>
        /// <param name="actionConifArgs">动作配置参数</param>
        public ActionInputArgs(Role user, List<Role> targets)
        {
            this.User = user;
            this.Targets = targets;
        }

        /// <summary>
        /// 动作目标
        /// </summary>
        public List<Role> Targets { get; set; }

        /// <summary>
        /// 动作使用者
        /// </summary>
        public Role User { get; set; }
    }
}