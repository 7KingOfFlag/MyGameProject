using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private CanvasGroup canvasGroup;
        /// <summary>
        /// 文本框实例字典
        /// </summary>
        private Dictionary<Vector2Int, TextMeshProUGUI> txtDict;
        /// <summary>
        /// 文本显示模式
        /// </summary>
        private TxtShowModeEnum m_txtShowMode = TxtShowModeEnum.Blank;
        private TxtShowModeEnum TxtShowMode
        {
            get
            {
                return m_txtShowMode;
            }
            set
            {
                if (value == m_txtShowMode)
                {
                    return;
                }
                if (value != TxtShowModeEnum.Blank)
                {
                    m_previousTxtShowMode = m_txtShowMode;
                }
                m_txtShowMode = value;
                ShfitTxtShowMode(m_txtShowMode);
            }
        }

        void Awake()
        {
            hexCellCanvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
            txtDict = new Dictionary<Vector2Int, TextMeshProUGUI>();
            canvasGroup.alpha = 0;
        }

        TxtShowModeEnum m_previousTxtShowMode;
        /// <summary>
        /// 切换文本显示模式
        /// </summary>
        /// <param name="txtShowMode"></param>
        private void ShfitTxtShowMode(TxtShowModeEnum txtShowMode)
        {
            switch (txtShowMode)
            {
                case TxtShowModeEnum.Blank:
                    canvasGroup.alpha = 0;
                    break;
                case TxtShowModeEnum.ShowCoordinate:
                    canvasGroup.alpha = 1;
                    if (m_previousTxtShowMode == TxtShowModeEnum.ShowCoordinate) return;

                    ShowCoordinate();
                    break;
                case TxtShowModeEnum.ShowDistance:
                    canvasGroup.alpha = 1;
                    if (m_previousTxtShowMode == TxtShowModeEnum.ShowDistance) return;

                    ShowDistance();
                    break;
                default:
                    Debug.LogWarning($"In {gameObject.name} has undefined enum");
                    break;
            }
        }

        /// <summary>
        /// 显示距离
        /// </summary>
        private void ShowDistance()
        {
            foreach (var item in txtDict)
            {
                var txt = item.Value;
                txt.text = $"{item.Key.x}";
                txt.fontSize = 0.4f;
            }
        }
        /// <summary>
        /// 显示坐标
        /// </summary>
        private void ShowCoordinate()
        {
            foreach (var item in txtDict)
            {
                var txt = item.Value;
                txt.text = $"{item.Key.x},{item.Key.y}";
                txt.fontSize = 0.3f;
            }
        }

        /// <summary>
        /// 生成位置节点信息文本框
        /// </summary>
        public void BuildHexPosTxt(int InitSzieX, int InitSzieY)
        {
            ClearInstaceTxt();
            txtDict.Clear();
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
            foreach (var item in txtDict.Values)
            {
                Destroy(item.gameObject);
            }
            txtDict.Clear();
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
            txtDict.Add(new Vector2Int(txtPosition.x, txtPosition.y), txt);
            txt.SetText($"{txtPosition.x},{txtPosition.y}");
        }

        /// <summary>
        /// 设置文本视图显示模式<-通关Unity事件机制调用
        /// </summary>
        /// <param name="dropdown"></param>
        public void SetTxtShowMode(TMP_Dropdown dropdown)
        {
            TxtShowMode = (TxtShowModeEnum)dropdown.value;
            Debug.Log($"TxtShowMode = {TxtShowMode}");

        }

        /// <summary>
        /// 文本显示模式枚举
        /// </summary>
        private enum TxtShowModeEnum
        {
            /// <summary>
            /// 不显示
            /// </summary>
            Blank,
            /// <summary>
            /// 显示坐标
            /// </summary>
            ShowCoordinate,
            /// <summary>
            /// 显示距离
            /// </summary>
            ShowDistance
        }
    }
}
