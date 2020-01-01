using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    public class HexTileInputEventArgs : EventArgs
    {
        private readonly Vector2Int m_ClickPosition;
        private readonly MouseButton m_ClickButtomCoed;

        public HexTileInputEventArgs(Vector2Int ClickPosition, MouseButton ClickButtomCoed)
        {
            this.m_ClickPosition = ClickPosition;
            this.m_ClickButtomCoed = ClickButtomCoed;
        }

        /// <summary>
        /// 点击位置
        /// </summary>
        public Vector2Int ClickPosition { get { return m_ClickPosition; } }

        /// <summary>
        /// 点击按钮代码
        /// </summary>
        public MouseButton ClickButtomCoed { get { return m_ClickButtomCoed; } }
    }
}