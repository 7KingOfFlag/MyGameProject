using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using OurGameName.DoMain.Entity.HexMap;
using OurGameName.DoMain.Entity.RoleSpace;

namespace OurGameName.DoMain.Entity
{
    /// <summary>
    /// 游戏存档 工具类
    /// </summary>
    public class GameArchive
    {
        /// <summary>
        /// 人物列表
        /// </summary>
        public List<Role> RoleList { get; set; }

        /// <summary>
        /// 地图文件序列化类
        /// 是精简版的地图类
        /// </summary>
        public HexCellSerialization[] HexCellSerialization;

        /// <summary>
        /// 地图大小
        /// </summary>
        public int X, Z;

        /// <summary>
        /// 新建游戏存档类
        /// </summary>
        /// <param name="roleList">人物列表</param>
        /// <param name="hexCells">游戏地图集合</param>
        public GameArchive(List<Role> roleList, HexCell[,] hexCells)
        {
            RoleList = roleList;
            HexCellSerialization = HexMap.HexCellSerialization.ShiftCells(hexCells);
            X = hexCells.GetLength(0);
            Z = hexCells.GetLength(1);
        }

        public GameArchive()
        {
        }

        /// <summary>
        /// 保存游戏
        /// </summary>
        /// <param name="sevePath">游戏保存路径</param>
        public void Save(string sevePath)
        {
            if (File.Exists(sevePath))//检查文件是否存在
            {
                File.Delete(sevePath);
            }

            using (StreamWriter writer = File.CreateText(sevePath))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string SaveJosn = serializer.Serialize(this);
                writer.Write(SaveJosn);
            }
        }

        /// <summary>
        /// 读取游戏存档,保存在当前游戏存档类中
        /// </summary>
        /// <param name="sevePath">游戏存档路径</param>
        /// <returns>
        ///     返回存档保存
        ///     成功返回 0
        ///     失败返回 1
        /// </returns>
        public static GameArchive Load(string sevePath)
        {
            if (File.Exists(sevePath))
            {
                using (StreamReader reader = File.OpenText(sevePath))
                {
                    string ArchiveJson = reader.ReadToEnd();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    GameArchive archive = serializer.Deserialize<GameArchive>(ArchiveJson);

                    return archive;
                }
            }
            return null;
        }
    }
}