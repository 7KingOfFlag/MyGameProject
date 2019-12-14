using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace OurGameName.DoMain.Attribute
{

    /// <summary>
    /// 文件导入工具类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 载入文件夹中的图片
        /// 仅限PNG和JPG 原因Texture2D.LoadImage()拓展方法限制
        /// </summary>
        /// <param name="path">文件绝对地址</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns>Texture2D<returns>
        public static Texture2D LoadTexture2D(string path,int width, int height)
        {
            byte[] bytes = LoadFile(path);

            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);

            return texture;
        }

        /// <summary>
        /// 读取文件返回文件的字节码形式
        /// </summary>
        /// <param name="path">文件绝对地址</param>
        /// <returns></returns>
        public static byte[] LoadFile(string path)
        {
            FileStream fileStream;
            byte[] bytes;
            using (fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, (int)fileStream.Length);
            }
            return bytes;
        }
        /*
        /// <summary>
        /// 将GIF转换成精灵贴图
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static List<Texture2D> GifToTexture(System.Drawing.Image image)
        {
            List<Texture2D> texture2D = null;
            if (image != null)
            {
                texture2D = new List<Texture2D>();
                //根据指定的唯一标识创建一个提供获取图形框架维度信息的实例;
                var guid = image.FrameDimensionsList;
                Debug.Log(guid[0].ToString());
                FrameDimension frameDimension = new FrameDimension(image.FrameDimensionsList[0]);
                
                //获取指定维度的帧数
                int framCount = image.GetFrameCount(frameDimension);

                for (int i = 0; i < framCount; i++)
                {
                    image.SelectActiveFrame(frameDimension, i);
                    var framBitmap = new Bitmap(image.Width, image.Height);
                    System.Drawing.Graphics.FromImage(framBitmap).
                        DrawImage(image, Point.Empty);

                    var frameTexture2D = new Texture2D(framBitmap.Width, framBitmap.Height);

                    for (int x = 0; x < framBitmap.Width; x++)
                    {
                        for (int y = 0; y < framBitmap.Height; y++)
                        {
                            System.Drawing.Color surceColor = framBitmap.GetPixel(x, y);

                            frameTexture2D.SetPixel(x,
                                framBitmap.Height - 1 - y,
                                new Color32(surceColor.R,
                                    surceColor.G,
                                    surceColor.B,
                                    surceColor.A));
                        }
                        frameTexture2D.Apply();
                        texture2D.Add(frameTexture2D);
                    }
                }

            }
            return texture2D;
        }

        public static System.Drawing.Image LoadImage(string path)
        {
            System.Drawing.Image image = null;
            image = System.Drawing.Image.FromFile(path);
            return image;
        }
        */
        /// <summary>
        /// 已覆盖的形式保存字符串到文件
        /// </summary>
        /// <param name="path">文件路径 包括文件名</param>
        /// <param name="str">文件字符串</param>
        public static void SaveStrFile(string path,string str)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Write(str);
            }
        }

        /// <summary>
        /// 以纯文本形式读取文件并返回字符串
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static string ReadStrToFile(string path)
        {
            string str = null;
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    str = reader.ReadToEnd();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return str;
        }

    }
}
