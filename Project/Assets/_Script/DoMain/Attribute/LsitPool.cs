using System.Collections.Generic;

namespace OurGameName.DoMain.Attribute
{
    /// <summary>
    /// 列表栈 先进先出(感觉不用备注 >_< )
    /// 因为是泛型所以不同的类型会有不同的栈
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public static class ListPool<T>
    {
        private static Stack<List<T>> stack = new Stack<List<T>>();

        /// <summary>
        /// Get 出栈
        /// </summary>
        /// <returns></returns>
        public static List<T> Get()
        {
            if (stack.Count > 0)
            {
                return stack.Pop();
            }
            return new List<T>();
        }

        /// <summary>
        /// Add 入栈
        /// </summary>
        /// <param name="list"></param>
        public static void Add(List<T> list)
        {
            list.Clear();
            stack.Push(list);
        }
    }
}