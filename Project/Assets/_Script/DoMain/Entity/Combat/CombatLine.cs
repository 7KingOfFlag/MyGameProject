using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using OurGameName.DoMain.Entity.RoleSpace;

namespace OurGameName.DoMain.Entity.Combat
{
    /// <summary>
    /// 战斗行
    /// 仅有显示的和切换选择人物的功能 不要添加控制代码
    /// </summary>
    public class CombatLine : MonoBehaviour
    {
        /// <summary>
        /// 人物头像生成位置参考点
        /// </summary>
        public Transform RoleBuilder;
        /// <summary>
        /// 战斗行角色头像预制体
        /// </summary>
        public CombatLineRole roleIconePrefab;

        public RectTransform RoundFalgPrefab;

        /// <summary>
        /// 已经显示的图像的集合
        /// </summary>
        private List<CombatLineRole> LineRoles;

        /// <summary>
        /// 在战斗行上最多显示角色头像的数量
        /// </summary>
        private const Byte MaxShouw = 7;

        /// <summary>
        /// 在战斗行上显示人物头像
        /// </summary>
        /// <param name="CombatRole">战斗行上的角色顺序</param>
        /// <param name="RoundEndIndex">大回合上的结束点标志</param>
        public void ShowRoleInLine(CombatRound round)
        {
            List<Role> CombatList = round.CombatList;
            if (LineRoles == null)
            {
                LineRoles = new List<CombatLineRole>();
                int positionCountX = 0;
                for (int i = 0; i < CombatList.Count; i++)
                {
                    if (i >= MaxShouw)
                    {
                        break;
                    }

                    CombatLineRole RoleBox = Instantiate(roleIconePrefab);

                    RoleBox.RoleID = CombatList[i].ID;

                    LineRoles.Add(RoleBox);
                    RoleBox.IsActionRole = (i == 0);
                    RoleBox.transform.SetParent(RoleBuilder);

                    //设置位置
                    Vector3 postition = Vector3.zero;
                    if (i == 1)
                    {
                        positionCountX += 95;
                        
                    }
                    else if (i > 1)
                    {
                        positionCountX += 65;
                    }
                    postition.x = positionCountX;
                    postition.y = -9;
                    RoleBox.transform.localPosition = postition;

                    //判断并添加大回合末尾标志
                    if (round.isRoundLast(i) == true)
                    {
                        RectTransform RoundFalg = Instantiate(RoundFalgPrefab);
                        RoundFalg.SetParent(RoleBox.transform);
                        postition = Vector3.zero;
                        postition.x = i == 0 ? 80f : 65f;
                        RoundFalg.localPosition = postition;
                    }
                }
            }
        }
    }
}
