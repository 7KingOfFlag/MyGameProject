namespace OurGameName.DoMain.GameAction.Args
{
    using System.Collections.Generic;

    /// <summary>
    /// 游戏动作条件校验返回值
    /// </summary>
    internal class ActionConditResult
    {
        /// <summary>
        /// 游戏动作条件校验返回值
        /// </summary>
        /// <param name="canExcute">能否执行</param>
        /// <param name="msg">附加消息</param>
        /// <param name="parent">父返回值</param>
        /// <param name="childs">子返回值</param>
        public ActionConditResult(
                    bool canExcute,
                    string msg = "",
                    ActionConditResult parent = null,
                    IEnumerable<ActionConditResult> childs = null)
        {
            this.CanExecute = canExcute;
            this.Msg = msg;
            this.Parent = parent;
            this.Childs = childs;
        }

        /// <summary>
        /// 能否执行
        /// </summary>
        public bool CanExecute { get; set; }

        /// <summary>
        /// 子返回值
        /// </summary>
        public IEnumerable<ActionConditResult> Childs { get; }

        /// <summary>
        /// 附加消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 父返回值
        /// </summary>
        public ActionConditResult Parent { get; }
    }
}