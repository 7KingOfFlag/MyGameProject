namespace OurGameName.DoMain.Controller
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.Tilemaps;

    internal class CameraMove : MonoBehaviour
    {
        public Tilemap BackGroundTilemap;
        public Cinemachine.CinemachineVirtualCamera CinemachineCamera;

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

        private Camera mainCamera;
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

        private void KeyMoveEvent()
        {
            Vector2 MoveVector = MoveAction.ReadValue<Vector2>();
            // Debug.Log($"MoveVector{MoveVector}");
            transform.position += new Vector3(MoveVector.x, MoveVector.y, 0f) * KeyboardMovespeed * Time.deltaTime;
        }

        private void ScrollWheelEvent(InputAction.CallbackContext obj)
        {
            var position = transform.position;
            float newFileOfView = position.z + obj.ReadValue<Vector2>().y * MouseTiltSpeed * Time.deltaTime;
            transform.position = new Vector3(position.x, position.y, Mathf.Clamp(newFileOfView, MinFieldOfView, MaxFieldOfView));
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
            else if (currentMouse.middleButton.ReadValue() == 0)
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

        private void OnGUI()
        {
            var FontStyle = new GUIStyle();
            FontStyle.fontSize = 24;
            GUI.Label(new Rect(mainCamera.pixelWidth - 200, 20, 100, 30), $"IsMouseHold:{currentMouse.leftButton.ReadValue()}", FontStyle);
        }

        #endregion 鼠标拖拽
    }
}