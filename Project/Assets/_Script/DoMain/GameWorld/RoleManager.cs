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

    internal class RoleManager : MonoBehaviour
    {
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
        }

        private void HexTileInputEvent_NewClick(object sender, MapInputEventArgs e)
        {
            Debug.Log($"ClickButtomCoed:{e.ClickButtomCoed} ClickPosition:{e.ClickPosition}");
            if (this.SelectRoleEntity != null)
            {
                if (e.ClickButtomCoed == MouseButton.RightMouse)
                {
                    // this.SelectRoleEntity.MoveRole(HexTileMetrics.ShortestPath(this.hexGrid, this.SelectRoleEntity.RolePosition, e.ClickPosition));
                }
                if (e.ClickButtomCoed == MouseButton.LeftMouse && e.ClickPosition.ToVector3Int() != this.SelectRoleEntity.MoveComponent.CurrentRolePosition)
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