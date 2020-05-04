using static OurGameName.DoMain.Entity.GameAction.ActionID;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTest")]

namespace OurGameName.DoMain.Entity.GameAction
{
    /// <summary>
    /// 游戏动作接口
    /// <para>定义游戏道具与技能具体效果的实现框架</para>
    /// </summary>
    internal interface IAction
    {
        /// <summary>
        /// 游戏动作ID
        /// </summary>
        ActionID ID { get; }

        /// <summary>
        /// 执行动作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ActionError Excute(ActionInput input);

        /// <summary>
        /// 读取动作需要的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ActionError Read(ActionInput input);
    }

    /// <summary>
    /// 游戏动作类
    /// <para>
    /// 实现游戏道具与技能具体效果的抽象父类
    /// </para>
    /// </summary>
    internal abstract class BaseAction : IAction
    {
        public ActionID ID { get; }

        public uint UID { get => ID.UID; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionType">游戏动作类型</param>
        /// <param name="runType">游戏动作子类</param>
        /// <param name="id">游戏动作ID</param>
        public BaseAction(ushort id, ActionTypeCode actionType, byte runType)
        {
            ID = new ActionID(actionType, runType, id);
        }

        public abstract ActionError Excute(ActionInput input);

        public virtual ActionError Read(ActionInput input)
        {
            return ActionError.NullError;
        }
    }

    public class ActionInput
    {
    }

    public enum ActionError
    {
        NullError
    }
}