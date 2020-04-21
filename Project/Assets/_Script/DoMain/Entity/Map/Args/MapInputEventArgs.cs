namespace OurGameName.DoMain.Entity.Map.Args
{
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>
    /// 地图输入事件参数
    /// </summary>
    public class MapInputEventArgs : EventArgs
    {
        public MapInputEventArgs(Vector2Int ClickPosition, MouseButton ClickButtomCoed)
        {
            this.ClickPosition = ClickPosition;
            this.ClickButtomCoed = ClickButtomCoed;
        }

        /// <summary>
        /// 点击按钮代码
        /// </summary>
        public MouseButton ClickButtomCoed { get; }

        /// <summary>
        /// 点击位置
        /// </summary>
        public Vector2Int ClickPosition { get; }
    }
}