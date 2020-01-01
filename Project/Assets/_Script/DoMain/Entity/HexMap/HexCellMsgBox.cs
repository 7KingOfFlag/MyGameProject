using System.Text;
using TMPro;
using UnityEngine;

namespace OurGameName.DoMain.Entity.HexMap
{
    internal class HexCellMsgBox : MonoBehaviour
    {
        private HexCell hexCell;
        private TextMeshProUGUI msgBox;
        private new RectTransform transform;

        private void Awake()
        {
            msgBox = GetComponentInChildren<TextMeshProUGUI>();
            transform = GetComponent<RectTransform>();

            Hidden();
        }

        public void Show(HexCell cell, Vector3 postiton)
        {
            if (cell == null)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("坐标:({0},{1},{2})\n",
                cell.coordinates.X, cell.coordinates.Y, cell.coordinates.Z));
            builder.Append(string.Format("地形:{0}\n", cell.TerrainTypeIndex.BeString()));
            builder.Append(string.Format("城市等级:{0}\n", cell.UrbanLevel.ToString()));
            if (cell.Wall == true)
            {
                builder.Append("有城墙\n");
            }
            postiton.z = 0;
            transform.position = postiton;
            msgBox.SetText(builder.ToString());
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hidden()
        {
            transform.position = new Vector3(-1000f, 1000f, 0);
            msgBox.text = "";
        }
    }
}