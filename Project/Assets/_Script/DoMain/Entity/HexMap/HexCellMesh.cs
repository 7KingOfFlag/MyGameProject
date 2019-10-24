using System;
using UnityEngine;
using UnityEngine.UI;


namespace OurGameName.DoMain.Entity.HexMap
{
    public class HexCellMesh:MonoBehaviour
    {
        HexCell cell;

        public HexMesh terrain;

        private Material material;

        /// <summary>
        /// 特征物体管理器
        /// </summary>
        public HexFeatureManager feature;

        private void Awake()
        {
            terrain = GetComponentInChildren<HexMesh>();

            cell = new HexCell();

        }

        private void Start()
        {
            material = terrain.GetComponent<MeshRenderer>().sharedMaterial;
            Triangulate();
        }

        /// <summary>
        /// 向块中添加地图单元
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <param name="cell"></param>
        public void AddCell(HexCell cell)
        {
            this.cell = cell;
            cell.chunk = this;
            cell.transform.SetParent(transform, false);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            Triangulate();
        }

        public void Triangulate()
        {
            terrain.Clear();
            feature.Clear();

            Triangulate(cell);

            terrain.Apply();
            feature.Apply();
        }

        private void Triangulate(HexCell cell)
        {
            Vector3 center = cell.transform.localPosition;
            for (int i = 0; i < 6; i++)
            {
                terrain.AddTriangle(center, center + HexMetrics.hexagon[i], center + HexMetrics.hexagon[i + 1]);
            }
            RefreshTexture();
            feature.AddFeature(cell, cell.transform.position);
        }

        private void RefreshTexture()
        {
            material.SetTexture("_BaseColeMap", HexMetrics.texture[(int)cell.TerrainTypeIndex]);
        }
    }
}
