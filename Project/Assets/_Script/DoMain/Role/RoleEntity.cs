namespace OurGameName.DoMain.RoleSpace
{
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.GameWorld;
    using OurGameName.DoMain.RoleSpace.Args;
    using OurGameName.DoMain.RoleSpace.Component;
    using UnityEngine;
    using UniRx;

    /// <summary>
    /// 角色实体
    /// </summary>
    internal class RoleEntity : MonoBehaviour
    {
        #region Component

        /// <summary>
        /// 行动点组件
        /// </summary>
        public ActionPointComponent ActionPointComponent;

        /// <summary>
        /// Hp组件
        /// </summary>
        public HpComponent HpComponent;

        /// <summary>
        /// 角色贴图组件
        /// </summary>
        public RoleImageComponent roleImageComponent;

        /// <summary>
        /// 角色移动组件
        /// </summary>
        public RoleMoveComponent MoveComponent { get; private set; }

        #endregion Component

        /// <summary>
        /// 该实体对应的角色
        /// </summary>
        public Role Role;

        /// <summary>
        /// 角色管理器
        /// </summary>
        public RoleManager RoleManager;

        private bool CanUpdate = false;

        /// <summary>
        /// 是否被选中
        /// </summary>
        private bool isSelect;

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelect
        {
            get
            {
                return this.isSelect;
            }
            set
            {
                if (this.isSelect == value)
                {
                    return;
                }

                this.isSelect = value;
                if (this.isSelect == true)
                {
                    this.roleImageComponent.Outline = true;
                }
                else
                {
                    this.roleImageComponent.Outline = false;
                }
            }
        }

        /// <summary>
        /// 角色位置
        /// </summary>
        public Vector2Int RolePosition { get { return this.MoveComponent.CurrentRolePosition.ToVector2Int(); } }

        #region Unity

        private void Awake()
        {
            this.IsSelect = false;
        }

        private void OnMouseUpAsButton()
        {
            this.IsSelect = true;
            this.RoleManager.SelectRoleEntity = this;
        }

        private void Update()
        {
            if (this.CanUpdate == false) return;

            this.MoveComponent.Update();
        }

        #endregion Unity

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
        internal void Init(Role role, Sprite roleSprite)
        {
            this.Role = role;

            this.ActionPointComponent.Init(role.ActionPoint);
            this.HpComponent.Init(role.MaxHP.Value);
            this.MoveComponent = new RoleMoveComponent(role.Position.Value, this, 300);
            this.roleImageComponent.Init(roleSprite);

            this.Role.HP.Subscribe(x => this.HpComponent.HP = x);
            this.Role.MaxHP.Subscribe(x => this.HpComponent.MaxHp = x);
            this.Role.Position
                .Where(x => x != this.MoveComponent.CurrentRolePosition)
                .Subscribe(x => this.MoveComponent.Move(x));

            this.CanUpdate = true;
        }
    }
}