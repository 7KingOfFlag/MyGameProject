using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OurGameName.DoMain.Entity.GameAction.ActionID;

namespace OurGameName.DoMain.Entity.GameAction
{
    /// <summary>
    /// 游戏动作事件
    /// </summary>
    internal class ActionEvent
    {
        /// <summary>
        /// 动作事件ID
        /// </summary>
        public ActionID ID { get; }
        /// <summary>
        /// 动作事件唯一识别码
        /// </summary>
        public uint UID { get => ID.UID; }

        /// <summary>
        /// 条件动作组
        /// </summary>
        public uint[] ConditionsActions { get; set; }
        /// <summary>
        /// 执行动作组
        /// </summary>
        public uint[] ExecutionACtions { get; set; }

        /// <summary>
        /// 游戏动作事件ID
        /// </summary>
        /// <param name="actionType">游戏动作类型</param>
        /// <param name="runType">游戏动作子类</param>
        /// <param name="id">游戏动作ID</param>
        /// <param name="conditionsActions">条件动作组</param>
        /// <param name="executionACtions">执行动作组</param>
        public ActionEvent(ActionTypeCode actionType, byte runType, ushort id,
                           uint[] conditionsActions, uint[] executionACtions)
        {
            ID = new ActionID(actionType, runType, id);
            ConditionsActions = conditionsActions ?? throw new ArgumentNullException(nameof(conditionsActions));
            ExecutionACtions = executionACtions ?? throw new ArgumentNullException(nameof(executionACtions));
        }
    }
}
