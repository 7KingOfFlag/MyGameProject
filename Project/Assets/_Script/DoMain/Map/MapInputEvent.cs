namespace OurGameName.DoMain.Map
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using OurGameName.DoMain.Map.Args;
    using OurGameName.Extension;
    using UnityEngine.UIElements;
    using OurGameName.DoMain.Map._2DMap;

    /// <summary>
    /// 地图输入事件
    /// </summary>
    internal class MapInputEvent : MonoBehaviour
    {
        /// <summary>
        /// 六边形地图
        /// </summary>
        public HexGrid context;

        /// <summary>
        /// 游戏输入
        /// </summary>
        public PlayerInput PlayerInput;

        public event EventHandler<MapInputEventArgs> NewClick;

        protected virtual void OnNewClick(MapInputEventArgs e)
        {
            e.Raise(this, ref NewClick);
        }

        private void Awake()
        {
            this.PlayerInput.actions["LeftClick"].performed += this.OnLeftClick;
            this.PlayerInput.actions["MiddleClick"].performed += this.OnMiddleClick;
            this.PlayerInput.actions["RighTClick"].performed += this.OnRighTClick;
        }

        private void OnLeftClick(InputAction.CallbackContext obj)
        {
            this.OnNewClick(new MapInputEventArgs(this.context.GetMouseCellPosition(), MouseButton.LeftMouse));
        }

        private void OnMiddleClick(InputAction.CallbackContext obj)
        {
            this.OnNewClick(new MapInputEventArgs(this.context.GetMouseCellPosition(), MouseButton.MiddleMouse));
        }

        private void OnRighTClick(InputAction.CallbackContext obj)
        {
            this.OnNewClick(new MapInputEventArgs(this.context.GetMouseCellPosition(), MouseButton.RightMouse));
        }
    }
}