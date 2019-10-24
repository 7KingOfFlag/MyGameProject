using System;
using UnityEngine;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.TileHexMap
{
    internal class HexTileCell
    {
        public event EventHandler<HexTileCellChangeEventArgs> CellChange;
        private string m_tileAssetName;
        private Vector3Int m_cellPosition;
        

        public HexTileCell(string TileAssetName, Vector3Int CellPosition)
        {
            m_tileAssetName = TileAssetName;
            m_cellPosition = CellPosition;
            
        }

        public HexTileCell() { }

        /// <summary>
        /// Tile单元格资源名称
        /// </summary>
        public string TileAssetName
        {
            get
            {
                return m_tileAssetName;
            }
            set
            {
                m_tileAssetName = value;
                CellChanging();
            }
        }

        /// <summary>
        /// 单元格位置
        /// </summary>
        public Vector3Int CeellPosition
        {
            get
            {
                return m_cellPosition;
            }
            set
            {
                m_cellPosition = value;
            }
        }

        /// <summary>
        /// 事件登记方法
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCellChange(HexTileCellChangeEventArgs e)
        {
            e.Raise(this,ref CellChange);
        }

        /// <summary>
        /// 单元格发生改变
        /// </summary>
        private void CellChanging()
        {
            OnCellChange(new HexTileCellChangeEventArgs(cellPosition: m_cellPosition));
        }
        
    }

    internal class HexTileCellChangeEventArgs: EventArgs
    {
        public Vector3Int CellPosition { get;private set; }

        public HexTileCellChangeEventArgs(Vector3Int cellPosition)
        {
            CellPosition = cellPosition;
        }
    }
}
