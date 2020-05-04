namespace OurGameName.DoMain.Attribute
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class VectorExtension
    {
        private static Vector2Int m_errorV2 = new Vector2Int(-1, -1);
        private static Vector3Int m_errorV3 = new Vector3Int(-1, -1, -1);

        /// <summary>
        /// 返回一个代表错误的三元整型向量
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector3Int Error(this Vector3Int vector3)
        {
            return m_errorV3;
        }

        /// <summary>
        /// 返回一个代表错误的二元整型向量
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector2Int Error(this Vector2Int vector2)
        {
            return m_errorV2;
        }

        /// <summary>
        /// 将三维向量转为二维向量,舍弃z
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2Int ToVector2Int(this Vector3Int vector)
        {
            return new Vector2Int(vector.x, vector.y);
        }

        public static Vector2Int[] ToVector2Int(this IEnumerable<Vector3Int> vectors)
        {
            return vectors.Select(item => item.ToVector2Int()).ToArray();
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector)
        {
            return new Vector3Int(vector.x, vector.y, 0);
        }

        public static Vector3Int[] ToVector3Int(this IEnumerable<Vector2Int> vectors)
        {
            return vectors.Select(item => item.ToVector3Int()).ToArray();
        }
    }
}