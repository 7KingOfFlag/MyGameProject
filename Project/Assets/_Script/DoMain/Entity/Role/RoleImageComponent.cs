namespace OurGameName.DoMain.Entity.RoleSpace
{
    using OurGameName.DoMain.Attribute;
    using UnityEngine;

    /// <summary>
    /// 角色图像
    /// </summary>
    internal class RoleImageComponent : MonoBehaviour
    {
        private Material spriteMaterial;

        public bool IsBlink
        {
            get
            {
                return spriteMaterial.GetFloat("_IsBlink") > 0;
            }
            set
            {
                if (value == true)
                {
                    spriteMaterial.SetFloat("_IsBlink", SharedMetrics.SharedTrue);
                }
                else
                {
                    spriteMaterial.SetFloat("_IsBlink", SharedMetrics.SharedFalse);
                }
            }
        }

        public bool Outline
        {
            set
            {
                if (value == true)
                {
                    spriteMaterial.SetFloat("_Outline", SharedMetrics.SharedTrue);
                }
                else
                {
                    spriteMaterial.SetFloat("_Outline", SharedMetrics.SharedTrue);
                }
            }
        }

        public Color OutlineColor
        {
            get
            {
                return spriteMaterial.GetColor("_OutlineColor");
            }
            set
            {
                spriteMaterial.SetColor("_OutlineColor", value);
            }
        }

        private void Awake()
        {
            spriteMaterial = GetComponent<SpriteRenderer>().sharedMaterial;
        }
    }
}