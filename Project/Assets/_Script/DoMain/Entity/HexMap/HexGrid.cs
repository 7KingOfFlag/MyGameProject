using UnityEngine;
using UnityEngine.UI;

namespace OurGameName.DoMain.Entity.HexMap
{
    public class HexGrid : MonoBehaviour
    {
        /// <summary>
        /// 地图单元格总数
        /// </summary>
        private int cellCountX, cellCountZ;

        /// <summary>
        /// 地图单元格预制体
        /// </summary>
        public HexCell cellPrefab;

        /// <summary>
        /// 网格视图 用于显示信息
        /// </summary>
        private Canvas GridCanvas;

        public Text CellLabel;
        public Image CellImage;

        /// <summary>
        /// 地图块预制体
        /// </summary>
        public HexCellMesh cellMeshPrefab;

        /// <summary>
        /// 地图单元格数组 地图上数据的存储对象
        /// </summary>
        public HexCell[,] HexCells { get; private set; }

        /// <summary>
        /// 地图块数组
        /// </summary>
        private HexCellMesh[,] cellMeshs;

        /// <summary>
        /// 地图颜色数组
        /// </summary>
        public Texture2D[] texture;

        /// <summary>
        /// 随机数种子
        /// </summary>
        public int seed;

        private void Awake()
        {
            HexMetrics.InitHashGrid(seed);
            HexMetrics.texture = texture;
            GridCanvas = GetComponentInChildren<Canvas>();
        }

        private void OnEnable()
        {
            if (!HexMetrics.noiseSource)
            {
                HexMetrics.InitHashGrid(seed);
            }
            if (HexMetrics.texture == null)
            {
                HexMetrics.texture = texture;
            }
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        /// <param name="CellCountX">地图单元格数量</param>
        /// <param name="CellCountZ">地图单元格数量</param>
        public void Init(int CellCountX, int CellCountZ)
        {
            cellCountX = CellCountX;
            cellCountZ = CellCountZ;

            CreateMeshs();
            CreateCell();
        }

        /// <summary>
        /// 新建地图单元格
        /// </summary>
        private void CreateCell()
        {
            HexCells = new HexCell[cellCountX, cellCountZ];
            for (int z = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++)
                {
                    CreateCell(x, z);
                }
            }

            for (int z = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++)
                {
                    SetCellNeighbor(x, z, HexCells[x, z]);
                }
            }
        }

        /// <summary>
        /// 创建对应索引的地图单元格
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        private void CreateCell(int x, int z)
        {
            HexCell cell = HexCells[x, z] = Instantiate(cellPrefab);

            SetCellPosition(cell, x, z);

            AddCellToChunk(x, z, cell);
        }

        /// <summary>
        /// 设置地图单元格在场景中的坐标
        /// </summary>
        /// <param name="cell">地图单元格</param>
        /// <param name="x">单元格数组索引</param>
        /// <param name="z">单元格数组索引</param>
        private void SetCellPosition(HexCell cell, int x, int z)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);
            cell.transform.localPosition = position;
            cell.coordinates = HexCoordinates.FromOffSetCoordinates(x, z);
        }

        /// <summary>
        /// 设置单元格相邻关系
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <param name="cell"></param>
        //  TODO:有BUG只能生成 X>=Z的地图否则地图会出错
        private void SetCellNeighbor(int x, int z, HexCell cell)
        {
            if (x > 0)
            {
                cell.SetNeighBor(HexDirection.W, HexCells[x - 1, z]);
            }
            if (z > 0)
            {
                if ((z & 1) == 0) //行数是偶数
                {
                    cell.SetNeighBor(HexDirection.SE, HexCells[x, z - 1]);
                    if (x > 0)
                    {
                        cell.SetNeighBor(HexDirection.SW, HexCells[x - 1, z - 1]);
                    }
                }
                else
                {
                    cell.SetNeighBor(HexDirection.SW, HexCells[x, z - 1]);

                    // TODO:临时修改 之后修正
                    /*
                    if (x < cellCountZ + chunkCountZ - 1)
                    {
                        cell.SetNeighBor(HexDirection.SE, HexCells[x + 1, z - 1]);
                    }*/
                    if (x < cellCountZ - 1)
                    {
                        cell.SetNeighBor(HexDirection.SE, HexCells[x + 1, z - 1]);
                    }
                }
            }
        }

        /// <summary>
        /// 将单元格添加到地图块中
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <param name="cell"></param>
        private void AddCellToChunk(int x, int z, HexCell cell)
        {
            cellMeshs[x, z].AddCell(cell);
        }

        /// <summary>
        /// 创建地图块
        /// </summary>
        private void CreateMeshs()
        {
            cellMeshs = new HexCellMesh[cellCountX, cellCountZ];

            for (int z = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++)
                {
                    HexCellMesh cellMesh = cellMeshs[x, z] = Instantiate(cellMeshPrefab, transform);
                }
            }
        }

        /// <summary>
        /// 从世界位置上获取相应的地图单元格
        /// </summary>
        /// <param name="position">世界坐标</param>
        /// <returns>世界坐标上的地图单元格对象</returns>
        public HexCell GetCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            Point point = coordinates.ToListIndex();
            return HexCells[point.X, point.Y];
        }

        /// <summary>
        /// 根据六边形坐标获取地图单元格对象
        /// </summary>
        /// <param name="Coordinates"></param>
        /// <returns>返回地图是的单元格 单元格不存在时返回null</returns>
        public HexCell GetCell(HexCoordinates Coordinates)
        {
            int x = Coordinates.X + Coordinates.Z / 2;
            if (isSlopOver(x, Coordinates.Z))
            {
                return HexCells[x, Coordinates.Z];
            }
            else
            {
                return (HexCell)null;
            }
        }

        /// <summary>
        /// 坐标是否在地图大小之内
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="z">纵坐标</param>
        /// <returns>在地图内返回:true 不在返回:false</returns>
        private bool isSlopOver(int x, int z)
        {
            if (x * z < 0)
            {
                return false;
            }
            else if (x > cellCountX && z > cellCountZ)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 清空地图
        /// 清空HexCells 和 chunks 对象中的元素
        /// </summary>
        public void Empty()
        {
            if (cellMeshs != null && cellMeshs[0, 0] != null)
            {
                foreach (var item in cellMeshs)
                {
                    Destroy(item.gameObject);
                }
            }
        }

        /*
        /// <summary>
        /// 刷新地图
        /// </summary>
        public void Refresh()
        {
            meshCollider.sharedMesh = null;
            hexMesh.Triangulate(cells);
            meshCollider.sharedMesh = hexMesh.hexMesh;
        }
        */
    }
}