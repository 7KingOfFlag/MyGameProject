using OurGameName.Config;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DialogBoxReturn = OurGameName.View.DialogBoxReturnArgs.DialogBoxReturnArgsCode;

namespace OurGameName.View.StarView
{
    internal class StarUI : MonoBehaviour
    {
        /// <summary>
        /// 主UI
        /// </summary>
        public Canvas MainUI;

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

        /// <summary>
        /// 对话框游戏资源
        /// </summary>
        public AssetReference DialogBoxAsset;

        /// <summary>
        /// 对话框实例
        /// </summary>
        private DialogBox dialogBox;

        private void Start()
        {
            BtnInit();
            AssetLoad();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.SetActiveScene(scene);
        }

        private void AssetLoad()
        {
            BtnExitAssetLoad();
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
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 跳转到新建游戏界面
        /// </summary>
        public void NewGame()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 跳转到设置界面
        /// </summary>
        public void Option()
        {
            SceneManager.LoadSceneAsync("OptionView", LoadSceneMode.Additive);
        }

        /// <summary>
        /// 弹出确认退出游戏界面
        /// </summary>
        public void Exit()
        {
            dialogBox.Result += (seed, e) => { if (e.result == DialogBoxReturn.Yes) Application.Quit(); };
            dialogBox.Show();
        }

        /// <summary>
        /// 初始化载入游戏按钮
        /// <para>不存在存档目录会新建存档目录</para>
        /// <para>不存在存档文件将不显示载入存档按钮</para>
        /// </summary>
        private void BtnLoadGameInit()
        {
            GameConfig gameConfig = GameConfig.Instance;
            string gameSavePath = gameConfig.PathConfig.ArchivePath;
            if (Directory.Exists(gameSavePath) == false)
            {
                Directory.CreateDirectory(gameSavePath);
                btnLoadGame.gameObject.SetActive(false);
            }
            else
            {
                var gameSaveDirFiles = Directory.GetFileSystemEntries(gameSavePath, "*.json");
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

        /// <summary>
        /// 退出按钮资源载入
        /// </summary>
        private void BtnExitAssetLoad()
        {
            btnExitGame.enabled = false;
            DialogBoxAsset.InstantiateAsync(MainUI.transform, false)
                .Completed += x =>
                {
                    Debug.Log("LoadCompleted");
                    dialogBox = x.Result.GetComponent<DialogBox>();
                    x.Result.transform.localPosition = Vector3.zero;
                    btnExitGame.enabled = true;
                };
        }
    }
}