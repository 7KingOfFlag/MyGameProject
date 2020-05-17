namespace OurGameName.DoMain.RoleSpace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OurGameName.DoMain.Attribute;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public sealed class RoleImageLoader : MonoBehaviour
    {
        /// <summary>
        /// 头像资源标志
        /// </summary>
        public AssetLabelReference AvatarLable;

        /// <summary>
        /// 立绘资源标志
        /// </summary>
        public AssetLabelReference WholeBodyImageLable;

        /// <summary>
        /// 头像资源延迟加载器
        /// </summary>
        private Lazy<List<Sprite>> AvatarLoader;

        /// <summary>
        /// 立绘资源延迟加载器
        /// </summary>
        private Lazy<List<Sprite>> WholeBodyLoader;

        /// <summary>
        /// 头像资源
        /// </summary>
        public List<Sprite> Avatar { get => this.AvatarLoader.Value; }

        /// <summary>
        /// 立绘资源
        /// </summary>
        public List<Sprite> WholeBodyImage { get => this.WholeBodyLoader.Value; }

        #region Unity

        private void Awake()
        {
            this.AvatarLoader = new Lazy<List<Sprite>>(() => this.LoadSprite(this.AvatarLable));
            this.WholeBodyLoader = new Lazy<List<Sprite>>(() => this.LoadSprite(this.WholeBodyImageLable));
        }

        /// <summary>
        /// 载入头像资源
        /// </summary>
        /// <param name="lable">载入标志</param>
        /// <returns></returns>
        private List<Sprite> LoadSprite(AssetLabelReference lable)
        {
            return LoaderHelper.LoadAsserts<Sprite>(lable).OrderBy(x => x.name).ToList();
        }

        #endregion Unity
    }
}