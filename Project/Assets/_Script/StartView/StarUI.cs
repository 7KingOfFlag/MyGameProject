using System;
using OurGameName.Config;
using OurGameName.DoMain;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace OurGameName.StarView
{
    public class StarUI : MonoBehaviour
    {
        /// <summary>
        /// 载入游戏
        /// </summary>
        public Button btnLoadGame;

        /// <summary>
        /// 开始新游戏
        /// </summary>
        public Button btnNewGame;

        /// <summary>
        /// 游戏设置
        /// </summary> 
        public Button btnOption;

        /// <summary>
        /// 退出游戏
        /// </summary>
        public Button btnExitGame;

        void Start()
        {
            BtnInit();
        }

        /// <summary>
        /// 按钮初始化
        /// </summary>
        private void BtnInit()
        {
            BtnLoadGameInit();
            btnLoadGame.onClick.AddListener(LoadGame);
            btnNewGame.onClick.AddListener(NewGame);
            btnOption.onClick.AddListener(Option);
            btnExitGame.onClick.AddListener(Exit);
        }

        /// <summary>
        /// 跳转到载入界面
        /// </summary>
        public void LoadGame()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 跳转到新建游戏界面
        /// </summary>
        public void NewGame()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 跳转到设置界面
        /// </summary>
        public void Option()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 弹出确认退出游戏界面
        /// </summary>
        public void Exit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化载入游戏按钮
        /// <para>不存在存档目录会新建存档目录</para>
        /// <para>不存在存档文件将不显示载入存档按钮</para>
        /// </summary>
        private void BtnLoadGameInit()
        {
            XmlConfigHelper xmlConfigHelper = new XmlConfigHelper();
            string GameSavePath = Environment.CurrentDirectory + xmlConfigHelper.GetConfig(ConfigType.PathConfig, "GameSavePath");
            if (Directory.Exists(GameSavePath) == false)
            {
                Directory.CreateDirectory(GameSavePath);
                btnLoadGame.gameObject.SetActive(false);
            }
            else
            {
                var gameSaveDirFiles = Directory.GetFileSystemEntries(GameSavePath, "*.json");
                if (gameSaveDirFiles.Length > 0)//存档目录是否存在文件
                {
                    btnLoadGame.gameObject.SetActive(true);
                }
                else
                {
                    btnLoadGame.gameObject.SetActive(false);
                }
            }
        }
    }
}
