﻿namespace OurGameName.DoMain.Entity.TileHexMap.UI
{
    using System;
    using OurGameName.DoMain.Attribute;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    internal class HexTileInputEvent : MonoBehaviour
    {
        public HexTileMapEditor context;
        public PlayerInput PlayerInput;

        public event EventHandler<HexTileInputEventArgs> NewClick;

        protected virtual void OnNewClick(HexTileInputEventArgs e)
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
            OnNewClick(new HexTileInputEventArgs(context.GetMouseCellPosition(), MouseButton.LeftMouse));
        }

        private void OnMiddleClick(InputAction.CallbackContext obj)
        {
            OnNewClick(new HexTileInputEventArgs(context.GetMouseCellPosition(), MouseButton.MiddleMouse));
        }

        private void OnRighTClick(InputAction.CallbackContext obj)
        {
            OnNewClick(new HexTileInputEventArgs(context.GetMouseCellPosition(), MouseButton.RightMouse));
        }
    }
}