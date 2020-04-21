namespace OurGameName.DoMain.Entity.Map
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using OurGameName.DoMain.Entity.Map.Args;
    using OurGameName.DoMain.Attribute;
    using UnityEngine.UIElements;
    using OurGameName.DoMain.Entity.Map._2DMap;

    /// <summary>
    /// 地图输入事件
    /// </summary>
    internal class MapInputEvent : MonoBehaviour
    {
        public HexGrid context;
        public PlayerInput PlayerInput;

        public event EventHandler<MapInputEventArgs> NewClick;

        protected virtual void OnNewClick(MapInputEventArgs e)
        {
            e.Raise(this, ref NewClick);
        }

        private void Awake()
        {
            PlayerInput.actions["LeftClick"].performed += OnLeftClick;
            PlayerInput.actions["MiddleClick"].performed += OnMiddleClick;
            PlayerInput.actions["RighTClick"].performed += OnRighTClick;
        }

        private void OnLeftClick(InputAction.CallbackContext obj)
        {
            OnNewClick(new MapInputEventArgs(context.GetMouseCellPosition(), MouseButton.LeftMouse));
        }

        private void OnMiddleClick(InputAction.CallbackContext obj)
        {
            OnNewClick(new MapInputEventArgs(context.GetMouseCellPosition(), MouseButton.MiddleMouse));
        }

        private void OnRighTClick(InputAction.CallbackContext obj)
        {
            OnNewClick(new MapInputEventArgs(context.GetMouseCellPosition(), MouseButton.RightMouse));
        }
    }
}