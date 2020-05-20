namespace OurGameName.DoMain.GameAction.Action
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
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
            var action = new T();
            switch (action.ID.ActionType)
            {
                case ActionID.ActionTypeCode.Condit:
                    this.Registered((IConditAction)action);
                    break;

                case ActionID.ActionTypeCode.Execute:
                    this.Registered((IExecuteAction)action);
                    break;

                default:
                    throw new ArgumentException($"出现为处理的ActionType枚举类型{action.ID.ActionType}");
            }
        }

        /// <summary>
        /// 解析游戏动作条件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T resolve<T>(ActionID id)
            where T : class, IAction
        {
            T result = null;
            switch (id.ActionType)
            {
                case ActionID.ActionTypeCode.Condit:
                    if (this.conditActionDict.TryGetValue(id, out var conditAction) == false)
                    {
                        throw new ArgumentException($"动作ID{id}未注册");
                    }

                    result = conditAction as T;
                    break;

                case ActionID.ActionTypeCode.Execute:
                    if (this.executeActionDict.TryGetValue(id, out var executeAction) == false)
                    {
                        throw new ArgumentException($"动作ID{id}未注册");
                    }

                    result = executeAction as T;
                    break;

                default:
                    throw new ArgumentException($"出现为处理的ActionType枚举类型{id.ActionType}");
            }

            if (result == null) throw new ArgumentException($"泛型类型错误,类型{nameof(T)}与ID{id}不对应");

            return result;
        }

        /// <summary>
        /// 注册游戏动作
        /// </summary>
        /// <param name="">注册动作</param>
        private void Registered(IExecuteAction executeAction)
        {
            if (this.executeActionDict.TryAdd(executeAction.ID, executeAction) == false)
            {
                throw new ArgumentException($"插入的动作ID:{executeAction.ID}已存在");
            }
        }

        /// <summary>
        /// 注册游戏动作
        /// </summary>
        /// <param name="">注册动作</param>
        private void Registered(IConditAction conditAction)
        {
            if (this.conditActionDict.TryAdd(conditAction.ID, conditAction) == false)
            {
                throw new ArgumentException($"插入的动作ID:{conditAction.ID}已存在");
            }
        }
    }
}