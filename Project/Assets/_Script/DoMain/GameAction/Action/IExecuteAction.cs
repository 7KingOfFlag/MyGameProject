namespace OurGameName.DoMain.GameAction.Action
{
    using OurGameName.DoMain.GameAction.Args;
    using static OurGameName.DoMain.GameAction.Args.ActionID;

    /// <summary>
    /// 游戏执行动作接口
    /// <para>定义游戏道具与技能具体效果的实现框架</para>
    /// </summary>
    internal interface IExecuteAction : IAction
    {
        /// <summary>
        /// 执行动作
        /// </summary>
        /// <param name="input">动作输入参数</param>
        void Execute(ActionInputArgs args);
    }

    /// <summary>
    /// 执行游戏动作基类
    /// <para>
    /// 实现游戏道具与技能具体效果的抽象父类
    /// </para>
    /// </summary>
    internal abstract class BaseExecuteAction : IExecuteAction
    {
        /// <summary>
        /// 执行游戏动作基类
        /// </summary>
        /// <param name="runType">游戏动作子类</param>
        /// <param name="id">游戏动作ID</param>
        public BaseExecuteAction(ushort id, byte runType)
        {
            this.ID = new ActionID(ActionTypeCode.Execute, runType, id);
        }

        /// <summary>
        /// 动作ID
        /// </summary>
        public ActionID ID { get; }

        /// <summary>
        /// 动作UID
        /// </summary>
        public uint UID { get => this.ID.UID; }

        /// <summary>
        /// 执行动作
        /// </summary>
        /// <param name="args">动作输入参数</param>
        public abstract void Execute(ActionInputArgs args);
    }
}