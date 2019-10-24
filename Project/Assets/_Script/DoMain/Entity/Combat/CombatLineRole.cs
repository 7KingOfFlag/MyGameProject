using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using OurGameName.DoMain.Attribute;
using OurGameName.DoMain.Data;

namespace OurGameName.DoMain.Entity.Combat
{
    public class CombatLineRole : MonoBehaviour
    {
        /// <summary>
        /// 角色立绘
        /// </summary>
        public RawImage RoleIocn;


        private bool isActionRole;

        /// <summary>
        /// 角色ID
        /// </summary>
        private int roleID = -1;

        private static Vector3 actionRoleScale = new Vector3(0.7f, 0.7f, 1);
        private static Vector3 waitRoleScale = new Vector3(0.5f, 0.5f, 1);

        private void Awake()
        {
            RoleIocn = transform.
                Find("Image(Mask)").
                Find("Role Iocn").
                GetComponent<RawImage>();
        }

        /// <summary>
        /// 是否是活动角色
        /// </summary>
        public bool IsActionRole
        {
            get
            {
                return isActionRole;
            }
            set
            {
                isActionRole = value;
                if (isActionRole == true)
                {
                    transform.localScale = actionRoleScale;
                }
                else
                {
                    transform.localScale = waitRoleScale;
                }
            }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            set
            {
                if (value != roleID)
                {
                    roleID = value;
                    Refresh();
                }
                else
                {
                    Debug.Log("设置的ID已经是当前ID");
                }
            }
        }

        private int roleState = 0;

        /// <summary>
        /// 角色状态
        /// </summary>
        public int RoleState
        {
            get
            {
                return roleState;
            }
            set
            {
                roleState = value;
                Refresh();
            }
        }

        private void Refresh()
        { 
            //string path;
            if (RoleState > 0)
            {
                /*
                path = string.Format("{0}{1}-{2}.{3}",
            GameData.PicturePath,
            GameData.GetMonster(roleID).Name,
            RoleState,
            GameData.GetMonster(roleID).ImageType);
            */
            }
            else
            {
                /*
                path = string.Format("{0}{1}.{2}",
                GameData.PicturePath,
                GameData.GetMonster(roleID).Name,
                GameData.GetMonster(roleID).ImageType);
                */
                // TODO: 设置图片
            }
        }

    }
}
