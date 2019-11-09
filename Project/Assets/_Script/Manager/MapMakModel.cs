using OurGameName.DoMain.Entity.HexMap;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using OurGameName.DoMain.Attribute;
using Terrain = OurGameName.DoMain.Entity.HexMap.Terrain;
using TMPro;

namespace OurGameName.Manager
{
    /// <summary>
    /// 地图生成参数模型
    /// </summary>
    public class MapMakModel : MonoBehaviour
    {
        /// <summary>
        /// 地图大小 X
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// 地图大小 Y
        /// </summary>
        public int Z { get; private set; }

        /// <summary>
        /// 地图中湖泊生成系数
        /// </summary>
        public int Lasks { get; set; }
        /// <summary>
        /// 地图中草原的生成系数
        /// </summary>
        public int Grassland { get; set; }
        /// <summary>
        /// 地图中山脉的生成系数
        /// </summary>
        public int Mountains { get; set; }
        /// <summary>
        /// 地图中沙漠的生成系数
        /// </summary>
        public int Desert { get; set; }

        /// <summary>
        /// 地图单元格总数
        /// </summary>
        private int MapSize
        {
            get
            {
                return X * Z;
            }
        }

        /// <summary>
        /// 随机数生成器
        /// </summary>
        public System.Random random { get; private set; }

        public TMP_InputField inputX, inputZ;
        public Slider SliderLasks,
            SliderGrassland,
            SliderMountains,
            SliderDesert;

        private void Start()
        {

            DateTime time = DateTime.Now;
            int Seed = (int)time.Ticks;
            Debug.Log(string.Format("Seed = {0}", Seed));
            random = new System.Random(Seed);
        }

        /// <summary>
        /// 从对话窗读取地图生成数据
        /// </summary>
        public void GetMapMakMolde()
        {
            X = int.Parse(inputX.text);
            Z = int.Parse(inputZ.text);
            Lasks = (int)SliderLasks.value;
            Grassland = (int)SliderGrassland.value;
            Mountains = (int)SliderMountains.value;
            Desert = (int)SliderDesert.value;
            Debug.Log(
                string.Format("Lasks = {0}\nGrassland = {1}\n Mountains = {2}\n Desert = {3}\n",
                Lasks, Grassland, Mountains, Desert));

        }

        #region 生成地图

        /// <summary>
        /// 生成地图
        /// </summary>
        public void MakeMap(HexGrid hexGrid)
        {
            hexGrid.Init(X, Z);

            foreach (var item in hexGrid.HexCells)
            {
                item.TerrainTypeIndex = Terrain.grassland;
            }

            AddTerrain(hexGrid.HexCells, Terrain.coast, Lasks, x => x.TerrainTypeIndex == Terrain.grassland);
            AddTerrain(hexGrid.HexCells, Terrain.desert, Desert, x => x.TerrainTypeIndex == Terrain.grassland,CoreSize:10);
            AddTerrain(hexGrid.HexCells, Terrain.mountains, Mountains, x => x.TerrainTypeIndex == Terrain.grassland);
        }


