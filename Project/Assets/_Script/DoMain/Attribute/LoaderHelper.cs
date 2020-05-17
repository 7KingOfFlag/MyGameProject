namespace OurGameName.DoMain.Attribute
{
    using System.Collections.Generic;
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
        internal static IList<T> LoadAsserts<T>(AssetLabelReference lable)
        {
            var loader = Addressables.LoadAssetsAsync<T>(lable, null).Task;
            loader.Wait();
            return loader.Result;
        }
    }
}