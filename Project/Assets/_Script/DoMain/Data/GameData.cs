using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurGameName.DoMain.Entity.RoleSpace;
using OurGameName.DoMain.Attribute;
using System.Web.Script.Serialization;
using UnityEngine;

namespace OurGameName.DoMain.Data
{
    /// <summary>
    /// 游戏数据中心
    /// </summary>
    internal class GameData : MonoBehaviour
    {
        public event EventHandler<AseetLoadStatusArgs> AseetLoadStatusChang;

        public TileAssetDate TileAssetDate;

        /// <summary>
        /// 当资源载入完成时向数据中心报告并分发该信息
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnAseetLoadStatusChang(AseetLoadStatusArgs e)
        {
            e.Raise(this, ref AseetLoadStatusChang);
        }
    }
}