        /// <summary>
        /// 添加地形
        /// </summary>
        /// <param name="map">所添加的地图</param>
        /// <param name="terrain">添加的地形类型</param>
        /// <param name="coefficient">地形系数 越大会生成越多地形 MAX:100 MIN:0</param>
        /// <param name="math">地形会从什么条件的单元格上生成的断言 True:可以生成  False:不会生成 </param>
        /// <param name="CoreSize">单个增殖块的标准大小</param>
        /// <param name="wave">单个核芯增殖数量波动值</param>
        /// <param name="maxSize">单个增殖块的大小的最大值</param>
        /// <param name="minSize">单个增殖块的大小的最大值</param>
        private void AddTerrain(HexCell[,] map,
                                Terrain terrain,
                                int coefficient,
                                Predicate<HexCell> math = null,
                                int CoreSize = 6,
                                int wave = 0,
                                int maxSize = 0,
                                int minSize = 0)
        {


            int addSzie = (int)(coefficient * 0.01 * MapSize);//根据系数计算添加的总数
            int CoreChunkCount = addSzie / CoreSize;    //增殖数量

            Debug.LogFormat("{0}:CoreChunkCount = {1}", terrain.ToString(), CoreChunkCount);

            if (math == null)
            {
                math = x => true;
            }

            for (int i = 0; i < CoreChunkCount; i++)
            {
                HexCell hexCell = GetRandomCell(map,math);
                if (hexCell == null) continue;
                hexCell.TerrainTypeIndex = terrain;

                int size = random.Next(CoreSize - wave, CoreSize + wave);

                //如果有设置大小限制
                if (maxSize + minSize > 0)
                {
                    if (maxSize > 0 && minSize > 0)
                    {
                        Extension.Limit(size, minSize, maxSize);
                    }
                    else if (maxSize > 0)
                    {
                        if (size > maxSize)
                        {
                            size = maxSize;
                        }
                    }
                    else if (minSize > 0)
                    {
                        if (size < minSize)
                        {
                            size = minSize;
                        }
                    }
                }
                
                ProliferationHexCell(hexCell,size,
                    math
                    );
            }
        }

        /// <summary>
        /// 在地图上随机选择单元格
        /// </summary>
        /// <param name="map">地图</param>
        /// <returns>选中的单元格</returns>
        private HexCell GetRandomCell(HexCell[,] map)
        {
            HexCell hexCell = map[random.Next(0,X),random.Next(0,Z)];
            return hexCell;
        }

        /// <summary>
        /// 在地图上随机选择单元格 如果无返回的单元格返回null
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="match">要选择的单元格的断言</param>
        /// <returns>选中的单元格 如果无返回的单元格返回null</returns>
        private HexCell GetRandomCell(HexCell[,] map, Predicate<HexCell> match)
        {
            HexCell hexCell;
            byte i = 0; //防死循环变量
            do
            {
                hexCell = map[random.Next(0, X), random.Next(0, Z)];
                i++;
            } while (match(hexCell) == false && i < 20);

            if (i >= 20)
            {
                hexCell = null;
            }
            return hexCell;
        }

        /// <summary>
        /// 增殖单元格
        /// </summary>
        /// <param name="cell">需要增殖的单元格</param>
        /// <param name="Size">单元格增值的数量</param>
        /// <param name="math">增殖单元格选择范围断言</param>
        /// <returns>增殖过项的列表</returns>
        private List<HexCell> ProliferationHexCell(HexCell cell,int Size,Predicate<HexCell> math)
        {
            //增殖过的列表
            List<HexCell> ProliferationList = new List<HexCell>();
            //增殖备选列表
            List<HexCell> AddCell = new List<HexCell>
            {
                cell
            };

            if (Size > MapSize)
            {
                Size = MapSize;
            }
            //增殖的单元格的地图类型 即核心单元格的地图类型 
            Terrain thisTerrain = cell.TerrainTypeIndex;
            int i = 0;
            while (i < Size && AddCell.Count > 0)
            {
                HexCell selectedCell = AddCell.GetRandomItem(random);
                //相邻可更改的单元格的备选列表
                List<HexCell> alternative = new List<HexCell>();

                //向备选列表添加所有的可以返回条件的相邻单元格
                foreach (HexCell item in selectedCell.neighbors)
                {
                    if (item != null && item.TerrainTypeIndex != thisTerrain && math(item))
                    {
                        alternative.Add(item);
                    }
                }

                //如果没有可改变的单元格则将这个单元格从增殖单元格备选列表中移除
                //然后重新选择一个备选列表中的单元格
                if (alternative.Count == 0)
                {
                    AddCell.Remove(selectedCell);
                    continue;
                }

                //随机从邻近备选列表中选一个单元格改变地貌，并加它添加到备选列表中
                HexCell neighbor = alternative.GetRandomItem(random);
                neighbor.TerrainTypeIndex = thisTerrain;
                ProliferationList.Add(neighbor);
                AddCell.Add(neighbor);
                i++;
            }
            return ProliferationList;
        }

        #endregion

        public void Show()
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
        }

        public void Exit()
        {
            Destroy(gameObject);
        }
    }
}
