using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using OurGameName.Config;
using System.Text;
using System.Threading.Tasks;
using OurGameName.DoMain.Attribute;

namespace UnitTest.ConfigText
{
    [TestClass]
    public class XmlConfigHelperxmTest
    {
        [TestMethod]
        public void XmlConfigHelperReadTest()
        {
            FileHelper.SaveStrFile("test.xml", TextXMl());
            XmlConfigHelper xmlConfigHelper = new XmlConfigHelper("test.xml");
            string GameSavePath = xmlConfigHelper.GetConfig(ConfigType.PathConfig, "GameSavePath");
            Assert.AreEqual("/Save/", GameSavePath);
        }

        [TestMethod]
        public void XmlConfigHelperWriteTest()
        {
            FileHelper.SaveStrFile("test.xml", TextXMl());
            XmlConfigHelper xmlConfigHelper = new XmlConfigHelper("test.xml");
            xmlConfigHelper.SetConfig(ConfigType.PathConfig, "GameSavePath",@"/SaveDir/");
            xmlConfigHelper.Save();
            string expected = @"<?xml version=""1.0"" encoding=""utf-8""?>
      <PathConfig>
        <KeyValuePair key=""GameSavePath"" value=""/SaveDir/"" />
         </PathConfig> ";
            string actual = FileHelper.ReadStrToFile("test.xml");
            Assert.AreEqual(expected.Replace(" ",""), actual.Replace(" ", ""));
        }

        private string TextXMl()
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
                        <PathConfig>
                          <KeyValuePair key = ""GameSavePath"" value = ""/Save/""/>
                           </PathConfig>
                         ";
        }
    }
}
