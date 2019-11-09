using OurGameName.DoMain.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OurGameName.DoMain.Entity.RoleSpace
{
    internal class RoleEntity : MonoBehaviour
    {
        public RoleImageComponent roleImage;
        
        public RoleMoveComponent MoveComponent { get; private set; }

        public RoleManager RoleManager;

        private bool m_isSelect;

        public bool IsSelect {
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

        void Awake()
        {
            if (RoleManager == null)
            {
                Debug.LogError("RoleEntity's RoleManager is null");
            }
            IsSelect = false;

            MoveComponent = new RoleMoveComponent(Vector3Int.zero, this, 20);
        }

        void Update()
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
        void OnMouseUpAsButton()
        {
            IsSelect = true;
            RoleManager.SelectRoleEntity = this;
        }
    }
}
