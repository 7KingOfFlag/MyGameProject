using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace OurGameName.DoMain.Entity.Log
{
    /// <summary>
    /// 游戏日志
    /// </summary>
    internal class GameLog : MonoBehaviour
    {
        private Dictionary<string,SortedList<DateTime,string>> LogList;
        public TextMeshProUGUI txtLog;

        private StringBuilder LogStringBuilder;

        void Awake()
        {
            LogList = new Dictionary<string, SortedList<DateTime, string>>();
            LogStringBuilder = new StringBuilder(2048);
        }

        public void AddLog(string content,string Tag)
        {
            if (txtLog == null)
            {
                Debug.LogError("GameLog Error txtLog is null");
                return;
            }

            if (LogList.ContainsKey(Tag) == false)
            {
                LogList.Add(Tag, new SortedList<DateTime, string>());
            }
            var timeData = DateTime.Now;
            LogList[Tag].Add(timeData, content);

            LogStringBuilder.Append($"{Tag} {timeData}:{content}");
            txtLog.SetText(LogStringBuilder.ToString());
        }
    }
}
