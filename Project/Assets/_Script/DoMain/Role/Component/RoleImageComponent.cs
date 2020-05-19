namespace OurGameName.DoMain.RoleSpace.Component
{
    using OurGameName.DoMain.Attribute;
    using UnityEngine;

    /// <summary>
    /// 角色图像
    /// </summary>
    internal class RoleImageComponent : MonoBehaviour
    {
        /// <summary>
        /// 角色贴图
        /// </summary>
        public SpriteRenderer RoleSprite;

        /// <summary>
        /// 贴图材质
        /// </summary>
        private Material spriteMaterial;

        /// <summary>
        /// 是否闪烁
        /// </summary>
        public bool IsBlink
        {
            get
            {
                return this.spriteMaterial.GetFloat("_IsBlink") > 0;
            }
            set
            {
                if (value == true)
                {
                    this.spriteMaterial.SetFloat("_Blink", SharedMetrics.SharedTrue);
                }
                else
                {
                    this.spriteMaterial.SetFloat("_Blink", SharedMetrics.SharedFalse);
                }
            }
        }

        /// <summary>
        /// 是否显示外边框
        /// </summary>
        public bool Outline
        {
            set
            {
                if (value == true)
                {
                    this.spriteMaterial.SetFloat("_Outline", SharedMetrics.SharedTrue);
                }
                else
                {
                    this.spriteMaterial.SetFloat("_Outline", SharedMetrics.SharedFalse);
                }
            }
        }

        /// <summary>
        /// 外边框颜色
        /// </summary>
        public Color OutlineColor
        {
            get
            {
                return this.spriteMaterial.GetColor("_OutlineColor");
            }
            set
            {
                this.spriteMaterial.SetColor("_OutlineColor", value);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(Sprite roleImage)
        {
            this.RoleSprite.sprite = roleImage;
            this.IsBlink = true;
            this.Outline = false;
            this.OutlineColor = Color.white;
        }

        private void Awake()
        {
            this.RoleSprite = this.GetComponent<SpriteRenderer>();
            this.spriteMaterial = this.RoleSprite.material;
        }
    }
}