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
    using UnityEngine.ResourceManagement.AsyncOperations;

    /// <summary>
    /// 角色资源载入器
    /// </summary>
    internal sealed class RoleAssertLoader : MonoBehaviour
    {
        /// <summary>
        /// 头像资源标志
        /// </summary>
        public AssetLabelReference AvatarLable;

        /// <summary>
        /// 角色实体预制体
        /// </summary>
        public AssetReference RoleEntityPrefab;

        /// <summary>
        /// 立绘资源标志
        /// </summary>
        public AssetLabelReference WholeBodyImageLable;

        /// <summary>
        /// 头像资源
        /// </summary>
        public List<Sprite> Avatar { get; private set; }

        /// <summary>
        /// 角色实体资源
        /// </summary>
        public Task<RoleEntity> RoleEntity => this.InstantiateRoleEntityAsync();

        /// <summary>
        /// 立绘资源
        /// </summary>
        public List<Sprite> WholeBodyImage { get; private set; }

        #region Unity

        private void Awake()
        {
            _ = this.InitAsync();
        }

        private async Task InitAsync()
        {
            this.Avatar = await this.LoadSpriteAsync(this.AvatarLable);
            this.WholeBodyImage = await this.LoadSpriteAsync(this.WholeBodyImageLable);
        }

        private void Start()
        {
        }

        #endregion Unity

        /// <summary>
        /// 实例化角色实体
        /// </summary>
        /// <returns></returns>
        private async Task<RoleEntity> InstantiateRoleEntityAsync()
        {
            var load = await this.RoleEntityPrefab.InstantiateAsync().Task;
            return load.GetComponent<RoleEntity>();
        }

        /// <summary>
        /// 载入头像资源
        /// </summary>
        /// <param name="lable">载入标志</param>
        /// <returns></returns>
        private async Task<List<Sprite>> LoadSpriteAsync(AssetLabelReference lable)
        {
            var result = await LoaderHelper.LoadAssertsAsync<Sprite>(lable);
            return result.OrderBy(x => x.name).ToList();
        }
    }
}