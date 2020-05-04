namespace OurGameName.DoMain.Attribute
{
    using System.IO;
    using UnityEngine;

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
        public static Texture2D LoadTexture2D(string path, int width, int height)
        {
            byte[] bytes = ReadFileToByteArray(path);

            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);

            return texture;
        }

        /// <summary>
        /// 读取文件返回文件的字节码形式
        /// </summary>
        /// <param name="path">文件绝对地址</param>
        /// <returns></returns>
        public static byte[] ReadFileToByteArray(string path)
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

        /// <summary>
        /// 以纯文本形式读取文件并返回字符串
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static string ReadFileToString(string path)
        {
            string result;
            if (File.Exists(path))
            {
                using (StreamReader reader = File.OpenText(path))
                {
                    result = reader.ReadToEnd();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return result;
        }

        /// <summary>
        /// 已覆盖的形式保存字符串到文件
        /// </summary>
        /// <param name="path">文件路径 包括文件名</param>
        /// <param name="context">文件字符串</param>
        public static void SaveStringToFile(string path, string context)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Write(context);
            }
        }
    }
}