namespace OurGameName.DoMain.RoleSpace
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

        private void Awake()
        {
            this.spriteMaterial = this.GetComponent<SpriteRenderer>().sharedMaterial;
        }
    }
}