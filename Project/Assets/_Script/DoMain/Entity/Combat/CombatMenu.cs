using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace OurGameName.DoMain.Entity.Combat
{
    /// <summary>
    /// 战斗菜单
    /// </summary>
    public class CombatMenu : MonoBehaviour
    {
        /// <summary>
        /// 子按钮列表
        /// </summary>
        public List<CombatMenuButton> sonBtnList;

        /// <summary>
        /// 技能列表
        /// </summary>
        public List<Skill> SkillList { get; set; }

        /// <summary>
        /// 按键预制体
        /// </summary>
        public CombatMenuButton BtnPrefab;

        private Image line;
        private Image V3;

        /// <summary>
        /// 菜单长度
        /// </summary>
        private float Len;

        /// <summary>
        /// 菜单斜率 单位弧度
        /// </summary>
        private float K;

        private new bool enabled;

        private void Awake()
        {
            line = transform.Find("Image").GetComponent<Image>();
            V3 = transform.Find("V3").GetComponent<Image>();
            Rect rect = line.rectTransform.rect;
            Len = rect.width;
            K = line.rectTransform.localRotation.eulerAngles.z * Mathf.PI / 180;
        }

        private void Update()
        {
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                if (enabled == value)
                {
                    return;
                }
                enabled = value;
                base.enabled = enabled;
                line.enabled = enabled;
                V3.enabled = enabled;
                if (sonBtnList != null)
                {
                    foreach (var item in sonBtnList)
                    {
                        item.Enabled = enabled;
                    }
                }
            }
        }

        public void Refresh()
        {
            AddBtn();

            SetPostiton();
        }

        /// <summary>
        /// 根据技能列表和按钮预制体,添加按钮实体
        /// </summary>
        private void AddBtn()
        {
            if (SkillList == null)
            {
                Debug.Log("SkillList is Null");
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("当前载入技能数 {0},具体技能如下:\n", SkillList.Count);
                foreach (var item in SkillList)
                {
                    builder.AppendFormat(item.Name + "\n");
                }
                Debug.Log(builder.ToString());
            }

            if (sonBtnList == null)
            {
                sonBtnList = new List<CombatMenuButton>();
            }

            foreach (var item in sonBtnList)
            {
                Destroy(item);
            }
            sonBtnList.Clear();

            foreach (var item in SkillList)
            {
                CombatMenuButton btn = Instantiate(BtnPrefab);
                btn.transform.SetParent(this.transform);
                sonBtnList.Add(btn);
                btn.Skill = item;
            }
        }

        /// <summary>
        /// 设置菜单上的按钮的位置
        /// </summary>
        /// <remarks>
        /// 需求:将按钮定位从上到下在距离菜单线段向右(→)*单位地方。按钮上下间隔*单位
        ///     *单位 为可设定数值
        /// 额外需求:
        ///     添加顶部对齐、居中对齐、底部对齐 三种模式
        ///     可以设置开始的位置
        /// 原理:
        /// 首先将菜单线段的中点为原点作平面直角坐标系
        /// 并将菜单线段视为线段a
        /// 然后将 a 的上端点与 X 轴正方向做垂线得到直线 b
        /// 最后将 a 的下端点与 b 做垂线得到直线 c
        /// 这三条线围成的图像即 三角形abc
        /// 下面将使用 b 上的点作为高度的基准点来定位按钮的位置
        /// </remarks>
        private void SetPostiton()
        {
            int btnHeight = 28;
            int btnWidth = 120;
            int btnMargin = 5;
            int btnStartMargin = 20;
            float tanK = (float)Math.Tan(K);   //根据斜率做点斜式方程 y = kx x = y/k
            //计算b 因为是以菜单的中点为原点所以具体的值要分出正负来符合实际情况
            float height = (float)Math.Sin(K) * Len * 0.5f;
            Vector3 Postion = Vector3.zero;

            for (int i = 0; i < sonBtnList.Count; i++)
            {
                float y = height - (i * (btnHeight + btnMargin)) - btnStartMargin;//设置按钮高度
                if (y < -height)    //判断是否超出菜单长度
                {
                    break;
                }
                float x = y / tanK + btnWidth / 2;
                Postion.x = x;
                Postion.y = y;
                sonBtnList[i].transform.localPosition = Postion;
            }
        }
    }
}