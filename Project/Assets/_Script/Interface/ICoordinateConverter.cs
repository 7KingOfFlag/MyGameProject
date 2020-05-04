namespace OurGameName.Interface
{
    using UnityEngine;

    /// <summary>
    /// 坐标转换器接口
    /// </summary>
    internal interface ICoordinateConverter
    {
        /// <summary>
        /// 将地图坐标转化为世界坐标
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        Vector3 CellToWorld(Vector3Int cellPosition);
    }
}