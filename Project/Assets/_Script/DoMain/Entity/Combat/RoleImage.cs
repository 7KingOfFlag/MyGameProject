using OurGameName.DoMain.Entity.RoleSpace;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace OurGameName.DoMain.Entity.Combat
{
    /// <summary>
    /// 战斗场景角色图像类
    /// </summary>
    public class RoleImage : MonoBehaviour
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        private string path;

        /// <summary>
        /// 图片类型
        /// </summary>
        public string ImageType { get; private set; }

        /// <summary>
        /// 生命条
        /// </summary>
        private Image HPLine;

        /// <summary>
        /// 角色图像
        /// </summary>
        public RawImage Icon;

        /// <summary>
        /// 名字文本框
        /// </summary>
        public Text txtName;

        private int hp = 100;

        /// <summary>
        /// 摄像头
        /// </summary>
        private new UnityEngine.Camera camera;

        private void Awake()
        {
            HPLine = GetComponentInChildren<Image>();
            Icon = GetComponentInChildren<RawImage>();
            txtName = transform.Find("txtName").GetComponent<Text>();
            camera = UnityEngine.Camera.main;
        }

        private void FixedUpdate()
        {
        }

        public int HP
        {
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = 0;
                }
                var rect = HPLine.rectTransform.rect;
                rect.height = hp;
            }
        }

        public RoleType RoleType
        {
            set
            {
                if (value == RoleType.ally ||
                    value == RoleType.player)
                {
                    HPLine.GetComponentInChildren<Text>().enabled = false;
                    HPLine.enabled = false;
                }
                else
                {
                    HPLine.GetComponentInChildren<Text>().enabled = true;
                    HPLine.enabled = true;
                }
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
                ImageType = value.Split(".".ToCharArray()).Last().ToLower();
            }
        }
    }
}