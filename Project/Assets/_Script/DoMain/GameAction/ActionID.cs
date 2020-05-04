using System.Runtime.InteropServices;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTest")]

namespace OurGameName.DoMain.Entity.GameAction
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ActionID
    {
        /// <summary>
        /// 游戏动作唯一识别码
        /// </summary>
        [FieldOffset(0)]
        internal readonly uint UID;

        /// <summary>
        /// 游戏动作类型
        /// </summary>
        [FieldOffset(0)]
        private readonly byte m_action;

        /// <summary>
        /// 游戏动作子类
        /// </summary>
        [FieldOffset(1)]
        internal readonly byte RunType;

        /// <summary>
        /// 游戏动作ID
        /// </summary>
        [FieldOffset(2)]
        internal readonly ushort ID;

        /// <summary>
        /// 游戏动作ID
        /// </summary>
        /// <param name="actionType">游戏动作类型</param>
        /// <param name="runType">游戏动作子类</param>
        /// <param name="id">游戏动作ID</param>
        public ActionID(ActionTypeCode actionType, byte runType, ushort id) : this()
        {
            m_action = (byte)actionType;
            RunType = runType;
            ID = id;
        }

        /// <summary>
        /// 游戏动作类型
        /// </summary>
        internal ActionTypeCode ActionType
        {
            get { return (ActionTypeCode)m_action; }
        }

        public enum ActionTypeCode
        {
            /// <summary>
            /// 条件类动作
            /// </summary>
            Conditions,

            /// <summary>
            /// 执行类动作
            /// </summary>
            Execution
        }
    }
}