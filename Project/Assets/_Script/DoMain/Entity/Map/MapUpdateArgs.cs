namespace OurGameName.DoMain.Entity.Map
{
    using System.Collections.Generic;

    /// <summary>
    /// 地图更新参数
    /// </summary>
    internal class MapUpdateArgs
    {
        /// <summary>
        /// 更新的地图单元
        /// </summary>
        public IReadOnlyList<Element> UpdateElement { get; set; }
    }
}