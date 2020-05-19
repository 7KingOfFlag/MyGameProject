namespace OurGameName.DoMain.RoleSpace.Component
{
    using Boo.Lang;
    using OurGameName.DoMain.UIComponent.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.UI;

    /// <summary>
    /// 行动点生成器
    /// </summary>
    public sealed class ActionPointComponent : MonoBehaviour
    {
        /// <summary>
        /// 行动点资源
        /// </summary>
        public AssetReference ActionPointAsset;

        /// <summary>
        /// 行动点最大值
        /// </summary>
        public int MaxActionPoint;

        /// <summary>
        /// 生成的行动点对象实例
        /// </summary>
        private List<Image> ActionPointList;

        /// <summary>
        /// 当前行动点
        /// </summary>
        private int currentActionPoint;

        /// <summary>
        /// 行动点 设置后显示指定数量行动点
        /// </summary>
        public int ActionPoint
        {
            get => this.currentActionPoint;
            set
            {
                if (value != this.currentActionPoint)
                {
                    value = Mathf.Min(value, this.MaxActionPoint);
                    this.ResetActionPoint(value);
                    this.currentActionPoint = value;
                }
            }
        }

        /// <summary>
        /// 初始化行动点
        /// </summary>
        /// <param name="maxActionPoint">行动点最大值</param>
        public void Init(int maxActionPoint)
        {
            this.MaxActionPoint = maxActionPoint;
            this.ActionPointList = new List<Image>(this.MaxActionPoint);
            for (int i = 0; i < this.MaxActionPoint; i++)
            {
                this.ActionPointAsset.InstantiateAsync(this.transform, false).Completed += obj =>
                {
                    this.ActionPointList.Add(obj.Result.GetComponent<Image>());
                    var index = this.ActionPointList.Count - 1;
                    this.ActionPointList[index].rectTransform.localPosition = new Vector3(index * 5, 0, 0);
                };
            }
        }

        /// <summary>
        /// 隐藏指定数量行动点
        /// </summary>
        /// <param name="value"></param>
        private void HideActionPoint(int value)
        {
            for (int i = 0; i < value; i++)
            {
                this.ActionPointList[this.currentActionPoint - value].SetAlpha(0);
            }
        }

        /// <summary>
        /// 重设行动点数量
        /// </summary>
        /// <param name="value">行动点数量</param>
        private void ResetActionPoint(int value)
        {
            int diff = value - this.currentActionPoint;

            if (diff > 0)
            {
                this.ShowActionPoint(diff);
            }
            else
            {
                this.HideActionPoint(-diff);
            }
        }

        /// <summary>
        /// 显示隐藏指定数量行动点
        /// </summary>
        /// <param name="value">添加个数</param>
        private void ShowActionPoint(int value)
        {
            for (int i = 0; i < value; i++)
            {
                this.ActionPointList[this.currentActionPoint + i].SetAlpha(1f);
            }
        }
    }
}