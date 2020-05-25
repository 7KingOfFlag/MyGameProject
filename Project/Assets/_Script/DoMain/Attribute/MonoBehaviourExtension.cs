namespace OurGameName.DoMain.Attribute
{
    using UnityEngine;

    internal static class MonoBehaviourExtension
    {
        /// <summary>
        /// 是否设置Unity公开属性
        /// </summary>
        /// <param name="mono">mono对象</param>
        /// <param name="propertyName">属性名</param>
        public static void IsSet(this MonoBehaviour mono, string propertyName)
        {
            if (mono == null) Debug.LogError($"{propertyName}未配置");
        }
    }
}