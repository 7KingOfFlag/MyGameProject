#define DebugMode

using System.Collections.Generic;
using OurGameName.DoMain.Entity.RoleSpace;
using UnityEngine;

namespace OurGameName.DoMain.Entity.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        /// <summary>
        /// 战斗人员数组
        /// </summary>
        private List<Role> CombatRoles;

        /// <summary>
        /// 战斗回合控制
        /// </summary>
        private CombatRound CombatRound;

        /// <summary>
        /// 战场角色画布
        /// </summary>
        public Canvas CombatPosition;

        /// <summary>
        /// 顶层UI
        /// </summary>
        public Canvas CombatUI;

        /// <summary>
        /// 角色图像预制体
        /// </summary>
        public RoleImage roleImagePrefab;

        /// <summary>
        /// 整场战斗中的角色图像的集合
        /// </summary>
        private List<RoleImage> roleImageList;

        /// <summary>
        /// 玩家盟友战斗位置
        /// </summary>
        private static Vector3 AllyCombatPostiton = new Vector3(8.5f, 1, -3.5f);

        private CombatLine CombatLine;

        /// <summary>
        /// 场景环境
        /// </summary>
        public Transform Environment;

        /// <summary>
        /// 战斗菜单
        /// </summary>
        private CombatMenu menu;

        private void Awake()
        {
            Transform environment = Instantiate(Environment);
            environment.localPosition = Vector3.zero;

            menu = GameObject.Find("CombatMenu").GetComponent<CombatMenu>();
            menu.Enabled = false;

            //GameData.LoadFromJsonInit();
            Init();
        }

        private void Start()
        {
            CombatRound.Refresh();
            LoadRoleOnMay();
            CombatLine.ShowRoleInLine(CombatRound);
        }

        private void Update()
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            CombatPosition = GameObject.Find("Combat Position").GetComponent<Canvas>();
            CombatUI = GameObject.Find("Combat UI").GetComponent<Canvas>();

            CombatRoles = new List<Role>();
            roleImageList = new List<RoleImage>();

            CombatLine = GameObject.Find("Combat Line").GetComponent<CombatLine>();
            CombatRound = new CombatRound(CombatRoles);
#if DebugMode
            Skill basicSkill = new Skill("斩击", 10, 1, 2);
            Skill EndTurn = new Skill("回合结束", 0, 0, 0,
                new List<string>()
                {
                "EndTurn"
                });

            Role R1 = new Role("张", "三", roleType: RoleType.player);
            Role R2 = new Role("李", "四", roleType: RoleType.enemy);
            Role R3 = new Role("王", "五", roleType: RoleType.ally);
            Role R4 = new Role("金", "杰", roleType: RoleType.ally);
            Role R5 = new Role("赵", "六", roleType: RoleType.ally);
            Role R6 = new Role("田", "七", roleType: RoleType.ally);

            CombatRoles.Add(R1);
            CombatRoles.Add(R2);
            CombatRoles.Add(R3);
            CombatRoles.Add(R4);
            CombatRoles.Add(R5);
            CombatRoles.Add(R6);

            foreach (var item in CombatRoles)
            {
                item.CombatInit();
                item.Skills = new List<Skill>()
                {
                    basicSkill,EndTurn
                };
            }

#endif
        }

        /// <summary>
        /// 将角色图像载入到地图上
        /// </summary>
        private void LoadRoleOnMay()
        {
            //位置统计
            int allyCount = 0, enemyCount = 0;
            bool isAlly;

            for (int i = 0; i < CombatRoles.Count; i++)
            {
                Role role = CombatRoles[i];

                bool isActionRole = role == CombatRound.Active;

                if (role.roleType == RoleType.ally ||
                    role.roleType == RoleType.player)
                {
                    isAlly = true;
                }
                else
                {
                    isAlly = false;
                }
                RoleImage roleImageBox = Instantiate(roleImagePrefab);

                // TODO：重新优化函数结构
                SetPostion(roleImageBox, isAlly == true ? allyCount++ : enemyCount++, isAlly, role, isActionRole);

                if (isActionRole == true)
                {
                    SetCombatMenu(role, roleImageBox);
                }
            }
        }

        /// <summary>
        /// 设置角色在地图上的位置
        /// </summary>
        /// <param name="roleCount"></param>
        /// <param name="isAlly"></param>
        /// <param name="role"></param>
        /// <param name="isActionRole"></param>
        private void SetPostion(RoleImage roleImageBox, int roleCount, bool isAlly, Role role, bool isActionRole)
        {
            roleImageBox.RoleType = role.roleType;

            roleImageBox.txtName.text = role.FamilyName + role.Name;
            /*
            roleImageBox.Path = string.Format("{0}{1}.{2}",
                GameData.PicturePath,
                GameData.GetMonster(role.ID).Name,
                GameData.GetMonster(role.ID).ImageType);
                */
            roleImageBox.transform.SetParent(CombatPosition.transform);

            roleImageBox.transform.localPosition = GetPostiton(isAlly,
                roleCount,
                isActionRole);

            roleImageList.Add(roleImageBox);
        }

        /// <summary>
        /// 获取地图上的站位
        /// </summary>
        /// <param name="isAlly"></param>
        /// <param name="RoleCount"></param>
        /// <param name="isActionRole"></param>
        /// <returns></returns>
        private Vector3 GetPostiton(bool isAlly, int RoleCount, bool isActionRole)
        {
            Vector3 position = Vector3.zero;
            //TODO:改变站位系统 站位分离
            if (isAlly && isActionRole)
            {
                position = AllyCombatPostiton;
            }
            else if (isAlly == true)
            {
                position.x += 12 + (RoleCount % 2 * 2);
                position.z = -5 + 1.25f * RoleCount;
            }
            else
            {
                position.x -= 2 - RoleCount - 1;
                position.z = -2f;
            }
            position.y = 1f;

            return position;
        }

        /// <summary>
        /// 在角色上设置活动菜单
        /// </summary>
        private void SetCombatMenu(Role role, RoleImage ActiveRoleImage)
        {
            menu.transform.SetParent(CombatUI.transform);
            menu.Enabled = true;
            menu.SkillList = role.Skills;
            menu.Refresh();
        }
    }
}