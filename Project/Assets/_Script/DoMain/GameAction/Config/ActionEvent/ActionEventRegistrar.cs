namespace OurGameName.DoMain.GameAction.Config.ActionEvent
{
    using OurGameName.DoMain.GameAction.ActionEvent;
    using OurGameName.DoMain.GameAction.Config.ActionEvent.Default;

    /// <summary>
    ///  游戏动作事件注册器
    /// </summary>
    internal class ActionEventRegistrar
    {
        /// <summary>
        /// 游戏动作事件注册器
        /// </summary>
        internal ActionEventRegistrar()
        {
            var container = ActionEventContainer.Instance;
            container.Registered<ChopActionEvent>();
            container.Registered<CureWoundsActionEvent>();
        }
    }
}