namespace OurGameName.DoMain.Entity.HexMap
{
    using OurGameName.DoMain.Attribute;
    using UnityEngine;

    /// <summary>
    /// 六边形常量
    /// 定义六边形地图的基本量
    /// </summary>
    public static class HexMetrics
    {
        /// <summary>
        /// 噪声扰动倍率
        /// </summary>
        public const float cellPreturbStrength = 5f;

        /// <summary>
        /// 六边形的内径
        /// </summary>
        /// 0.8660254038f = 二分根号3 是六边形内外径的比值
        public const float innerRadius = outerRadius * 0.8660254038f;

        /// <summary>
        /// 六边形的外径与边长
        /// </summary>
        public const float outerRadius = 10f;

        /// <summary>
        /// 六边形顶点常量数组
        /// </summary>
        public static Vector3[] hexagon =
        {
            new Vector3(0f, 0f, outerRadius),
            new Vector3(innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(0f, 0f, -outerRadius),
            new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
            new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
            new Vector3(0f, 0f, outerRadius),
        };

        /// <summary>
        /// 噪声取样
        /// </summary>
        public static Texture2D noiseSource;

        /// <summary>
        /// 地图颜色数组
        /// </summary>
        public static Texture2D[] texture;

        #region 城墙设置

        /// <summary>
        /// 城墙高度
        /// </summary>
        public const float wallHeight = 3f;

        /// <summary>
        /// 城墙厚度
        /// </summary>
        public const float wallThickness = 0.75f;

        /// <summary>
        /// 城墙厚度偏移量
        /// </summary>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        public static Vector3 WallThicknessOffset(Vector3 near, Vector3 far)
        {
            Vector3 offset;
            offset.x = far.x - near.x;
            offset.y = 0;
            offset.z = far.z - near.z;
            return offset.normalized * (wallThickness * 0.5f);
        }

        #endregion 城墙设置

        #region 随机数哈希表

        /// <summary>
        /// 随机数选择缩放值
        /// </summary>
        public const float hashGridScale = 0.25f;

        /// <summary>
        /// 随机哈希表大小
        /// </summary>
        public const int hashGridSize = 256;

        /// <summary>
        /// 随机哈希表
        /// </summary>
        private static Float2[] hashGrid;

        public static void InitHashGrid(int seed)
        {
            hashGrid = new Float2[hashGridSize * hashGridSize];
            Random.State currentState = Random.state;
            Random.InitState(seed);
            for (int i = 0; i < hashGrid.Length; i++)
            {
                hashGrid[i] = Float2.Create();
            }
            Random.state = currentState;
        }

        public static Float2 SampleHashGrid(Vector3 position)
        {
            int x = (int)(position.x * hashGridScale) % hashGridSize;
            int z = (int)(position.z * hashGridScale) % hashGridSize;

            x = x < 0 ? x + hashGridSize : x;
            z = z < 0 ? z + hashGridSize : z;
            return hashGrid[x + z * hashGridSize];
        }

        #endregion 随机数哈希表
    }
}