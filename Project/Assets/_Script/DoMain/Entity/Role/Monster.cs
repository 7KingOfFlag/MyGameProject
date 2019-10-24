using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurGameName.DoMain.Entity.RoleSpace
{
    public class Picture
    {
        public Picture() {
            ID = Count++;
        }

        [NonSerialized]
        private static int Count = 0;

        public int ID { get; private set; }

        /// <summary>
        /// 图像名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 同名图像数量
        /// </summary>
        public int MonsterType { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public string ImageType { get; set; }

    }
}
