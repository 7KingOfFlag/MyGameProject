namespace OurGameName.DoMain.Entity.TileHexMap
{
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class HexTileInputEventArgs : EventArgs
    {
        private readonly MouseButton m_ClickButtomCoed;
        private readonly Vector2Int m_ClickPosition;

        public HexTileInputEventArgs(Vector2Int ClickPosition, MouseButton ClickButtomCoed)
        {
            this.m_ClickPosition = ClickPosition;
            this.m_ClickButtomCoed = ClickButtomCoed;
        }

        /// <summary>
        /// 点击按钮代码
        /// </summary>
        public MouseButton ClickButtomCoed { get { return m_ClickButtomCoed; } }

        /// <summary>
        /// 点击位置
        /// </summary>
        public Vector2Int ClickPosition { get { return m_ClickPosition; } }
    }
}