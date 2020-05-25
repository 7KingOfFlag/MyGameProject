namespace OurGameName.DoMain.RoleSpace.Component
{
    using System.Linq;
    using System.Collections.Generic;
    using OurGameName.DoMain.Attribute;
    using UnityEngine;

    /// <summary>
    /// 角色移动组件
    /// </summary>
    internal class RoleMoveComponent
    {
        /// <summary>
        /// 角色当前位置
        /// </summary>
        public Vector3Int CurrentRolePosition = new Vector3Int();

        /// <summary>
        /// 移动速度
        /// </summary>
        public int MoveSpeed;

        /// <summary>
        /// 移动阈值
        /// </summary>
        private const float MaxMoveCount = 100f;

        /// <summary>
        /// 组件附加的角色
        /// </summary>
        private readonly RoleEntity context;

        /// <summary>
        /// 移动计数器
        /// <para>到达移动阈值时即完成移动</para>
        /// </summary>
        private float moveCount;

        /// <summary>
        /// 移动目标列表
        /// </summary>
        private List<Vector2Int> moveTargetList;

        /// <summary>
        /// 角色目标位置
        /// </summary>
        private Vector3Int RoletargetPosition;

        /// <summary>
        /// 角色移动组件
        /// </summary>
        /// <param name="position">初始位置</param>
        /// <param name="context"></param>
        /// <param name="moveSpeed">移动速度</param>
        public RoleMoveComponent(Vector3Int position, RoleEntity context, int moveSpeed)
        {
            this.CurrentRolePosition = position;
            this.RoletargetPosition = position;
            this.MoveSpeed = moveSpeed;
            this.context = context;
            this.moveCount = 0;
        }

        /// <summary>
        /// 是否在移动中
        /// </summary>
        public bool OnMove { get; set; }

        /// <summary>
        /// 移动至指定位置
        /// </summary>
        /// <param name="moveTargetList"></param>
        public void Move(List<Vector2Int> moveTargetList)
        {
            this.moveTargetList = moveTargetList;
            this.RoletargetPosition = moveTargetList.Last().ToVector3Int();
            this.SetMovePerform();
        }

        /// <summary>
        /// 移动至指定位置
        /// </summary>
        /// <param name="moveTargetPosition"></param>
        public void Move(Vector3Int moveTargetPosition)
        {
            this.RoletargetPosition = moveTargetPosition;
            this.SetMovePerform();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if (this.CurrentRolePosition != this.RoletargetPosition)
            {
                if (this.moveCount < MaxMoveCount)
                {
                    this.moveCount += this.MoveSpeed * Time.deltaTime;
                    this.OnMove = true;
                }
                else
                {
                    this.moveCount = 0;
                    this.SetPosition(this.RoletargetPosition);
                    this.OnMove = false;
                }
            }
        }

        /// <summary>
        /// 设置移动的演出
        /// </summary>
        private void SetMovePerform()
        {
            if (this.CurrentRolePosition != this.RoletargetPosition)
            {
                Vector3 moveAngle = this.RoletargetPosition - this.CurrentRolePosition;
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
            this.context.RolePosition.Value = newPosition;
            if (this.moveTargetList.Count > 1)
            {
                this.moveTargetList.RemoveAt(this.moveTargetList.Count - 1);
                this.RoletargetPosition = this.moveTargetList.Last().ToVector3Int();
            }
        }
    }
}