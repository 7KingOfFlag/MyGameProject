using UnityEngine;

namespace OurGameName.DoMain.Entity.Map
{
    /// <summary>
    /// 六边形坐标结构
    /// 用于转换坐标
    /// </summary>
    [System.Serializable]
    public struct HexCoordinates
    {
        [SerializeField]
        private int x, z;

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Z
        {
            get
            {
                return z;
            }
        }

        public int Y
        {
            get
            {
                return -X - Z;
            }
        }

        public HexCoordinates(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        /// <summary>
        /// 从标准坐标转换为六边形坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns>六边形坐标</returns>
        public static HexCoordinates FromOffSetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }

        /// <summary>
        /// 从六边形坐标换为标准坐标
        /// </summary>
        /// <param name="coordinates">六边形坐标</param>
        /// <returns></returns>
        public Point ToListIndex()
        {
            return new Point(X + (Z / 2), Z);
        }

        /// <summary>
        /// 从位置转换为六边形坐标
        /// </summary>
        /// <param name="position">地图上的物理位置</param>
        /// <returns>六边形坐标</returns>
        public static HexCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.innerRadius * 2f);
            float y = -x;
            float offset = position.z / (HexMetrics.outerRadius * 3f);
            x -= offset;
            y -= offset;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x - y);

            if (iX + iY + iZ != 0)
            {
                float fx = Mathf.Abs(x - iX);
                float fy = Mathf.Abs(y - iY);
                float fz = Mathf.Abs(-x - y - iZ);

                if (fx > fy && fx > fz)
                {
                    iX = -iY - iZ;
                }
                else if (fz > fy)
                {
                    iZ = -iX - iY;
                }
            }
            return new HexCoordinates(iX, iZ);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", X, Y, Z);
        }
    }

    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public static readonly Point Empty = new Point(int.MinValue, int.MinValue);

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            bool result;
            if (obj is Point point)
            {
                result = point.X == X && point.Y == Y ? true : false;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}