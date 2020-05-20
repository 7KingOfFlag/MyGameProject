namespace OurGameName.View
{
    using System;
    using OurGameName.General.Extension;
    using UnityEngine;
    using UnityEngine.UI;

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
            this.canvasGroup.alpha = 0f;
        }

        public void Show()
        {
            this.canvasGroup.alpha = 1f;
        }

        internal virtual void OnResult(DialogBoxReturnArgs e)
        {
            e.Raise(this, ref Result);
        }

        private void OnNo()
        {
            this.OnResult(new DialogBoxReturnArgs(DialogBoxReturnArgs.DialogBoxReturnArgsCode.No));
            this.Hide();
        }

        private void OnYes()
        {
            this.OnResult(new DialogBoxReturnArgs(DialogBoxReturnArgs.DialogBoxReturnArgsCode.Yes));
            this.Hide();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("代码质量", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
        private void Start()
        {
            this.canvasGroup.alpha = 0;
            this.btnYes.onClick.AddListener(this.OnYes);
            this.btnNo.onClick.AddListener(this.OnNo);
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