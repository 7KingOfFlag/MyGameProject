using System;
using System.Collections.Generic;
using Random = System.Random;

namespace OurGameName.DoMain.Attribute
{
    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class Extension
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


        
    }

}
