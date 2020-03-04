using System.Collections.Generic;
using System.Linq;
using System.Text;
using OurGameName.DoMain.Attribute;
using OurGameName.DoMain.Entity.RoleSpace;
using UnityEngine;

namespace OurGameName.DoMain.Entity.Combat
{
    /// <summary>
    /// 战斗回合控制类
    /// 使用NextTime()结束当前角色的回合
    /// </summary>
    public class CombatRound
    {
        /// <summary>
        /// 战斗序列
        /// </summary>
        public List<Role> CombatList;

        /// <summary>
        /// 战斗人员数组
        /// </summary>
        public List<Role> CombatRoles;

        /// <summary>
        /// 当前回合的角色的索引
        /// </summary>
        private byte ActiveRoleIndex;

        /// <summary>
        /// 回合分割标志 标记当前与下一个大回合结束的地方
        /// </summary>
        public int[] RoundEndIndex { get; private set; }

        /// <summary>
        /// 战斗回合控制类
        /// </summary>
        /// <param name="combatRoles">战斗人员数组</param>
        public CombatRound(List<Role> combatRoles)
        {
            CombatList = new List<Role>();
            CombatRoles = combatRoles;
            ActiveRoleIndex = 0;
            RoundEndIndex = new int[2];
        }

        /// <summary>
        /// 当前活动的角色
        /// </summary>
        public Role Active
        {
            get
            {
                return CombatList[ActiveRoleIndex];
            }
        }

        /// <summary>
        /// 战斗排序
        /// </summary>
        /// <param name="list">要排序的战斗人员数组</param>
        /// <returns></returns>
        private List<Role> CombatSort(List<Role> list)
        {
            return (from role in list
                    orderby role.Speed
                    select role)
                   .ToList();
        }

        /// <summary>
        /// 结束当前角色回合 进入下一回合
        /// </summary>
        public void NextTime()
        {
            ActiveRoleIndex++;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            CombatList.AddRange(CombatSort(CombatRoles));
            RoundEndIndex[0] = CombatList.Count - 1;
            CombatList.AddRange(CombatSort(CombatRoles));
            RoundEndIndex[1] = CombatList.Count - 1;
            StringBuilder builder = new StringBuilder();
            builder.Append("战斗序列以刷新当前序列:\n");
            for (int i = 0; i < CombatList.Count; i++)
            {
                builder.AppendFormat("{0}.{1}", i, CombatList[i].GetName());
                if (isRoundLast(i) == true)
                {
                    builder.AppendFormat("\t--大回合结束\n");
                }
                else
                {
                    builder.AppendFormat("\n");
                }
            }
            Debug.Log(builder.ToString());
        }

        /// <summary>
        /// 当前角色是否排在一个大回合末尾
        /// </summary>
        /// <param name="index"></param>
        public bool isRoundLast(int index)
        {
            return RoundEndIndex.IsItemInSource(x => x == index);
        }
    }
}