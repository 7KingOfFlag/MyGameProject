using System;
using System.Threading;

namespace OurGameName.DoMain.Attribute
{
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
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
            if (temp != null) temp(sender, e);
        }

    }
}
