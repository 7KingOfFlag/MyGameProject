namespace OurGameName.DoMain.RoleSpace
{
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.GameWorld;
    using OurGameName.DoMain.RoleSpace.Args;
    using UnityEngine;

    internal class RoleEntity : MonoBehaviour
    {
        /// <summary>
        /// 角色贴图组件
        /// </summary>
        public RoleImageComponent roleImageComponent;

        /// <summary>
        /// 角色管理器
        /// </summary>
        public RoleManager RoleManager;

        /// <summary>
        /// 是否被选中
        /// </summary>
        private bool m_isSelect;

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelect
        {
            get
            {
                return this.m_isSelect;
            }
            set
            {
                if (this.roleImageComponent == null)
                {
                    Debug.LogError("Important Error!!RoleEntity can't have RoleImageComponent");
                    return;
                }
                else
                {
                    if (this.m_isSelect == value)
                    {
                        return;
                    }

                    this.m_isSelect = value;
                    if (this.m_isSelect == true)
                    {
                        this.roleImageComponent.Outline = true;
                        this.roleImageComponent.IsBlink = true;
                    }
                    else
                    {
                        this.roleImageComponent.Outline = false;
                        this.roleImageComponent.IsBlink = false;
                    }
                }
            }
        }

        /// <summary>
        /// 角色移动组件
        /// </summary>
        public RoleMoveComponent MoveComponent { get; private set; }

        /// <summary>
        /// 角色位置
        /// </summary>
        public Vector2Int RolePosition { get { return this.MoveComponent.CurrentRolePosition.ToVector2Int(); } }

        /// <summary>
        /// 移动角色
        /// </summary>
        /// <param name="TargetCellPosition">目的地单元格</param>
        public void MoveRole(Vector2Int TargetCellPosition)
        {
            this.MoveComponent.Move(TargetCellPosition.ToVector3Int());
        }

        /// <summary>
        /// 移动角色
        /// </summary>
        /// <param name="moveList">目的地单元格列表</param>
        public void MoveRole(List<Vector2Int> moveList)
        {
            this.MoveComponent.Move(moveList.ToList());
        }

        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="args"></param>
        internal void Init(RoleEntityInitArgs args)
        {
        }

        private void Awake()
        {
            if (this.RoleManager == null)
            {
                Debug.LogError("RoleEntity's RoleManager is null");
            }
            this.IsSelect = false;

            this.MoveComponent = new RoleMoveComponent(Vector3Int.zero, this, 30);
        }

        private void OnMouseUpAsButton()
        {
            this.IsSelect = true;
            this.RoleManager.SelectRoleEntity = this;
        }

        private void Update()
        {
            this.MoveComponent.Update();
        }
    }
}