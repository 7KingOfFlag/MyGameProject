namespace OurGameName.DoMain.Map.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.Map.Extensions;
    using UnityEngine;
    using Terrain = Args.Terrain;

    /// <summary>
    /// 寻路服务
    /// </summary>
    internal class NavigateService : INavigateService
    {
        /// <summary>
        /// 获取最短路径
        /// </summary>
        /// <param name="map">游戏地图</param>
        /// <param name="start">起始点</param>
        /// <param name="end">终点</param>
        /// <param name="passabilityArgs">角色通过性参数</param>
        /// <returns>路径数组</returns>
        public List<Vector2Int> GetPath(GameMap map, Vector2Int start, Vector2Int end, RolePassabilityArgs passabilityArgs)
        {
            IComparer<NavigatePointArgs> NavigatePointArgsComparer = new NavigatePointArgs();
            var openSet = new NavigatePointList();
            var closeSet = new HashSet<Vector2Int>();
            var result = new List<Vector2Int>();
            var navigateCount = 0; // 寻路使用次数计数器
            var navigateStartTime = DateTime.Now; // // 寻路用时计时器
            openSet.Add(new NavigatePointArgs()
            {
                ActualCost = 0,
                ExpectCoset = 0,
                Position = start,
                Parent = null,
            });

            while (openSet.IsEmpty())
            {
                navigateCount++;
                var point = openSet.Min;
                openSet.Remove(point);
                if (point.Position != end)
                {
                    closeSet.Add(point.Position);
                    List<NavigatePointArgs> neighbors = map[point.Position.x, point.Position.y]
                        .neighbors
                        .Where(x => x != null && closeSet.Contains(x.Value) == false)
                        .Select(x =>
                       {
                           var args = this.GetNavigatePointArgs(map, point.ActualCost, x.Value, end, passabilityArgs);
                           args.Parent = point;
                           return args;
                       }).ToList();

                    openSet.AddRang(neighbors);
                }
                else
                {
                    result = this.GetPath(point);
                    break;
                }
            }

            var navigateTime = DateTime.Now - navigateStartTime;
            Debug.Log($"寻路完成!\n路径{string.Join("=>", result)}\n" +
                $"共计遍历{navigateCount}次,总耗时{navigateTime.TotalMilliseconds}ms");
            return result;
        }

        /// <summary>
        /// 通过性测试
        /// </summary>
        /// <param name="args"></param>
        /// <param name="neighborTerrain"></param>
        /// <returns></returns>
        private static bool PassabilityTest(RolePassabilityArgs args, Terrain neighborTerrain)
        {
            return (args.CanPassMountain == false && neighborTerrain == Terrain.Mountain)
                || (args.CanPassWater == false && neighborTerrain == Terrain.Ocean);
        }

        /// <summary>
        /// 获取预期通过费用
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int GetExpectCoset(Vector2Int a, Vector2Int b)
        {
            //return (int)Mathf.Sqrt(Mathf.Pow(b.x - a.x, 2f) + Mathf.Pow(b.y - a.y, 2f));
            return Mathf.Abs((a.x - b.x) + (a.y - b.y));
        }

        /// <summary>
        /// 获取起始点至终点的导航点参数
        /// </summary>
        /// <param name="map">游戏地图</param>
        /// <param name="startCost">其实费用</param>
        /// <param name="start">起始点</param>
        /// <param name="end">终点</param>
        /// <param name="args">角色通过性参数</param>
        /// <returns></returns>
        private NavigatePointArgs GetNavigatePointArgs(GameMap map, int startCost, Vector2Int start, Vector2Int end, RolePassabilityArgs args)
        {
            Terrain neighborTerrain = map[start.x, start.y].Terrain;
            int cost = PassabilityTest(args, neighborTerrain) == true ? int.MaxValue : neighborTerrain.GetCost();

            return new NavigatePointArgs()
            {
                ActualCost = cost + startCost,
                ExpectCoset = this.GetExpectCoset(start, end),
                Position = start,
            };
        }

        /// <summary>
        /// 获取最终路径
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private List<Vector2Int> GetPath(NavigatePointArgs point)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            var next = point;
            while (next.Parent != null)
            {
                result.Add(next.Position);
                next = next.Parent;
            }

            //if (result.Count > 0)
            //{
            //    result.RemoveAt(result.Count - 1);
            //}

            return result;
        }

        /// <summary>
        /// 导航点参数
        /// </summary>
        private class NavigatePointArgs : IComparer<NavigatePointArgs>
        {
            /// <summary>
            /// 实际通过费用
            /// </summary>
            public int ActualCost { get; set; }

            /// <summary>
            /// 综合通过费用
            /// </summary>
            public int Cost => this.ActualCost + this.ExpectCoset;

            /// <summary>
            /// 预期通过费用
            /// </summary>
            public int ExpectCoset { get; set; }

            /// <summary>
            /// 父节点
            /// </summary>
            public NavigatePointArgs Parent { get; set; }

            /// <summary>
            /// 导航点位置
            /// </summary>
            public Vector2Int Position { get; set; }

            /// <summary>
            /// 比较器
            /// </summary>
            public int Compare(NavigatePointArgs x, NavigatePointArgs y)
            {
                return x.Cost - y.Cost;
            }
        }

        /// <summary>
        /// 导航点参列表
        /// </summary>
        private class NavigatePointList
        {
            private SortedList<int, List<NavigatePointArgs>> navigatePointList;

            public NavigatePointList()
            {
                this.navigatePointList = new SortedList<int, List<NavigatePointArgs>>();
            }

            /// <summary>
            /// 获取最小值
            /// </summary>
            public NavigatePointArgs Min
            {
                get
                {
                    var key = this.navigatePointList.Keys.Min();
                    var list = this.navigatePointList[key];
                    var result = list.First();
                    return result;
                }
            }

            public void Remove(NavigatePointArgs args)
            {
                var list = this.navigatePointList[args.Cost];
                list.Remove(args);
                if (list.Any() == false)
                {
                    this.navigatePointList.Remove(args.Cost);
                }
            }

            /// <summary>
            /// 添加
            /// </summary>
            /// <param name="args"></param>
            internal void Add(NavigatePointArgs args)
            {
                if (this.navigatePointList.ContainsKey(args.Cost) == false)
                {
                    this.navigatePointList.Add(args.Cost, new List<NavigatePointArgs>());
                }
                this.navigatePointList[args.Cost].Add(args);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="args"></param>
            internal void AddRang(IEnumerable<NavigatePointArgs> args)
            {
                foreach (var item in args)
                {
                    this.Add(item);
                }
            }

            /// <summary>
            /// 是否为空
            /// </summary>
            /// <returns></returns>
            internal bool IsEmpty()
            {
                return this.navigatePointList.Keys.Any();
            }
        }
    }
}