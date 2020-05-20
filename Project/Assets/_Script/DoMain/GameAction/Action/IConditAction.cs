namespace OurGameName.DoMain.GameAction.Action
{
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.DoMain.GameAction.Config.Action;
    using static OurGameName.DoMain.GameAction.Args.ActionID;

    /// <summary>
    /// 游戏动作条件接口
    /// <para>定义游戏道具与技能执行所需的条件</para>
    /// </summary>
    internal interface IConditAction : IAction
    {
        /// <summary>
        /// 子条件
        /// </summary>
        List<IConditAction> Childs { get; }

        /// <summary>
        /// 父条件
        /// </summary>
        IConditAction Parent { get; }
    }

    /// <summary>
    /// 游戏动作条件接口
    /// <para>定义游戏道具与技能执行所需的条件</para>
    /// </summary>
    /// <typeparam name="T">动作配置参数的类型</typeparam>
    internal interface IConditAction<T> : IConditAction
    {
        /// <summary>
        /// 校验动作条件
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns>校验结果</returns>
        ActionConditResult CheckCondition(ReadonlyActionInputArgs<T> args);
    }

    /// <summary>
    /// 执行游戏动作基类
    /// <para>
    /// 实现游戏道具与技能具体效果的抽象父类
    /// </para>
    /// </summary>
    /// <typeparam name="T">动作配置参数的类型</typeparam>
    internal abstract class BaseConditAction<T> : IConditAction<T>
    {
        /// <summary>
        /// 执行游戏动作基类
        /// </summary>
        /// <param name="runType">游戏动作子类</param>
        /// <param name="id">游戏动作ID</param>
        /// <param name="childs">子条件</param>
        /// <param name="parent">父条件</param>
        protected BaseConditAction(
            ActionRunType runType,
            ConditActionName id,
            IConditAction parent = null,
            List<IConditAction> childs = null)
        {
            this.ID = new ActionID(ActionTypeCode.Condit, (byte)runType, (ushort)id);
            this.Parent = parent;
            this.Childs = childs;
        }

        /// <summary>
        /// 子条件
        /// </summary>
        public List<IConditAction> Childs { get; }

        /// <summary>
        /// 动作ID
        /// </summary>
        public ActionID ID { get; }

        /// <summary>
        /// 父条件
        /// </summary>
        public IConditAction Parent { get; }

        /// <summary>
        /// 动作UID
        /// </summary>
        public uint UID { get => this.ID.UID; }

        /// <summary>
        /// 校验动作条件
        /// </summary>
        /// <param name="args">动作输入参数</param>
        /// <returns>校验结果</returns>
        public abstract ActionConditResult CheckCondition(ReadonlyActionInputArgs<T> args);
    }
}