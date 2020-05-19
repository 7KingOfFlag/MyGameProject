namespace OurGameName.DoMain.GameAction.Action
{
    using OurGameName.DoMain.GameAction.Args;

    /// <summary>
    /// 游戏动作接口
    /// </summary>
    internal interface IAction
    {
        /// <summary>
        /// 游戏动作ID
        /// </summary>
        ActionID ID { get; }
    }
}