using OurGameName.DoMain.Attribute;
using OurGameName.DoMain.Entity.TileHexMap;
using OurGameName.DoMain.Entity.TileHexMap.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


namespace OurGameName.DoMain.Entity.RoleSpace
{
    internal class RoleManager : MonoBehaviour
    {
        public HexTileInputEvent HexTileInputEvent;
        public Tilemap BackgroundTileMap;
        public PlayerInput PlayerInput;
        public RoleEntity SelectRoleEntity { private get; set; }

        void Awake()
        {
            SelectRoleEntity = null;
        }

        private void Start()
        {
            HexTileInputEvent.NewClick += HexTileInputEvent_NewClick;
        }

        private void HexTileInputEvent_NewClick(object sender, HexTileInputEventArgs e)
        {
            //Debug.Log($"ClickButtomCoed:{e.ClickButtomCoed} ClickPosition:{e.ClickPosition}");
            if (SelectRoleEntity != null)
            {
                if (e.ClickButtomCoed == MouseButton.RightMouse)
                {
                    SelectRoleEntity.MoveRole(e.ClickPosition);
                }
                if (e.ClickButtomCoed == MouseButton.LeftMouse && e.ClickPosition.ToVector3Int() != SelectRoleEntity.MoveComponent.CurrentRolePosition)
                {
                    SelectRoleEntity.IsSelect = false;
                    SelectRoleEntity = null;
                }
            }
        }

        /// <summary>
        /// 将地图坐标转化为世界坐标
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        public Vector3 CellToWorld(Vector3Int cellPosition)
        {
            return BackgroundTileMap.CellToWorld(cellPosition);
        }
    }

}
