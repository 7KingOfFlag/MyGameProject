namespace OurGameName.DoMain.RoleSpace
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Threading.Tasks;
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.GameAction.Args;
    using OurGameName.DoMain.GameAction.Config.ActionEvent;
    using OurGameName.DoMain.GameWorld;
    using OurGameName.DoMain.Map;
    using OurGameName.DoMain.Map._2DMap;
    using OurGameName.DoMain.Map.Args;
    using OurGameName.DoMain.Map.Service;
    using OurGameName.DoMain.RoleSpace.SkillSpace;
    using OurGameName.Interface;
    using UniRx;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;
    using Vector3 = UnityEngine.Vector3;

    internal class RoleManager : MonoBehaviour
    {
        /// <summary>
        /// 游戏世界
        /// </summary>
        public World GameWorld;

        /// <summary>
        /// 六边形地图
        /// </summary>
        public HexGrid HexGrid;

        /// <summary>
        /// 地图输入事件
        /// </summary>
        public MapInputEvent MapInputEvent;

        /// <summary>
        /// 输入控制器
        /// </summary>
        public PlayerInput PlayerInput;

        /// <summary>
        /// 角色资源载入器
        /// </summary>
        public RoleAssertLoader RoleAssertLoader;

        /// <summary>
        /// 坐标转换器
        /// </summary>
        private ICoordinateConverter coordinateConverter;

        /// <summary>
        /// 寻路服务
        /// </summary>
        private INavigateService navigateService;

        /// <summary>
        /// 角色
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// 选中的角色实体
        /// </summary>
        public RoleEntity SelectRoleEntity { get; set; }

        #region Unity相关

        private void Awake()
        {
            this.CheckIsSet();

            this.SelectRoleEntity = null;
            this.coordinateConverter = this.HexGrid;
            this.navigateService = new NavigateService();
        }

        /// <summary>
        /// 校验是否设置Unity公开属性
        /// </summary>
        private void CheckIsSet()
        {
            this.GameWorld.IsSet(nameof(this.GameWorld));
            this.HexGrid.IsSet(nameof(this.HexGrid));
            this.MapInputEvent.IsSet(nameof(this.MapInputEvent));
            this.PlayerInput.IsSet(nameof(this.PlayerInput));
            this.RoleAssertLoader.IsSet(nameof(this.RoleAssertLoader));
        }

        private void Start()
        {
            this.MapInputEvent.NewClick += this.OnClick;
            this.Roles = this.LoadRoles();
            _ = this.BuildRoleEntityAsync(this.Roles);
        }

        #endregion Unity相关

        /// <summary>
        /// 将地图坐标转化为世界坐标
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        internal Vector3 CellToWorld(Vector3Int currentRolePosition)
        {
            return this.coordinateConverter.CellToWorld(currentRolePosition);
        }

        /// <summary>
        /// 指定位置上是否有角色
        /// </summary>
        /// <param name="postion">坐标</param>
        /// <returns></returns>
        internal bool HasRole(Vector2Int postion, out Role role)
        {
            role = this.Roles.FirstOrDefault(x => x.Position.Value.ToVector2Int() == postion);
            return role != null;
        }

        /// <summary>
        /// 尝试取消选中角色实体
        /// </summary>
        /// <param name="roleEntity"></param>
        internal void TryDeSelectRoleEntity(RoleEntity roleEntity)
        {
            if (roleEntity != this.SelectRoleEntity) return;

            this.DeSelectRoleEntity();
        }

        /// <summary>
        /// 尝试选中角色实体
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <returns></returns>
        internal void TrySelectRoleEntity(RoleEntity roleEntity)
        {
            if (this.SelectRoleEntity == roleEntity
                && this.SelectRoleEntity != null
                && this.SelectRoleEntity.RoleStatus.Status != RoleSkillStatus.Unselected)
            {
                return;
            };

            if (this.SelectRoleEntity != null)
            {
                this.SelectRoleEntity.IsSelect = false;
            }
            roleEntity.IsSelect = true;
            this.SelectRoleEntity = roleEntity;
        }

        /// <summary>
        /// 生成角色实体
        /// </summary>
        /// <param name="roles"></param>
        private async Task BuildRoleEntityAsync(List<Role> roles)
        {
            while (this.RoleAssertLoader.WholeBodyImage == null) await Task.Delay(100);
            foreach (var role in roles)
            {
                var roleEntity = await this.RoleAssertLoader.RoleEntity;
                var roleImage = this.RoleAssertLoader.WholeBodyImage[role.ID];
                roleEntity.RoleManager = this;
                roleEntity.Init(role, roleImage);
                roleEntity.transform.SetParent(this.transform);
                roleEntity.transform.localPosition = this.CellToWorld(role.Position.Value);
            }
        }

        /// <summary>
        /// 能否触发技能
        /// </summary>
        /// <param name="args"></param>
        private bool CanExecuteSkill(MapInputEventArgs args)
        {
            if (this.HasRole(args.ClickPosition, out var target) == false)
            {
                return false;
            }
            var skill = this.SelectRoleEntity.RoleStatus.SelectedSkill;
            var skillArgs = new ReadonlyActionInputArgs(this.SelectRoleEntity.Role, new List<Role> { target });
            var skillResult = skill.CheckConditAction(skillArgs);
            Debug.Log(skillResult.ReturnMsg());
            return skillResult.All(x => x.CanExecute == true);
        }

        /// <summary>
        /// 取消选择当前选中的角色
        /// </summary>
        private void DeSelectRoleEntity()
        {
            this.SelectRoleEntity.IsSelect = false;
            if (this.SelectRoleEntity.RoleStatus.Status == RoleSkillStatus.Skill)
            {
                this.SelectRoleEntity.RoleStatus.SelectedSkill = null;
            }
            this.SelectRoleEntity.RoleStatus.Status = RoleSkillStatus.Unselected;
            this.SelectRoleEntity = null;
        }

        /// <summary>
        /// 执行技能
        /// </summary>
        /// <param name="args"></param>
        private void ExecutionSkill(MapInputEventArgs args)
        {
            if (this.CanExecuteSkill(args) == false) return;

            if (this.HasRole(args.ClickPosition, out var target) == false)
            {
                return;
            }
            var skill = this.SelectRoleEntity.RoleStatus.SelectedSkill;
            var skillArgs = new ActionInputArgs(this.SelectRoleEntity.Role, new List<Role> { target });
            skill.ExecutionAction(skillArgs);
        }

        /// <summary>
        /// 载入角色
        /// </summary>
        /// <returns></returns>
        private List<Role> LoadRoles()
        {
            var defaultSkill = ActionEventRegistrar.GetDefaultSkill();

            return new List<Role>
            {
                new Role("姬","掘突", new DateTime(1, 8, 2), 4, 300)
                {
                    Position = new ReactiveProperty<Vector3Int>(new Vector3Int(0, 0, 0)),
                    Skills = defaultSkill,
                },
                new Role("石", "腊", new DateTime(1, 4, 5), 4, 300)
                {
                    Position = new ReactiveProperty<Vector3Int>(new Vector3Int(2, 2, 0)),
                    Skills = defaultSkill,
                },
            };
        }

        /// <summary>
        /// 移动角色
        /// </summary>
        /// <param name="args"></param>
        private void MoveRole(MapInputEventArgs args)
        {
            var start = this.SelectRoleEntity.RolePosition.Value.ToVector2Int();
            var end = args.ClickPosition;
            if (start == end) return;
            if (this.HasRole(args.ClickPosition, out var _)) return;

            var RolePassability = new RolePassabilityArgs();
            var path = this.navigateService.GetPath(this.GameWorld.GameMap, start, end, RolePassability);
            if (path.Count > 0)
            {
                this.SelectRoleEntity.MoveRole(path);
                // this.HexGrid.SetTestMap(path);
            }
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">地图输入事件参数</param>
        private void OnClick(object sender, MapInputEventArgs e)
        {
            if (e.ClickPosition.x == -1 || e.ClickPosition.y == -1) return;
            //Debug.Log($"ClickButtomCoed:{e.ClickButtomCoed} ClickPosition:{e.ClickPosition}");

            if (this.SelectRoleEntity != null)
            {
                if (e.ClickButtomCoed == MouseButton.LeftMouse
                    && this.SelectRoleEntity.RoleStatus.Status != RoleSkillStatus.Unselected)
                {
                    switch (this.SelectRoleEntity.RoleStatus.Status)
                    {
                        case RoleSkillStatus.Move:
                            this.MoveRole(e);
                            break;

                        case RoleSkillStatus.Skill:
                            this.ExecutionSkill(e);
                            break;

                        default:
                            throw new ArgumentException($"出现未处理的枚举:{this.SelectRoleEntity.RoleStatus.Status}");
                    }
                    this.DeSelectRoleEntity();
                }
                if (e.ClickButtomCoed == MouseButton.RightMouse && e.ClickPosition.ToVector3Int() != this.SelectRoleEntity.MoveComponent.CurrentRolePosition)
                {
                    this.DeSelectRoleEntity();
                }
            }
        }
    }
}