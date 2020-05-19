namespace OurGameName.DoMain.GameAction.Action
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using OurGameName.DoMain.GameAction.Args;

    /// <summary>
    /// 游戏动作容器
    /// </summary>
    internal class ActionContainer
    {
        /// <summary>
        /// 锁实例
        /// </summary>
        private static readonly object balanceLock = new object();

        /// <summary>
        /// 单例
        /// </summary>
        private static ActionContainer instance = null;

        /// <summary>
        /// 游戏动作字典
        /// </summary>
        /// <remarks>线程安全</remarks>
        private readonly ConcurrentDictionary<ActionID, IConditAction> conditActionDict;

        /// <summary>
        /// 游戏动作字典
        /// </summary>
        /// <remarks>线程安全</remarks>
        private readonly ConcurrentDictionary<ActionID, IExecuteAction> executeActionDict;

        /// 单例构造函数
        /// </summary>
        private ActionContainer()
        {
            IEqualityComparer<ActionID> comparer = new ActionID();
            this.conditActionDict = new ConcurrentDictionary<ActionID, IConditAction>(comparer);
            this.executeActionDict = new ConcurrentDictionary<ActionID, IExecuteAction>(comparer);
        }

        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static ActionContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (balanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new ActionContainer();
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
            where T : IAction, new()
        {
            var interfaces = typeof(T).GetInterfaces();

            if (interfaces.Contains(typeof(IConditAction)) == true)
            {
                var action = (IConditAction)new T();
                this.Registered(action);
                return;
            }

            if (interfaces.Contains(typeof(IExecuteAction)) == true)
            {
                var action = (IExecuteAction)new T();
                this.Registered(action);
                return;
            }
        }

        /// <summary>
        /// 注册游戏动作
        /// </summary>
        /// <param name="">注册动作</param>
        public void Registered(IConditAction conditAction)
        {
            if (this.conditActionDict.TryAdd(conditAction.ID, conditAction) == false)
            {
                throw new ArgumentException($"插入的动作ID:{conditAction.ID}已存在");
            }
        }

        /// <summary>
        /// 注册游戏动作
        /// </summary>
        /// <param name="">注册动作</param>
        public void Registered(IExecuteAction executeAction)
        {
            if (this.executeActionDict.TryAdd(executeAction.ID, executeAction) == false)
            {
                throw new ArgumentException($"插入的动作ID:{executeAction.ID}已存在");
            }
        }

        /// <summary>
        /// 解析游戏动作条件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IConditAction resolveConditAction(ActionID id)
        {
            if (id.ActionType != ActionID.ActionTypeCode.Condit)
            {
                throw new ArgumentException($"动作ID{id}类型错误,不是条件类动作ID");
            }

            if (this.conditActionDict.TryGetValue(id, out var result) == false)
            {
                throw new ArgumentException($"动作ID{id}未注册");
            }

            return result;
        }

        /// <summary>
        /// 解析游戏动作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IExecuteAction resolveIExecuteAction(ActionID id)
        {
            if (id.ActionType != ActionID.ActionTypeCode.Execute)
            {
                throw new ArgumentException($"动作ID{id}类型错误,不是执行类动作ID");
            }

            if (this.executeActionDict.TryGetValue(id, out var result) == false)
            {
                throw new ArgumentException($"动作ID{id}未注册");
            }

            return result;
        }
    }
}