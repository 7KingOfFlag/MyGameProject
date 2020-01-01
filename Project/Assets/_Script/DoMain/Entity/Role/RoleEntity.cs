using OurGameName.DoMain.Attribute;
using System.Linq;
using UnityEngine;

namespace OurGameName.DoMain.Entity.RoleSpace
{
    internal class RoleEntity : MonoBehaviour
    {
        public RoleImageComponent roleImage;

        public RoleMoveComponent MoveComponent { get; private set; }

        public Vector2Int RolePosition { get { return MoveComponent.CurrentRolePosition.ToVector2Int(); } }

        public RoleManager RoleManager;

        private bool m_isSelect;

        public bool IsSelect
        {
            get
            {
                return m_isSelect;
            }
            set
            {
                if (roleImage == null)
                {
                    Debug.LogError("Important Error!!RoleEntity can't have RoleImageComponent");
                    return;
                }
                else
                {
                    if (m_isSelect == value)
                    {
                        return;
                    }

                    m_isSelect = value;
                    if (m_isSelect == true)
                    {
                        roleImage.Outline = true;
                        roleImage.IsBlink = true;
                    }
                    else
                    {
                        roleImage.Outline = false;
                        roleImage.IsBlink = false;
                    }
                }
            }
        }

        private void Awake()
        {
            if (RoleManager == null)
            {
                Debug.LogError("RoleEntity's RoleManager is null");
            }
            IsSelect = false;

            MoveComponent = new RoleMoveComponent(Vector3Int.zero, this, 20);
        }

        private void Update()
        {
            MoveComponent.Update();
        }

        /// <summary>
        /// 移动角色
        /// </summary>
        /// <param name="TargetCellPosition">目的地单元格</param>
        public void MoveRole(Vector2Int TargetCellPosition)
        {
            MoveComponent.Move(TargetCellPosition.ToVector3Int());
        }

        public void MoveRole(Vector2Int[] moveList)
        {
            MoveComponent.Move(moveList.ToList());
        }

        private void OnMouseUpAsButton()
        {
            IsSelect = true;
            RoleManager.SelectRoleEntity = this;
        }
    }
}