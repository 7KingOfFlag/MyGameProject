namespace OurGameName.Extension
{
    using System;
    using System.Threading;

    /// <summary>
    /// 事件扩展方法
    /// </summary>
    internal static class EventArgExtensions
    {
        /// <summary>
        /// 线程安全的事件登记方法
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        /// <param name="eventDelegate"></param>
        public static void Raise<TEventArgs>(this TEventArgs e,
            object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            Volatile.Read(ref eventDelegate)?.Invoke(sender, e);
        }
    }
}