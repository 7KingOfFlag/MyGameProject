namespace OurGameName.DoMain.UIComponent.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine.UI;

    internal static class ImageExtensions
    {
        /// <summary>
        /// 设置Alpha通道
        /// </summary>
        public static void SetAlpha(this Image image, float value)
        {
            var color = image.color;
            color.a = value;
            image.color = color;
        }

        /// <summary>
        /// 设置Blue通道
        /// </summary>
        public static void SetBlue(this Image image, float value)
        {
            var color = image.color;
            color.b = value;
            image.color = color;
        }

        /// <summary>
        /// 设置Greed通道
        /// </summary>
        public static void SetGreed(this Image image, float value)
        {
            var color = image.color;
            color.g = value;
            image.color = color;
        }

        /// <summary>
        /// 设置Red通道
        /// </summary>
        public static void SetRed(this Image image, float value)
        {
            var color = image.color;
            color.r = value;
            image.color = color;
        }
    }
}