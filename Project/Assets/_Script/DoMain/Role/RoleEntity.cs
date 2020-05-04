namespace OurGameName.DoMain.RoleSpace
{
    using System.Linq;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.GameWorld;
    using UnityEngine;

    internal class RoleEntity : MonoBehaviour
    {
        public RoleImageComponent roleImage;

        public RoleManager RoleManager;
        private bool m_isSelect;

        public bool IsSelect
        {
            get
            {
                return this.m_isSelect;
            }
            set
            {
                if (this.roleImage == null)
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
                        this.roleImage.Outline = true;
                        this.roleImage.IsBlink = true;
                    }
                    else
                    {
                        this.roleImage.Outline = false;
                        this.roleImage.IsBlink = false;
                    }
                }
            }
        }

        public RoleMoveComponent MoveComponent { get; private set; }

        public Vector2Int RolePosition { get { return this.MoveComponent.CurrentRolePosition.ToVector2Int(); } }

        /// <summary>
        /// 移动角色
        /// </summary>
        /// <param name="TargetCellPosition">目的地单元格</param>
        public void MoveRole(Vector2Int TargetCellPosition)
        {
            this.MoveComponent.Move(TargetCellPosition.ToVector3Int());
        }

        public void MoveRole(Vector2Int[] moveList)
        {
            this.MoveComponent.Move(moveList.ToList());
        }

        private void Awake()
        {
            if (this.RoleManager == null)
            {
                Debug.LogError("RoleEntity's RoleManager is null");
            }
            this.IsSelect = false;

            this.MoveComponent = new RoleMoveComponent(Vector3Int.zero, this, 20);
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