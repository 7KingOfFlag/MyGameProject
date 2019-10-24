﻿using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace OurGameName.DoMain.Entity.HexMap
{
    public class HexMapEditor : MonoBehaviour
    {
        public HexGrid hexGrid;

        /// <summary>
        /// 当前选择的单元格
        /// </summary>
        private HexCell selectedCell;

        /// <summary>
        /// 海拔
        /// </summary>
        [SerializeField]
        private int activeElevation;

        /// <summary>
        /// 是否设置海拔
        /// </summary>
        private bool ApplyElevation;

        /// <summary>
        /// 水平面
        /// </summary>
        public int activeWaterLevel;

        /// <summary>
        /// 是否设置水平面
        /// </summary>
        public bool ApplyWaterLevel;

        /// <summary>
        /// 颜色索引
        /// </summary>
        private int activeTerraintypeIndex = -1;

        /// <summary>
        /// 城市等级
        /// </summary>
        private int activeUrbanLevel;
        /// <summary>
        /// 是否设置城市等级
        /// </summary>
        private bool ApplyUrbanLevel;

        /// <summary>
        /// 城墙设置触发器
        /// </summary>
        OptionToggle wallMode;

        /// <summary>
        /// 刷子大小
        /// </summary>
        private int brushSize = 0;

        /// <summary>
        /// 鼠标是否拖拽
        /// </summary>
        private bool isDrag;
        /// <summary>
        /// 鼠标拖住方向
        /// </summary>
        private HexDirection DragDir;
        /// <summary>
        /// 拖拽的初始单元格
        /// </summary>
        HexCell previousCell;

        private void Awake()
        {
            HoverInit();
        }

        private void Update()
        {
            JudgeHover();
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleInput();
            }
            else
            {
                previousCell = null;
            }
        }

        /// <summary>
        /// 通过射线获取鼠标之下的单元格
        /// </summary>
        /// <returns>鼠标下方的单元格</returns>
        private HexCell GetCell()
        {
            Ray inputRay = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            HexCell cell;
            if (Physics.Raycast(inputRay, out hit))
            {
                //Debug.Log("Is Hit");
                cell = hexGrid.GetCell(hit.point);
            }
            else
            {
                cell = null;
            }
            return cell;
        }

        /// <summary>
        /// 处理鼠标点击操作
        /// </summary>
        private void HandleInput()
        {
            HexCell cell = GetCell();
            if (cell != null)
            {
                if (previousCell && previousCell != selectedCell)
                {
                    ValidateDrag(selectedCell);
                }
                EditCells(cell);

                previousCell = selectedCell;
                isDrag = true;
            }
            else
            {
                previousCell = null;
            }
        }

        /// <summary>
        /// 验证拖拽
        /// </summary>
        /// <param name="selectedCell">拖拽起始的单元格</param>
        private void ValidateDrag(HexCell selectedCell)
        {
            for (DragDir = HexDirection.NE; DragDir < HexDirection.NW; DragDir++)
            {
                if (previousCell.GetNeighbor(DragDir) == selectedCell)
                {
                    isDrag = true;
                    return;
                }
                else
                {
                    isDrag = false;
                }
            }
        }

        private void EditCell(HexCell cell)
        {
            if (cell == null) { return; }

            if (activeTerraintypeIndex >= 0)
            {
                cell.TerrainTypeIndex = (Terrain)activeTerraintypeIndex;
            }

            if (ApplyUrbanLevel == true)
            {
                cell.UrbanLevel = activeUrbanLevel;
            }

            if (wallMode != OptionToggle.Ignore)
            {
                cell.Wall = wallMode == OptionToggle.Yes;
            }
        }

        /// <summary>
        /// 编辑单元格
        /// </summary>
        /// <param name="center"></param>
        private void EditCells(HexCell center)
        {
            if (center == null)
            {
                return;
            }
            selectedCell = center;

            int centerX = center.coordinates.X;
            int centerZ = center.coordinates.Z;

            for (int r = 0, z = centerZ - brushSize; z <= centerZ; r++, z++)
            {
                for (int x = centerX - r; x <= centerX + brushSize; x++)
                {
                    EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
                }
            }

            for (int r = 0, z = centerZ + brushSize; z > centerZ; z--, r++)
            {
                for (int x = centerX - brushSize; x <= centerX + r; x++)
                {
                    EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
                }
            }
        }

        public void SetElevation(Slider slider) => activeElevation = (int)slider.value;

        public void SetBrushSize(Slider slider) => brushSize = (int)slider.value;

        public void SetTerraintypeIndex(int index)
        {
            activeTerraintypeIndex = index;
        }

        public void SetApplyElevation(Toggle toggle) => ApplyElevation = toggle.isOn;

        public void SetApplyWaterLevel(Toggle toggle) => ApplyWaterLevel = toggle.isOn;

        public void SetWaterLeve(Slider slider) => activeWaterLevel = (int)slider.value;

        public void SetApplyUrbanLevel(Toggle toggle) => ApplyUrbanLevel = toggle.isOn;

        public void SetUrbanLevel(Slider slider) => activeUrbanLevel = (int)slider.value;

        public void SetWallMode(int mode) => wallMode = (OptionToggle)mode;

        #region 鼠标悬浮

        HexCellMsgBox msgBox;

        /// <summary>
        /// 悬浮时所执行的函数
        /// </summary>
        private void OnHover()
        {
            msgBox.Show(CellUnderMouse, Input.mousePosition);
        }

        public void OnUnhover()
        {
            msgBox.Hidden();
        }

        /// <summary>
        /// 鼠标下的单元格
        /// </summary>
        private HexCell CellUnderMouse;

        /// <summary>
        /// 鼠标悬停显示提示所需要的时间
        /// </summary>
        private const float MouseHoverOverTime = 1.5f;

        /// <summary>
        /// 鼠标悬浮计时器
        /// </summary>
        private float hoverTimer;

        private event Action onHover, onUnhover;

        public void HoverInit()
        {
            hoverTimer = 0;
            CellUnderMouse = null;
            onHover += OnHover;
            onUnhover += OnUnhover;
            msgBox = GetComponentInChildren<HexCellMsgBox>();
        }

        /// <summary>
        /// 判断是否悬浮
        /// </summary>
        private void JudgeHover()
        {
            HexCell cell = GetCell();

            if (cell != null && cell == CellUnderMouse)
            {
                hoverTimer += Time.deltaTime;
            }
            else
            {
                hoverTimer = 0;
                CellUnderMouse = cell;
            }

            if (hoverTimer > MouseHoverOverTime)
            {
                onHover();
            }
            else
            {
                onUnhover();
            }
        }



        #endregion

        /// <summary>
        /// 设置触发器
        /// </summary>
        enum OptionToggle
        {
            /// <summary>
            /// 忽略
            /// </summary>
            Ignore,
            /// <summary>
            /// 是
            /// </summary>
            Yes,
            /// <summary>
            /// 否
            /// </summary>
            No
        }
    }
}
