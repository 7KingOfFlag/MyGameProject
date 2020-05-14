namespace OurGameName.DoMain.GameWorld
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

        /// <summary>
        /// 将地图坐标转化为世界坐标
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        internal Vector3 CellToWorld(Vector3Int currentRolePosition)
        {
            return this.coordinateConverter.CellToWorld(currentRolePosition);
        }

        private void Awake()
        {
            this.SelectRoleEntity = null;
            this.coordinateConverter = this.HexGrid;
            this.navigateService = new NavigateService();
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

        private void Start()
        {
            this.MapInputEvent.NewClick += this.HexTileInputEvent_NewClick;
        }
    }
}