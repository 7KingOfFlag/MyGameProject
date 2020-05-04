namespace OurGameName.DoMain.Controller
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.Tilemaps;
    using Cinemachine;

    internal class CameraController : MonoBehaviour
    {
        public Tilemap BackGroundTilemap;
        public CinemachineVirtualCamera CinemachineCamera;

        /// <summary>
        /// 键盘移动速度
        /// </summary>
        public float KeyboardMovespeed = 10f;

        /// <summary>
        /// 最大视野距离
        /// </summary>
        public int MaxFieldOfView = 4;

        /// <summary>
        /// 最小视野距离
        /// </summary>
        public int MinFieldOfView = -3;

        /// <summary>
        /// 鼠标拖曳速度
        /// </summary>
        public float MouseDragSpeed = 12f;

        /// <summary>
        /// 鼠标俯仰速度
        /// </summary>
        public float MouseTiltSpeed = 5f;

        public PlayerInput PlayerInput;

        /// <summary>
        /// 当前鼠标
        /// </summary>
        private Mouse currentMouse;

        private InputAction MoveAction;

        private void Awake()
        {
            this.currentMouse = Mouse.current;
            this.PlayerInput.actions["ScrollWheel"].performed += this.ScrollWheelEvent;
            this.MoveAction = this.PlayerInput.actions["Move"];
        }

        private void FixedUpdate()
        {
            this.DragMapEvent();
            this.KeyMoveEvent();
        }

        private void KeyMoveEvent()
        {
            Vector2 moveVector = this.MoveAction.ReadValue<Vector2>();
            // Debug.Log($"MoveVector{MoveVector}");
            this.transform.position += new Vector3(moveVector.x, moveVector.y, 0f) * this.KeyboardMovespeed * Time.deltaTime;
        }

        private void ScrollWheelEvent(InputAction.CallbackContext obj)
        {
            var position = this.transform.position;
            float newFileOfView = position.z + obj.ReadValue<Vector2>().y * this.MouseTiltSpeed * Time.deltaTime;
            this.transform.position = new Vector3(position.x, position.y, Mathf.Clamp(newFileOfView, this.MinFieldOfView, this.MaxFieldOfView));
        }

        #region 鼠标拖拽

        private bool IsMouseHold = false;
        private Vector2 MouseDownPosition;
        private Vector2 MouseUpPosition;

        private void DragMapEvent()
        {
            if (this.currentMouse.middleButton.ReadValue() == 1 && this.IsMouseHold == false)
            {
                this.IsMouseHold = true;
                this.MouseDownPosition = this.currentMouse.position.ReadValue();
            }
            else if (this.currentMouse.middleButton.ReadValue() == 0)
            {
                this.IsMouseHold = false;
            }

            if (this.IsMouseHold == true)
            {
                this.MouseUpPosition = this.currentMouse.position.ReadValue();
                var moveVector = (this.MouseDownPosition - this.MouseUpPosition).normalized;
                this.transform.position += new Vector3(moveVector.x, moveVector.y) * this.MouseDragSpeed * Time.deltaTime;
                this.MouseDownPosition = this.MouseUpPosition;
            }
        }

        //private void OnGUI()
        //{
        //    var FontStyle = new GUIStyle();
        //    FontStyle.fontSize = 24;
        //    GUI.Label(new Rect(this.mainCamera.pixelWidth - 200, 20, 100, 30), $"IsMouseHold:{this.currentMouse.leftButton.ReadValue()}", FontStyle);
        //}

        #endregion 鼠标拖拽
    }
}