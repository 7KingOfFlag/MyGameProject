namespace OurGameName.DoMain.GameAction.Args
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 游戏动作ID
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ActionID : IEqualityComparer<ActionID>
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
        private readonly byte action;

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
            this.action = (byte)actionType;
            this.RunType = runType;
            this.ID = id;
        }

        /// <summary>
        /// 游戏动作类型
        /// </summary>
        internal ActionTypeCode ActionType
        {
            get { return (ActionTypeCode)this.action; }
        }

        public enum ActionTypeCode
        {
            /// <summary>
            /// 条件类动作
            /// </summary>
            Condit,

            /// <summary>
            /// 执行类动作
            /// </summary>
            Execute
        }

        public static bool operator !=(ActionID x, ActionID y)
        {
            return x.UID != y.UID;
        }

        public static bool operator ==(ActionID x, ActionID y)
        {
            return x.UID == y.UID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is ActionID == false)
            {
                return false;
            }

            return ((ActionID)obj).UID == this.UID;
        }

        public override int GetHashCode()
        {
            return this.UID.GetHashCode();
        }

        public bool Equals(ActionID x, ActionID y)
        {
            return x == y;
        }

        public int GetHashCode(ActionID obj)
        {
            return obj.GetHashCode();
        }
    }
}