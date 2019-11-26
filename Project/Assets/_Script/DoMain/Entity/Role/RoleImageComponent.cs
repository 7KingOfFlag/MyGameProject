using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using OurGameName.DoMain.Attribute;

namespace OurGameName.DoMain.Entity.RoleSpace
{
    /// <summary>
    /// 角色图像
    /// </summary>
    internal class RoleImageComponent : MonoBehaviour
    {
        private Material spriteMaterial;

        void Awake()
        {
            spriteMaterial = GetComponent<SpriteRenderer>().sharedMaterial;
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
    }
}
