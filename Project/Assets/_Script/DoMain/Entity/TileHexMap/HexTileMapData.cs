using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using UnityEngine;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    internal class HexTileMapData
    {
        public HexCell[] HexTileCells;
        private Vector2Int m_mapSize;
        public Vector2Int MapSize
        {
            get
            {
                return m_mapSize;
            }
        }
        public HexTileMapData(int x,int y)
        {
            HexTileCells = new HexCell[x * y];
            m_mapSize = new Vector2Int(x, y);
        }
        
        public HexTileMapData()
        {

        }
        
        public void SaveToFile(string SavePath)
        {
            if (File.Exists(SavePath))//检查文件是否存在
            {
                File.Delete(SavePath);
            }

            using (StreamWriter writer = File.CreateText(SavePath))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string SaveJosn = serializer.Serialize(this);
                writer.Write(SaveJosn);
            }
        }

        public static HexTileMapData LoadForFile(string SavePath)
        {
            if (File.Exists(SavePath) == true)
            {
                using (StreamReader reader = File.OpenText(SavePath))
                {

                    string DataJson = reader.ReadToEnd();
                    UnityEngine.Debug.Log(DataJson);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    HexTileMapData data = serializer.Deserialize<HexTileMapData>(DataJson);

                    return data;
                }
            }
            else
            {
                UnityEngine.Debug.LogError($"文件:{SavePath}不存在");
            }
            return null;
        }
    }
}
