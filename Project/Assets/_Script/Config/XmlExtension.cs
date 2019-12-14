using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OurGameName.Config
{
    internal static class XmlExtension
    {
        /// <summary>
        /// 获取第一个名字为nodeName的Xml节点的索引
        /// </summary>
        /// <param name="xmlNodeList">被查找的xml节点列表</param>
        /// <param name="nodeName">需要查找索引的xml节点的名字</param>
        /// <returns>找到返回对应节点返回索引 没找到返回-1</returns>
        public static int GetInex(this XmlNodeList xmlNodeList,string nodeName)
        {
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                if (xmlNodeList[i].Name == nodeName)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 搜索并返回第一个与指定指定谓词所定义的条件相匹配的元素
        /// </summary>
        /// <param name="xmlNodeList">被查找的xmlNodeList</param>
        /// <param name="match">Predicate<XmlNode> 委托，用于定义要搜索的元素的条件。</param>
        /// <returns></returns>
        public static XmlNode First(this XmlNodeList xmlNodeList,Predicate<XmlNode> match)
        {
            foreach (XmlNode item in xmlNodeList)
            {
                if (match(item) == true)
                {
                    return item;
                }
            }
            throw new XmlConfigNotFondException();
        }

        /// <summary>
        /// 是否存在对应名字的xml节点
        /// </summary>
        /// <param name="xmlNodeList">被查找的xml节点列表</param>
        /// <param name="nodeName">被查找的xml节点的名字</param>
        /// <returns>存在返回:true 不存在返回：false</returns>
        public static bool Contains(this XmlNodeList xmlNodeList, string nodeName)
        {
            return -1 != xmlNodeList.GetInex(nodeName);
        }
    }
}
