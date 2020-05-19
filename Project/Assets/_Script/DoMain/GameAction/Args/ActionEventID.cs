namespace OurGameName.DoMain.GameAction.Args
{
    using System.Collections.Generic;

    /// <summary>
    /// 游戏动作事件ID
    /// </summary>
    internal struct ActionEventID : IEqualityComparer<ActionEventID>
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <param name="uid"></param>
        public ActionEventID(uint uid)
        {
            this.UID = uid;
        }

        /// <summary>
        /// 游戏动作事件ID
        /// </summary>
        public uint UID { get; private set; }

        public static bool operator !=(ActionEventID x, ActionEventID y)
        {
            return x.UID != y.UID;
        }

        public static bool operator ==(ActionEventID x, ActionEventID y)
        {
            return x.UID == y.UID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is ActionEventID == false)
            {
                return false;
            }

            return ((ActionEventID)obj).UID == this.UID;
        }

        public bool Equals(ActionEventID x, ActionEventID y)
        {
            return x == y;
        }

        public override int GetHashCode()
        {
            return this.UID.GetHashCode();
        }

        public int GetHashCode(ActionEventID obj)
        {
            return obj.GetHashCode();
        }
    }
}