using OurGameName.DoMain.Attribute;
using UnityEngine;

namespace OurGameName.DoMain.Entity.HexMap
{
    /// <summary>
    /// 特征物体管理器
    /// </summary>
    public class HexFeatureManager : MonoBehaviour
    {
        /// <summary>
        /// 特征物体预制体
        /// </summary>
        public Transform[] urbanPrefabs;

        /// <summary>
        /// 城墙网格
        /// </summary>
        public HexMesh wall;

        /// <summary>
        /// 特征物体容器
        /// </summary>
        private Transform container;

        private void Awake()
        {
            wall = GetComponentInChildren<HexMesh>();
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            if (container != null)
            {
                Destroy(container.gameObject);
            }
            container = new GameObject("特征物体容器").transform;
            container.SetParent(transform.parent, false);

            wall.Clear();
        }

        /// <summary>
        /// 应用
        /// </summary>
        public void Apply()
        {
            wall.Apply();
        }

        /// <summary>
        /// 添加特征物体
        /// </summary>
        /// <param name="position"></param>
        public void AddFeature(HexCell cell, Vector3 position)
        {
            Float2 hash = HexMetrics.SampleHashGrid(position);

            //设置特征物体出现概率与物体等级的相关性
            if (hash.a >= cell.UrbanLevel * 0.25f)
            {
                return;
            }

            Transform instance = Instantiate(urbanPrefabs[cell.UrbanLevel - 1]);
            position.y += instance.localScale.y * 0.5f;
            instance.localPosition = position;
            instance.localRotation = Quaternion.Euler(0f, 360f * hash.b, 0f);
            instance.SetParent(container, false);
        }

        /// <summary>
        /// 控制城墙的添加
        /// </summary>
        /// <param name="nearLeft">近端左<-坐标</param>
        /// <param name="nearRight">近端右->坐标</param>
        /// <param name="nearCell">近端单元格</param>
        /// <param name="farLeft">远端左<-坐标</param>
        /// <param name="farRight">远端右->坐标</param>
        /// <param name="farCell">远端单元格</param>
        public void AddWall(
            Vector3 nearLeft, Vector3 nearRight, HexCell nearCell,
            Vector3 farLeft, Vector3 farRight, HexCell farCell
            )
        {
            //仅在有城墙的单元格与没有城墙的单元格相连时才添加城墙
            if (nearCell.Wall != farCell.Wall &&
                nearCell.TerrainTypeIndex != Terrain.coast)
            {
                AddWallSegment(nearLeft, nearRight, farLeft, farRight);
            }
        }

        public void AddWall(
            Vector3 c1, HexCell cell1,
            Vector3 c2, HexCell cell2,
            Vector3 c3, HexCell cell3
            )
        {
            if (cell1.Wall == true)
            {
                if (cell2.Wall == true)
                {
                    if (cell3.Wall == false)
                    {
                        AddWallSegment(c3, cell3, c1, cell1, c2, cell2);
                    }
                }
                else if (cell3.Wall == true)
                {
                    AddWallSegment(c2, cell2, c3, cell3, c1, cell1);
                }
                else
                {
                    AddWallSegment(c1, cell1, c2, cell2, c3, cell3);
                }
            }
            else if (cell2.Wall)
            {
                if (cell3.Wall)
                {
                    AddWallSegment(c1, cell1, c2, cell2, c3, cell3);
                }
                else
                {
                    AddWallSegment(c2, cell2, c3, cell3, c1, cell1);
                }
            }
            else if (cell3.Wall)
            {
                AddWallSegment(c3, cell3, c1, cell1, c2, cell2);
            }
        }

        /// <summary>
        /// 在四个点中间创建一道城墙
        /// </summary>
        /// <param name="nearLeft"></param>
        /// <param name="nearRight"></param>
        /// <param name="farLeft"></param>
        /// <param name="farRight"></param>
        private void AddWallSegment(Vector3 nearLeft, Vector3 nearRight, Vector3 farLeft, Vector3 farRight)
        {
            Vector3 left = Vector3.Lerp(nearLeft, farLeft, 0.5f);
            Vector3 right = Vector3.Lerp(nearRight, farRight, 0.5f);

            Vector3 leftThicknessOffset =
                HexMetrics.WallThicknessOffset(nearLeft, farLeft);

            Vector3 rightThicknessOffset =
                HexMetrics.WallThicknessOffset(nearRight, farRight);

            float leftTop = left.y + HexMetrics.wallHeight;
            float rightTop = right.y + HexMetrics.wallHeight;

            Vector3 v1, v2, v3, v4;
            v1 = v3 = left - leftThicknessOffset;
            v2 = v4 = right - rightThicknessOffset;
            v3.y = leftTop;
            v4.y = rightTop;
            wall.AddQuad(v1, v2, v3, v4);

            Vector3 t1 = v3, t2 = v4;

            v1 = v3 = left + leftThicknessOffset;
            v2 = v4 = right + rightThicknessOffset;
            v3.y = leftTop;
            v4.y = rightTop;
            wall.AddQuad(v1, v2, v3, v4);

            wall.AddQuad(t1, t2, v3, v4);
        }

        /// <summary>
        /// 为角落添加城墙的
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="pivotCell"></param>
        /// <param name="left"></param>
        /// <param name="leftCell"></param>
        /// <param name="right"></param>
        /// <param name="rightCell"></param>
        private void AddWallSegment(
            Vector3 pivot, HexCell pivotCell,
            Vector3 left, HexCell leftCell,
            Vector3 right, HexCell rightCell
            )
        {
            AddWallSegment(pivot, pivot, left, right);
        }
    }
}