using System;
using OurGameName.DoMain;
using UnityEngine;
using UnityEngine.UI;


namespace OurGameName.StarView
{
    public class UIMain : MonoBehaviour
    {
        /// <summary>
        /// 开始游戏
        /// </summary>
        public Button btnStart;
        /// <summary>
        /// 游戏设置
        /// </summary> 
        public Button btnOption;
        /// <summary>
        /// 退出游戏
        /// </summary>
        public Button btnExit;
        /// <summary>
        /// 载入游戏
        /// </summary>
        public Button btnLoadGame;
        /// <summary>
        /// 保存界面
        /// </summary>
        public Canvas SaveCanvas;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            EventTriggerListener.Get(btnStart.gameObject).onClick = OnButtonClick;
            EventTriggerListener.Get(btnOption.gameObject).onClick = OnButtonClick;
            EventTriggerListener.Get(btnExit.gameObject).onClick = OnButtonClick;
            EventTriggerListener.Get(btnLoadGame.gameObject).onClick = OnButtonClick;

            SaveCanvas.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SaveCanvas.enabled)
                {
                    SaveCanvas.enabled = false;
                }
            }
        }

        private void OnButtonClick(GameObject g)
        {
            if (g == btnStart.gameObject)
            {
                BtnStartClick();
            }

            if (g == btnOption.gameObject)
            {
                Debug.Log("设置");
            }

            if (g == btnExit.gameObject)
            {
                btnExitClick();
            }

            if (g == btnLoadGame.gameObject)
            {
                btnLoadGameClick();
            }
        }

        public void BtnStartClick()
        {
            SaveCanvas.enabled = true;
        }

        public void btnOptionClick()
        {
            Debug.Log("设置");
        }

        public void btnExitClick()
        {
            Debug.Log("退出");
            Application.Quit();
        }

        public void btnLoadGameClick()
        {
            Debug.Log("载入游戏");
        }
    }
}
