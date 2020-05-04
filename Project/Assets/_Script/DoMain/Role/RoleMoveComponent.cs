namespace OurGameName.DoMain.RoleSpace
{
    using System.Collections.Generic;
    using OurGameName.DoMain.Attribute;
    using UnityEngine;

    internal class RoleMoveComponent
    {
        public Vector3Int CurrentRolePosition = new Vector3Int();
        public int MoveSpeed;

        private const float m_MaxMoveCount = 100f;
        private readonly RoleEntity context;
        private float m_moveCount;

        /// <summary>
        /// 移动目标列表
        /// </summary>
        private List<Vector2Int> m_moveTargetList;

        private bool m_onMove;
        private Vector3Int m_targetRolePosition;

        public RoleMoveComponent(Vector3Int currentRolePosition, RoleEntity context, int moveSpeed)
        {
            this.CurrentRolePosition = currentRolePosition;
            this.m_targetRolePosition = currentRolePosition;
            this.MoveSpeed = moveSpeed;
            this.context = context;
            this.m_moveCount = 0;
        }

        public bool OnMove
        {
            get { return this.m_onMove; }
            set
            {
                if (this.m_onMove != value)
                {
                    this.m_onMove = value;
                }
            }
        }

        /// <summary>
        /// 移动至指定位置
        /// </summary>
        /// <param name="moveTargetList"></param>
        public void Move(List<Vector2Int> moveTargetList)
        {
            this.m_moveTargetList = moveTargetList;
            this.m_targetRolePosition = moveTargetList[0].ToVector3Int();
            this.SetMovePerform();
        }

        /// <summary>
        /// 移动至指定位置
        /// </summary>
        /// <param name="moveTargetPosition"></param>
        public void Move(Vector3Int moveTargetPosition)
        {
            this.m_targetRolePosition = moveTargetPosition;
            this.SetMovePerform();
        }

        public void Update()
        {
            if (this.CurrentRolePosition != this.m_targetRolePosition)
            {
                if (this.m_moveCount < m_MaxMoveCount)
                {
                    this.m_moveCount += this.MoveSpeed * Time.deltaTime;
                    this.OnMove = true;
                }
                else
                {
                    this.m_moveCount = 0;
                    this.SetPosition(this.m_targetRolePosition);
                    this.OnMove = false;
                }
            }
        }

        /// <summary>
        /// 设置移动的演出
        /// </summary>
        private void SetMovePerform()
        {
            if (this.CurrentRolePosition != this.m_targetRolePosition)
            {
                Vector3 moveAngle = this.m_targetRolePosition - this.CurrentRolePosition;
                this.context.transform.position = this.context.RoleManager.CellToWorld(this.CurrentRolePosition) + moveAngle * 0.25f;
            }
            else
            {
                this.context.transform.position = this.context.RoleManager.CellToWorld(this.CurrentRolePosition);
            }
        }

        /// <summary>
        /// 移动物理位置
        /// </summary>
        /// <param name="newPosition"></param>
        private void SetPosition(Vector3Int newPosition)
        {
            this.context.transform.position = this.context.RoleManager.CellToWorld(newPosition);
            this.CurrentRolePosition = newPosition;
            this.m_moveTargetList.RemoveAt(0);
            this.m_targetRolePosition = this.m_moveTargetList[0].ToVector3Int();
        }
    }
}