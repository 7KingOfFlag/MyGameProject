using OurGameName.DoMain.Attribute;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OurGameName.View
{
    public class DialogBox : MonoBehaviour
    {
        /// <summary>
        /// 对话框按钮的返回结果
        /// </summary>
        internal event EventHandler<DialogBoxReturnArgs> Result;

        public Button btnYes;
        public Button btnNo;
        public CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup.alpha = 0;
            btnYes.onClick.AddListener(OnYes);
            btnNo.onClick.AddListener(OnNo);
        }

        private void OnYes()
        {
            OnResult(new DialogBoxReturnArgs(DialogBoxReturnArgs.DialogBoxReturnArgsCode.Yes));
            Hide();
        }

        private void OnNo()
        {
            OnResult(new DialogBoxReturnArgs(DialogBoxReturnArgs.DialogBoxReturnArgsCode.No));
            Hide();
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
        }

        /// <summary>
        /// 关闭对话框
        /// </summary>
        public void Hide()
        {
            canvasGroup.alpha = 0f;
        }

        internal virtual void OnResult(DialogBoxReturnArgs e)
        {
            e.Raise(this, ref Result);
        }
    }

    /// <summary>
    /// 对话框事件返回参数
    /// </summary>
    internal class DialogBoxReturnArgs : EventArgs
    {
        /// <summary>
        /// 对话框选项返回结果
        /// </summary>
        public DialogBoxReturnArgsCode result;

        /// <summary>
        /// 对话框事件返回参数
        /// </summary>
        /// <param name="result">对话框选项返回结果</param>
        public DialogBoxReturnArgs(DialogBoxReturnArgsCode result)
        {
            this.result = result;
        }

        internal enum DialogBoxReturnArgsCode
        {
            Yes,
            No
        }
    }
}