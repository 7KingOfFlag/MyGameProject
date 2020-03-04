using System.Collections.Generic;
using OurGameName.DoMain.Attribute;
using UnityEngine;

namespace OurGameName.DoMain.Entity.RoleSpace
{
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
            CurrentRolePosition = currentRolePosition;
            m_targetRolePosition = currentRolePosition;
            MoveSpeed = moveSpeed;
            this.context = context;
            m_moveCount = 0;
        }

        public bool OnMove
        {
            get { return m_onMove; }
            set
            {
                if (m_onMove != value)
                {
                    m_onMove = value;
                }
            }
        }

        public void Move(List<Vector2Int> moveTargetList)
        {
            m_moveTargetList = moveTargetList;
            m_targetRolePosition = moveTargetList[0].ToVector3Int();
            SetMovePerform();
        }

        /// <summary>
        /// 移动至指定位置
        /// </summary>
        /// <param name="moveTargetPosition"></param>
        public void Move(Vector3Int moveTargetPosition)
        {
            m_targetRolePosition = moveTargetPosition;
            SetMovePerform();
        }

        public void Update()
        {
            if (CurrentRolePosition != m_targetRolePosition)
            {
                if (m_moveCount < m_MaxMoveCount)
                {
                    m_moveCount += MoveSpeed * Time.deltaTime;
                    OnMove = true;
                }
                else
                {
                    m_moveCount = 0;
                    SetPosition(m_targetRolePosition);
                    OnMove = false;
                }
            }
        }

        /// <summary>
        /// 设置移动的演出
        /// </summary>
        private void SetMovePerform()
        {
            if (CurrentRolePosition != m_targetRolePosition)
            {
                Vector3 moveAngle = m_targetRolePosition - CurrentRolePosition;
                context.transform.position = context.RoleManager.CellToWorld(CurrentRolePosition) + moveAngle * 0.25f;
            }
            else
            {
                context.transform.position = context.RoleManager.CellToWorld(CurrentRolePosition);
            }
        }

        /// <summary>
        /// 移动物理位置
        /// </summary>
        /// <param name="newPosition"></param>
        private void SetPosition(Vector3Int newPosition)
        {
            context.transform.position = context.RoleManager.CellToWorld(newPosition);
            CurrentRolePosition = newPosition;
            m_moveTargetList.RemoveAt(0);
            m_targetRolePosition = m_moveTargetList[0].ToVector3Int();
        }
    }
}