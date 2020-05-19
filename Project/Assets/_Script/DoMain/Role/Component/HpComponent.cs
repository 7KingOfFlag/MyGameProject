namespace OurGameName.DoMain.RoleSpace.Component
{
    using UnityEngine;
    using Slider = UnityEngine.UI.Slider;

    public class HpComponent : MonoBehaviour
    {
        /// <summary>
        /// 透明的控制用画布组
        /// </summary>
        public CanvasGroup AhplaController;

        /// <summary>
        /// HP滚动条
        ///
        /// </summary>
        public Slider HpSlider;

        /// <summary>
        /// 生命值
        /// </summary>
        public int HP
        {
            get => (int)this.HpSlider.value;
            set => this.HpSlider.value = value;
        }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int MaxHp
        {
            get => (int)this.HpSlider.maxValue;
            set => this.HpSlider.maxValue = value;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visibility
        {
            get => this.AhplaController.alpha == 1f;
            set => this.AhplaController.alpha = value ? 1 : 0f;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxHp">最大生命值</param>
        public void Init(int maxHp)
        {
            this.HpSlider.maxValue = maxHp;
            this.HpSlider.minValue = 0;
            this.HpSlider.value = maxHp;
        }
    }
}