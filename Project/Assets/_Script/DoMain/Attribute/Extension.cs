using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = System.Random;
using System.Linq;
using System.Text.RegularExpressions;

namespace OurGameName.DoMain.Attribute
{
    /// <summary>
    /// 扩展方法类
    /// </summary>
    internal static class Extension
    {
        private static Random selfRandom = new Random();

        /// <summary>
        /// 从列表中随机返回一个项
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="list">扩展方法的实例对象</param>
        /// <param name="random">随机数生成器</param>
        /// <returns>列表中的随机项</returns>
        public static T GetRandomItem<T>(this List<T> list,Random random = null)
        {
            if (random == null)
            {
                random = selfRandom;
            }

            T result;

            if (list.Count == 0)
            {
                IndexOutOfRangeException e = new IndexOutOfRangeException("列表内容为空");
                throw e;
            }

            if (list.Count == 1)
            {
                result = list[0];
            }
            else
            {
                result = list[random.Next(0, list.Count - 1)];
            }
            return result;
        }

        /// <summary>
        /// 在集合中是否存在给定条件的元素
        /// </summary>
        /// <typeparam name="TSource">元素类型</typeparam>
        /// <param name="source">元素集合</param>
        /// <param name="predicate">元素要求委托</param>
        /// <returns></returns>
        public static bool IsItemInSource<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 如果 i 在min与max之间返回i,如果i越界则返回越界的数
        /// </summary>
        /// <param name="i"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Limit(int i, int min, int max)
        {
            if (i > max)
            {
                return max;
            }
            else if (i < min)
            {
                return min;
            }
            else
            {
                return i;
            }
        }
        /// <summary>
        /// 交换两个对象的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swop<T>(ref T a,ref T b)
        {
            T t = b;
            b = a;
            a = t;
        }
        /// <summary>
        /// 索引 使用二维整型向量索引二维数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T GetItem<T>(this T[,] array,Vector2Int index)
        {
            return array[index.x, index.y];
        }

        /// <summary>
        /// 设置项 使用二维整型向量索引二维数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static void SetItem<T>(this T[,] array, Vector2Int index, T value)
        {
            array[index.x, index.y] = value;
        }
        /// <summary>
        /// 移除字符串中的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveNumber(this string str)
        {
            return Regex.Replace(str, @"[0-9]", "");
        }

        /// <summary>
        /// 将二维数组中的每个元素投影到新二维数组。
        /// </summary>
        /// <typeparam name="TSource">source 的元素类型。</typeparam>
        /// <typeparam name="TResult">selector 返回的值的类型</typeparam>
        /// <param name="source">一个二维数组，要对该二维数组调用转换函数</param>
        /// <param name="selector">应用于每个元素的转换函数</param>
        /// <returns></returns>
        public static TResult[,] Select<TSource, TResult>(this TSource[,] source, Func<TSource, TResult> selector)
        {
            TResult[,] result = new TResult[source.GetLength(0), source.GetLength(1)];
            for (int x = 0; x < source.GetLength(0); x++)
            {
                for (int y = 0; y < source.GetLength(1); y++)
                {
                    result[x, y] = selector(source[x,y]);
                }
            }
            return result;
        }

        /// <summary>
        /// 使用指定值初始化一个二维数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="size">数组大小</param>
        /// <param name="initValue">初始化值</param>
        /// <returns></returns>
        public static T[,] Init<T>(Vector2Int size, T initValue)
        {
            T[,] result = new T[size.x, size.y];
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    result[x, y] = initValue;
                }
            }
            return result;
        }
    }
}
