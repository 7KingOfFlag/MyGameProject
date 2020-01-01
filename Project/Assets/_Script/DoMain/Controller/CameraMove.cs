using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace OurGameName.DoMain.Controller
{
    internal class CameraMove : MonoBehaviour
    {
        public PlayerInput PlayerInput;

        /// <summary>
        /// 键盘移动速度
        /// </summary>
        public float KeyboardMovespeed = 10f;

        /// <summary>
        /// 鼠标俯仰速度
        /// </summary>
        public float MouseTiltSpeed = 5f;

        /// <summary>
        /// 鼠标拖曳速度
        /// </summary>
        public float MouseDragSpeed = 12f;

        public Tilemap BackGroundTilemap;
        public Cinemachine.CinemachineVirtualCamera CinemachineCamera;

        /// <summary>
        /// 最小视野距离
        /// </summary>
        public int MinFieldOfView = -3;

        /// <summary>
        /// 最大视野距离
        /// </summary>
        public int MaxFieldOfView = 4;

        private Camera mainCamera;

        /// <summary>
        /// 当前鼠标
        /// </summary>
        private Mouse currentMouse;

        private InputAction MoveAction;

        private void Awake()
        {
            mainCamera = Camera.main;
            currentMouse = Mouse.current;
            PlayerInput.actions["ScrollWheel"].performed += ScrollWheelEvent;
            MoveAction = PlayerInput.actions["Move"];
        }

        private void FixedUpdate()
        {
            DragMapEvent();
            KeyMoveEvent();
        }

        private void ScrollWheelEvent(InputAction.CallbackContext obj)
        {
            var position = transform.position;
            float newFileOfView = position.z + obj.ReadValue<Vector2>().y * MouseTiltSpeed * Time.deltaTime;
            transform.position = new Vector3(position.x, position.y, Mathf.Clamp(newFileOfView, MinFieldOfView, MaxFieldOfView));
        }

        private void KeyMoveEvent()
        {
            Vector2 MoveVector = MoveAction.ReadValue<Vector2>();
            //Debug.Log($"MoveVector{MoveVector}");
            transform.position += new Vector3(MoveVector.x, MoveVector.y, 0f) * KeyboardMovespeed * Time.deltaTime;
        }

        #region 鼠标拖拽

        private Vector2 MouseDownPosition;
        private Vector2 MouseUpPosition;
        private bool IsMouseHold = false;

        private void DragMapEvent()
        {
            if (currentMouse.middleButton.ReadValue() == 1 && IsMouseHold == false)
            {
                IsMouseHold = true;
                MouseDownPosition = currentMouse.position.ReadValue();
            }
            else if (currentMouse.middleButton.ReadValue() == 0)
            {
                IsMouseHold = false;
            }

            if (IsMouseHold == true)
            {
                MouseUpPosition = currentMouse.position.ReadValue();
                var moveVector = (MouseDownPosition - MouseUpPosition).normalized;
                transform.position += new Vector3(moveVector.x, moveVector.y) * MouseDragSpeed * Time.deltaTime;
                MouseDownPosition = MouseUpPosition;
            }
        }

        private void OnGUI()
        {
            var FontStyle = new GUIStyle();
            FontStyle.fontSize = 24;
            GUI.Label(new Rect(mainCamera.pixelWidth - 200, 20, 100, 30), $"IsMouseHold:{currentMouse.leftButton.ReadValue()}", FontStyle);
        }

        #endregion 鼠标拖拽
    }
}