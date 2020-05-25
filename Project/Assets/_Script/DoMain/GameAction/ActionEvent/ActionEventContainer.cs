namespace OurGameName.DoMain.GameAction.ActionEvent
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using OurGameName.DoMain.GameAction.Args;
    using UnityEngine.InputSystem.LowLevel;

    /// <summary>
    /// 游戏动作事件容器
    /// </summary>
    internal sealed class ActionEventContainer
    {
        /// <summary>
        /// 锁实例
        /// </summary>
        private static readonly object balanceLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static ActionEventContainer instance = null;

        private readonly ConcurrentDictionary<ActionEventID, IActionEvent> actionEventDict;

        /// 单例构造函数
        /// </summary>
        private ActionEventContainer()
        {
            IEqualityComparer<ActionEventID> comparer = new ActionEventID();
            this.actionEventDict = new ConcurrentDictionary<ActionEventID, IActionEvent>(comparer);
        }

        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static ActionEventContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (balanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new ActionEventContainer();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// 注册游戏动作
        /// </summary>
        /// <param name="">注册动作</param>
        public void Registered<T>()
            where T : class, IActionEvent, new()
        {
            var actionEvent = new T();
            if (this.actionEventDict.TryAdd(actionEvent.UID, actionEvent))
            {
                throw new ArgumentException($"插入的动作事件ID:{actionEvent.UID}已存在");
            }
        }

        public T Resolve<T>()
             where T : class, IActionEvent, new()
        {
            var result = new T();
            if (this.actionEventDict.ContainsKey(result.UID) == false)
            {
                throw new ArgumentException($"动作事件{typeof(T)}未注册");
            }

            return result;
        }

        /// <summary>
        /// 解析游戏动作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionEvent Resolve(ActionEventID id)
        {
            if (this.actionEventDict.TryGetValue(id, out var result) == false)
            {
                throw new ArgumentException($"动作事件ID{id}未注册");
            }

            return (IActionEvent)Activator.CreateInstance(result.GetType());
        }
    }
}