namespace OurGameName.DoMain.Entity.RoleSpace
{
    using OurGameName.DoMain.Attribute;
    using OurGameName.DoMain.Entity.Map;
    using OurGameName.DoMain.Entity.Map.Args;
    using OurGameName.DoMain.Entity.TileHexMap;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.Tilemaps;
    using UnityEngine.UIElements;

    internal class RoleManager : MonoBehaviour
    {
        /// <summary>
        ///
        /// </summary>
        public Tilemap BackgroundTileMap;

        /// <summary>
        ///
        /// </summary>
        public HexGrid hexGrid;

        /// <summary>
        /// 地图输入事件
        /// </summary>
        public MapInputEvent MapInputEvent;

        /// <summary>
        /// 输入控制器
        /// </summary>
        public PlayerInput PlayerInput;

        /// <summary>
        /// 选中的角色实体
        /// </summary>
        public RoleEntity SelectRoleEntity { get; set; }

        /// <summary>
        /// 将地图坐标转化为世界坐标
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        public Vector3 CellToWorld(Vector3Int cellPosition)
        {
            return BackgroundTileMap.CellToWorld(cellPosition);
        }

        private void Awake()
        {
            SelectRoleEntity = null;
        }

        private void HexTileInputEvent_NewClick(object sender, MapInputEventArgs e)
        {
            Debug.Log($"ClickButtomCoed:{e.ClickButtomCoed} ClickPosition:{e.ClickPosition}");
            if (SelectRoleEntity != null)
            {
                if (e.ClickButtomCoed == MouseButton.RightMouse)
                {
                    SelectRoleEntity.MoveRole(HexTileMetrics.ShortestPath(this.hexGrid, SelectRoleEntity.RolePosition, e.ClickPosition));
                }
                if (e.ClickButtomCoed == MouseButton.LeftMouse && e.ClickPosition.ToVector3Int() != SelectRoleEntity.MoveComponent.CurrentRolePosition)
                {
                    SelectRoleEntity.IsSelect = false;
                    SelectRoleEntity = null;
                }
            }
        }

        private void Start()
        {
            MapInputEvent.NewClick += HexTileInputEvent_NewClick;
        }
    }
}