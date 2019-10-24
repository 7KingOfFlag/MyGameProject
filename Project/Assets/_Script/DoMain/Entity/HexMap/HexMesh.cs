using System.Collections.Generic;
using UnityEngine;
using System;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.HexMap
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        public Mesh hexMesh;
        /// <summary>
        /// 网格顶点
        /// </summary>
        [NonSerialized] List<Vector3> vertices;
        /// <summary>
        /// 网格三角形
        /// </summary>
        [NonSerialized] List<int> triangles;
        /// <summary>
        /// 网格顶点上的颜色
        /// </summary>
        [NonSerialized] List<Color> colors;

        /// <summary>
        /// 颜色使能
        /// </summary>
        public bool ColorEnabled = true;

        /// <summary>
        /// 网格碰撞体
        /// </summary>
        MeshCollider meshCollider;

        /// <summary>
        /// 碰撞体使能
        /// </summary>
        public bool ColliderEnabled = true;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
            if (ColliderEnabled == true)
            {
                meshCollider = gameObject.AddComponent<MeshCollider>();
            }
            hexMesh.name = "Hex Mesh";
            //vertices = new List<Vector3>();
            //triangles = new List<int>();
            //colors = new List<Color>();
            Debug.Log(" is Awake");
        }

        public void Apply()
        {
            hexMesh.vertices = vertices.ToArray();
            ListPool<Vector3>.Add(vertices);
            hexMesh.triangles = triangles.ToArray();
            ListPool<int>.Add(triangles);
            hexMesh.colors = colors.ToArray();
            ListPool<Color>.Add(colors);
            hexMesh.RecalculateNormals();

            if (ColliderEnabled == true)
            {
                meshCollider.sharedMesh = hexMesh;
            }
        }

        public void Clear()
        {
            hexMesh.Clear();
            vertices = ListPool<Vector3>.Get();
            triangles = ListPool<int>.Get();
            colors = ListPool<Color>.Get();
            if (ColliderEnabled == true)
            {
                meshCollider.sharedMesh.Clear();
            }
        }

        

        /// <summary>
        /// 向网格中添加一个三角形
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(Perturb(v1));
            vertices.Add(Perturb(v2));
            vertices.Add(Perturb(v3));

            for (int i = 0; i < 3; i++)
            {
                triangles.Add(vertexIndex + i);
            }
        }

        /// <summary>
        /// 向网格中的三角形三个顶点添加统一的颜色
        /// </summary>
        public void AddTriangleColor(Color color)
        {
            for (int i = 0; i < 3; i++)
            {
                colors.Add(color);
            }
        }

        /// <summary>
        /// 向网格中的三角形每一个顶点分别添加颜色
        /// </summary>
        public void AddTriangleColor(Color c1, Color c2, Color c3)
        {
            colors.Add(c1);
            colors.Add(c2);
            colors.Add(c3);

        }

        /// <summary>
        /// 向网格中添加一个四边形
        /// </summary>
        public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            int vertexIndex = vertices.Count;

            vertices.Add(Perturb(v1));
            vertices.Add(Perturb(v2));
            vertices.Add(Perturb(v3));
            vertices.Add(Perturb(v4));

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);
        }

        /// <summary>
        /// 设置四边形的颜色
        /// </summary>
        public void AddQuadColor(Color c1, Color c2)
        {
            colors.Add(c1);
            colors.Add(c1);
            colors.Add(c2);
            colors.Add(c2);
        }

        /// <summary>
        /// 生成地图扰动
        /// </summary>
        /// <param name="postion">位置</param>
        /// <returns></returns>
        private Vector3 Perturb(Vector3 postion)
        {
            /*
            Vector4 sample = HexMetrics.SmapleNoise(postion);
            postion.x += sample.x * HexMetrics.cellPreturbStrength;
            postion.y += sample.y * HexMetrics.cellPreturbStrength;
            postion.z += sample.z * HexMetrics.cellPreturbStrength;
            */
            return postion;
        }
    }
}
