namespace OurGameName.DoMain.RoleSpace.Component
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.Role.Component;
    using OurGameName.DoMain.RoleSpace.SkillSpace;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    /// <summary>
    /// 角色状态接口
    /// </summary>
    internal interface IRoleStatus
    {
        /// <summary>
        /// 被选中的技能
        /// </summary>
        Skill SelectedSkill { get; set; }

        /// <summary>
        /// 角色技能状态
        /// </summary>
        RoleSkillStatus Status { get; set; }
    }

    /// <summary>
    /// 角色技能组件
    /// </summary>
    internal sealed class RoleSkillComponet : MonoBehaviour, IRoleStatus
    {
        /// <summary>
        /// 透明的控制用画布组
        /// </summary>
        public CanvasGroup AhplaController;

        /// <summary>
        /// 技能按钮资源
        /// </summary>
        public AssetReference SkillButtonAsset;

        /// <summary>
        /// 技能按钮
        /// </summary>
        private List<SkillButtonCompnent> SkillButtons = new List<SkillButtonCompnent>();

        /// <summary>
        /// 被选中的技能
        /// </summary>
        public Skill SelectedSkill { get; set; } = null;

        /// <summary>
        /// 角色技能状态
        /// </summary>
        public RoleSkillStatus Status { get; set; } = RoleSkillStatus.Unselected;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visibility
        {
            get => this.AhplaController.alpha == 1f;
            set => this.AhplaController.alpha = value ? 1 : 0f;
        }

        /// <summary>
        /// 技能使用者
        /// </summary>
        private Role User { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="roleEntity"></param>
        internal void Init(RoleEntity roleEntity)
        {
            this.User = roleEntity.Role;
            this.InitSkilllButtonAsync(this.User.Skills, roleEntity).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取按钮实例
        /// </summary>
        /// <returns></returns>
        private async Task<SkillButtonCompnent> GetButtonInstantiate()
        {
            var result = await this.SkillButtonAsset.InstantiateAsync(this.transform).Task.ConfigureAwait(true);
            return result.GetComponent<SkillButtonCompnent>();
        }

        /// <summary>
        /// 初始化技能按钮
        /// </summary>
        /// <param name="skills">角色技能</param>
        private async Task InitSkilllButtonAsync(List<Skill> skills, RoleEntity roleEntity)
        {
            for (int i = 0; i < skills.Count; i++)
            {
                var BtnSkill = await this.GetButtonInstantiate().ConfigureAwait(true);
                var skill = skills[i];
                BtnSkill.Init(skill.Name, () =>
                {
                    this.Status = RoleSkillStatus.Skill;
                    this.SelectedSkill = skill;
                });
            }
            var btnMove = await this.GetButtonInstantiate().ConfigureAwait(true);
            btnMove.Init("移动", () => this.Status = RoleSkillStatus.Move);

            var btnUnselected = await this.GetButtonInstantiate().ConfigureAwait(true);
            btnUnselected.Init("取消", () =>
            {
                this.Status = RoleSkillStatus.Unselected;
                roleEntity.TryDeSelectRoleEntity();
            });
        }
    }
}