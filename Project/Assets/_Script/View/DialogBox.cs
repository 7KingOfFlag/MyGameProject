using System;
using OurGameName.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace OurGameName.View
{
    public class DialogBox : MonoBehaviour
    {
        public Button btnNo;

        public Button btnYes;

        public CanvasGroup canvasGroup;

        /// <summary>
        /// 对话框按钮的返回结果
        /// </summary>
        internal event EventHandler<DialogBoxReturnArgs> Result;

        /// <summary>
        /// 关闭对话框
        /// </summary>
        public void Hide()
        {
            canvasGroup.alpha = 0f;
        }

        public void Show()
        {
            canvasGroup.alpha = 1f;
        }

        internal virtual void OnResult(DialogBoxReturnArgs e)
        {
            e.Raise(this, ref Result);
        }

        private void OnNo()
        {
            OnResult(new DialogBoxReturnArgs(DialogBoxReturnArgs.DialogBoxReturnArgsCode.No));
            Hide();
        }

        private void OnYes()
        {
            OnResult(new DialogBoxReturnArgs(DialogBoxReturnArgs.DialogBoxReturnArgsCode.Yes));
            Hide();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("代码质量", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
        private void Start()
        {
            canvasGroup.alpha = 0;
            btnYes.onClick.AddListener(OnYes);
            btnNo.onClick.AddListener(OnNo);
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