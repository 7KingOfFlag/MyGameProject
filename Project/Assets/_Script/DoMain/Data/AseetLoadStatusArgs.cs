using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build.Pipeline;

namespace OurGameName.DoMain.Data
{
    internal class AseetLoadStatusArgs : EventArgs
    {
        public AseetLoadStatusArgs(Type aseetType, string aseetName, LoadStatus loadStatus)
        {
            AseetType = aseetType;
            AseetName = aseetName;
            AseetLoadStatus = loadStatus;
        }

        public Type AseetType { get; }
        public string AseetName { get; }
        public LoadStatus AseetLoadStatus { get; }

        public enum LoadStatus
        {
            /// <summary>
            /// 载入完成
            /// </summary>
            Completed,
            /// <summary>
            /// 卸载完成
            /// </summary>
            Destroyed,
            /// <summary>
            /// 无类型载入完成
            /// </summary>
            CompletedTypeless
        }
    }
}
