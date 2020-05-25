namespace OurGameName.DoMain.Role.Component
{
    using TMPro;
    using UniRx;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class SkillButtonCompnent : MonoBehaviour
    {
        /// <summary>
        /// 按钮
        /// </summary>
        public Button Button;

        /// <summary>
        /// 技能文本框
        /// </summary>
        public TextMeshProUGUI Text;

        /// <summary>
        /// 技能名
        /// </summary>
        private ReactiveProperty<string> SkillName { get; } = new ReactiveProperty<string>("");

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="skillName">技能名</param>
        /// <param name="cell">点击回调事件</param>
        public void Init(string skillName, UnityAction cell)
        {
            this.SkillName.Value = skillName;

            this.Button.onClick.RemoveAllListeners();
            this.Button.onClick.AddListener(cell);
        }

        #region Unity

        private void Awake()
        {
            this.SkillName.Subscribe(x => this.Text.text = x);
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        #endregion Unity
    }
}