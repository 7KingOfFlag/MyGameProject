namespace OurGameName.DoMain.Map.Service
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 寻路服务的接口
    /// </summary>
    internal interface INavigateService
    {
        /// <summary>
        /// 获取最短路径
        /// </summary>
        /// <param name="map">游戏地图</param>
        /// <param name="start">起始点</param>
        /// <param name="end">终点</param>
        /// <param name="passabilityArgs">角色通过性参数</param>
        /// <returns>路径数组</returns>
        List<Vector2Int> GetPath(GameMap map, Vector2Int start, Vector2Int end, RolePassabilityArgs passabilityArgs);
    }
}