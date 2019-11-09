using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace OurGameName.DoMain.Entity.TileHexMap.UI
{
    internal class HexMapMakeBox : MonoBehaviour
    {
        public TMP_Dropdown drpdMapSize;
        public TextMeshProUGUI txtWarning;

        public HexTileMapEditor context;

        public void OnBtnYesClick()
        {
            Vector2Int mapSize = new Vector2Int();

            switch (drpdMapSize.value)
            {
                case 0:
                    mapSize = new Vector2Int(16, 9);
                    break;
                case 1:
                    mapSize = new Vector2Int(32, 18);
                    break;
                case 2:
                    mapSize = new Vector2Int(64, 36);
                    break;
                case 3:
                    mapSize = new Vector2Int(128, 72);
                    break;
                default:
                    mapSize = new Vector2Int(16, 9);
                    Debug.LogWarning("地图创建参数 - 地图大小下拉列表框出现未设定选项");
                    break;
            }
            HexMapCreateArgs args = new HexMapCreateArgs(mapSize);
            context.CreateHexMap(args);
            Exit();
        }

        public void OnBtnExitClick()
        {
            Exit();
        }
        
        private void Exit()
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 六边形地图创建参数
    /// </summary>
    internal class HexMapCreateArgs
    {
        public Vector2Int MapSize { get; private set; }
        public HexMapCreateArgs(Vector2Int mapSize)
        {
            MapSize = mapSize;
        }
    }
}
