using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    /// <summary>
    /// 六边形节点表面画布
    /// </summary>
    internal class HexCellDebugTxtCanvas : MonoBehaviour
    {
        /// <summary>
        /// 背景tileMap
        /// </summary>
        public Tilemap tilemapBackground;

        /// <summary>
        /// 节点位置文本框预制体
        /// </summary>
        public AssetReference hexPosTxtPrefab;

        private Canvas hexCellCanvas;
        /// <summary>
        /// 文本框实例字典
        /// </summary>
        private Dictionary<Vector2Int, TextMeshProUGUI> txtInstanceDict;

        void Awake()
        {
            hexCellCanvas = GetComponent<Canvas>();
            txtInstanceDict = new Dictionary<Vector2Int, TextMeshProUGUI>();
            hexCellCanvas.enabled = false;
        }

        void Start()
        {
            BuildHexPosTxt(50, 50);
        }

        /// <summary>
        /// 生成位置节点信息文本框
        /// </summary>
        public void BuildHexPosTxt(int InitSzieX, int InitSzieY)
        {
            ClearInstaceTxt();
            txtInstanceDict.Clear();
            for (int x = 0; x < InitSzieX; x++)
            {
                for (int y = 0; y < InitSzieY; y++)
                {
                    InstantatePosTxt(x, y);
                }
            }
        }
        /// <summary>
        /// 清除生成的实例文本框
        /// </summary>
        private void ClearInstaceTxt()
        {
            foreach (var item in txtInstanceDict.Values)
            {
                Destroy(item.gameObject);
            }
            txtInstanceDict.Clear();
        }

        /// <summary>
        /// 在坐标位置实例化位置信息文本框
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void InstantatePosTxt(int x, int y)
        {
            hexPosTxtPrefab.InstantiateAsync(
                position: tilemapBackground.CellToWorld(new Vector3Int(x, y, 0)),
                rotation: new Quaternion(0, 0, 0, 0),
                parent: transform).Completed += HexTileMapEditor_Completed;
        }

        private void HexTileMapEditor_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            var txt = obj.Result.GetComponent<TextMeshProUGUI>();
            var txtPosition = tilemapBackground.WorldToCell(txt.transform.position);
            txt.SetText($"{txtPosition.x},{txtPosition.y}");
            txtInstanceDict.Add(new Vector2Int(txtPosition.x, txtPosition.y), txt);
        }

        public void IsShowNumberGrid(Toggle toggle)
        {
            hexCellCanvas.enabled = toggle.isOn;
        }
    }
}
