namespace OurGameName.DoMain.RoleSpace
{
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.Map;
    using OurGameName.DoMain.Map.Args;
    using OurGameName.DoMain.Map._2DMap;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.Tilemaps;
    using UnityEngine.UIElements;
    using OurGameName.DoMain.RoleSpace;
    using OurGameName.Interface;
    using System;
    using OurGameName.DoMain.Map.Service;
    using OurGameName.DoMain.Map.Extensions;
    using System.Linq;
    using Vector3 = UnityEngine.Vector3;
    using Boo.Lang;
    using OurGameName.DoMain.GameWorld;

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
        /// 角色
        /// </summary>
        public List<Role> Roles;

        /// <summary>
        /// 坐标转换器
        /// </summary>
        private ICoordinateConverter coordinateConverter;

        /// <summary>
        /// 寻路服务
        /// </summary>
        private INavigateService navigateService;

        /// <summary>
        /// 选中的角色实体
        /// </summary>
        public RoleEntity SelectRoleEntity { get; set; }

        #region Unity

        private void Awake()
        {
            this.SelectRoleEntity = null;
            this.coordinateConverter = this.HexGrid;
            this.navigateService = new NavigateService();
            this.Roles = this.LoadRoles();
        }

        private void Start()
        {
            this.MapInputEvent.NewClick += this.HexTileInputEvent_NewClick;
            this.BuildRoleEntity(this.Roles);
        }

        #endregion Unity

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
        /// 生成角色实体
        /// </summary>
        /// <param name="roles"></param>
        private void BuildRoleEntity(List<Role> roles)
        {
            throw new NotImplementedException();
        }

        private void HexTileInputEvent_NewClick(object sender, MapInputEventArgs e)
        {
            if (e.ClickPosition.x == -1 || e.ClickPosition.y == -1) return;
            //Debug.Log($"ClickButtomCoed:{e.ClickButtomCoed} ClickPosition:{e.ClickPosition}");

            if (this.SelectRoleEntity != null)
            {
                if (e.ClickButtomCoed == MouseButton.LeftMouse)
                {
                    var start = this.SelectRoleEntity.RolePosition;
                    var end = e.ClickPosition;
                    if (start == end) return;

                    var RolePassability = new RolePassabilityArgs();
                    var path = this.navigateService.GetPath(this.GameWorld.GameMap, start, end, RolePassability);
                    if (path.Count > 0)
                    {
                        this.SelectRoleEntity.MoveRole(path);
                        this.HexGrid.SetTestMap(path);

                        Debug.Log(string.Join("=>", path));
                    }
                }
                if (e.ClickButtomCoed == MouseButton.RightMouse && e.ClickPosition.ToVector3Int() != this.SelectRoleEntity.MoveComponent.CurrentRolePosition)
                {
                    this.SelectRoleEntity.IsSelect = false;
                    this.SelectRoleEntity = null;
                }
            }
        }

        /// <summary>
        /// 载入角色
        /// </summary>
        /// <returns></returns>
        private List<Role> LoadRoles()
        {
            return new List<Role>
            {
                new Role("姬","掘突", new DateTime(0,8,2),4,300)
                {
                    Position = new Vector3Int(0,0,0),
                },
                new Role("石", "腊", new DateTime(0,4,5),4,300)
                {
                    Position = new Vector3Int(2,2,0),
                },
            };
        }
    }
}