using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OurGameName.DoMain.Attribute;
using Packages.Rider.Editor;
using UnityEngine;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTest.ConfigText")]
namespace OurGameName.Config
{
    /// <summary>
    /// xml 配置文档帮助类
    /// </summary>
    internal class XmlConfigHelper
    {
        /// <summary>
        /// xml 配置文件
        /// </summary>
        private XmlDocument m_configXml;

        /// <summary>
        /// Xml配置文件字典 用于对配置简直对的查找
        /// </summary>
        private Dictionary<ConfigType, Dictionary<string, string>> m_xmlConfigDict;

        private string configXmlPath;

        public XmlConfigHelper(string configXmlPath = null)
        {
            if (configXmlPath == null)
            {
                this.configXmlPath = Environment.CurrentDirectory + "\\Assets\\GlobalConfig.xml";
                configXmlPath = this.configXmlPath; 
            }
            else
            {
                this.configXmlPath = configXmlPath;
            }

            this.m_configXml = new XmlDocument();
            string configXml = "";
            try
            {
                configXml = FileHelper.ReadStrToFile(configXmlPath);
            }
            catch(FileNotFoundException)
            {
                FileHelper.SaveStrFile(configXmlPath, configXml);
            }
            this.m_configXml.LoadXml(configXml);

            IntXmlConfigDict(this.m_configXml);
        }

        /// <summary>
        /// 初始化Xml配置文件字典
        /// </summary>
        /// <param name="m_configXml"></param>
        private void IntXmlConfigDict(XmlDocument m_configXml)
        {
            m_xmlConfigDict = new Dictionary<ConfigType, Dictionary<string, string>>();
            int index;
            for (int i = 0; i < Enum.GetNames(typeof(ConfigType)).Length; i++)
            {
                var temp = new Dictionary<string, string>();
                index = m_configXml.ChildNodes.GetInex(((ConfigType)i).ToString());
                if (index != -1)
                { 
                    if (m_configXml.ChildNodes[index].HasChildNodes == true)
                    {
                        foreach (XmlNode item in m_configXml.ChildNodes[index].ChildNodes)
                        {
                            if (item.Name == "KeyValuePair")
                            {
                                temp.Add(item.Attributes["key"].Value, item.Attributes["value"].Value);
                            }
                        }
                    }
                }
                m_xmlConfigDict.Add((ConfigType)i, temp);
            }
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="configType">配置类型</param>
        /// <param name="configName">配置项名称</param>
        /// <returns>对象配置值</returns>
        public string GetConfig(ConfigType configType, string configName)
        {
            if (IsConfigDiceHasConfigName(configType, configName) == true)
            {
                return m_xmlConfigDict[configType][configName];
            }
            else
            {
                throw new XmlConfigNotFondException();
            }
        }

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="configType">配置类型</param>
        /// <param name="configName">配置项名称</param>
        /// <param name="configValue">配置项值</param>
        public void SetConfig(ConfigType configType, string configName, string configValue)
        {
            if (IsConfigDiceHasConfigName(configType, configName) == true)
            {
                var childNodes = m_configXml.ChildNodes;
                var configTypeNode = childNodes[childNodes.GetInex(configType.ToString())].ChildNodes;
                var targetNode = configTypeNode.First(item => item.Attributes["key"].Value == configName);
                targetNode.Attributes["value"].Value = configValue;
            }
            else
            {
                throw new XmlConfigNotFondException();
            }
        }

        /// <summary>
        /// 将实例中的保存到xml
        /// </summary>
        public void Save()
        {
            this.m_configXml.Save(configXmlPath);
        }

        /// <summary>
        /// Xml配置字典是否包含所查找的配置类型下的配置项
        /// </summary>
        /// <param name="configType">配置类型</param>
        /// <param name="configName">配置项</param>
        /// <returns></returns>
        private bool IsConfigDiceHasConfigName(ConfigType configType, string configName)
        {
            return m_xmlConfigDict.ContainsKey(configType) && m_xmlConfigDict[configType].ContainsKey(configName);
        }
    }

    /// <summary>
    /// 未找到Xml配置项异常
    /// </summary>
    internal class XmlConfigNotFondException : Exception
    {
        /// <summary>
        /// 未找到Xml配置项异常
        /// </summary>
        public XmlConfigNotFondException()
        {
        }
    }

    /// <summary>
    /// 设置文档类型
    /// </summary>
    internal enum ConfigType
    {
        /// <summary>
        /// 地址设置
        /// </summary>
        PathConfig
    }
}
