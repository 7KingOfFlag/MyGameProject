using System.Collections.Generic;
using OurGameName.DoMain.Entity;
using UnityEngine;
using OurGameName.DoMain.Entity.HexMap;
using OurGameName.DoMain.Entity.RoleSpace;


namespace OurGameName.Manager
{
    public class GameController : MonoBehaviour
    {
        List<Role> RoleList;
        HexCell[,] hexCells;
        public HexGrid HexGrid;
        public Canvas canvas;
        private void Awake()
        {
            RoleList = new List<Role>();

            Role[] roles = new Role[]
            {
               new Role("张","三","2018.01.01",10,10,10,10,10,0,"无所属","无"),
               new Role("李","四","2018.02.06",10,10,10,10,10,0,"天龙派","外门弟子"),
               new Role("王","五","2018.03.11",10,10,10,10,10,0,"灵山","内门弟子"),
               new Role("赵","六","2018.04.16",10,10,10,10,10,0,"逍遥派","长老")
            };

            foreach (var item in roles)
            {
                RoleList.Add(item);
            }
        }

        private void Start()
        {
        }

        public void SaveGame()
        {
            hexCells = HexGrid.HexCells;
            GameArchive gameArchive = new GameArchive(RoleList, hexCells);
            string path = Application.dataPath + "/Data/Save/Save1.json";
            if (! System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "//Data//Save");
            }
            gameArchive.Save(path);

            Debug.Log("保存完成");
        }

        public void LoadGame()
        {
            Empty();
            string path = Application.dataPath + "/Data/Save/Save1.json";
            GameArchive Archive = GameArchive.Load(path);
            if (Archive != null)
            {
                if (Archive.HexCellSerialization != null)
                {
                    HexGrid.Init(Archive.X, Archive.Z);
                    HexCellSerialization.CoverageHexMap(HexGrid.HexCells, Archive.HexCellSerialization);
                }
                Debug.Log("载入完成");
            }
        }
        #region 生成地图

        public MapMakModel MapMakMoldePrefab;
        private MapMakModel model;

        /// <summary>
        /// 显示地图生成对话框
        /// </summary>
        public void ShowMakeBox()
        {
            model = Instantiate(MapMakMoldePrefab);
            model.transform.SetParent(canvas.transform);
            model.Show();
        }

        /// <summary>
        /// 生成地图
        /// </summary>
        public void MakeMap()
        {
            Empty();
            model.GetMapMakMolde();
            model.MakeMap(HexGrid);
            Debug.Log("生成完成");
            model.Exit();
        }

        #endregion
        /// <summary>
        /// 清空地图
        /// </summary>
        public void Empty()
        {
            HexGrid.Empty();
            Debug.Log("清空完成");
        }

    }
}
