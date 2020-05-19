namespace OurGameName.DoMain.Attribute
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine.AddressableAssets;

    /// <summary>
    /// 资源载入帮助类
    /// </summary>
    internal static class LoaderHelper
    {
        /// <summary>
        /// 载入标签资源
        /// </summary>
        /// <typeparam name="T">载入类型</typeparam>
        /// <param name="lable">资源标签</param>
        /// <returns>载入的资源</returns>
        internal static async Task<IList<T>> LoadAssertsAsync<T>(AssetLabelReference lable)
        {
            return await Addressables.LoadAssetsAsync<T>(lable, null).Task; ;
        }
    }
}