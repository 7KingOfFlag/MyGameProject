using System;

namespace OurGameName.DoMain.RoleSpace
{
    public class Picture
    {
        [NonSerialized]
        private static int Count = 0;

        public Picture()
        {
            ID = Count++;
        }

        public int ID { get; private set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public string ImageType { get; set; }

        /// <summary>
        /// 同名图像数量
        /// </summary>
        public int MonsterType { get; set; }

        /// <summary>
        /// 图像名
        /// </summary>
        public string Name { get; set; }
    }
}