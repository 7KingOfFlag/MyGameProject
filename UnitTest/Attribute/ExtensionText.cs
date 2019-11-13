using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OurGameName.DoMain.Attribute;

namespace UnitTest.Attribute
{
    [TestClass]
    public class ExtensionText
    {
        [TestMethod]
        public void SwopTest()
        {
            int a = 2, b = 3;
            Extension.Swop(ref a, ref b);
            Assert.IsTrue(a == 3 && b == 2);
        }

        [TestMethod]
        public void RemoveNumberTest()
        {
            Assert.AreEqual("ABC", "74A5B12C".RemoveNumber());
        }
    }
}
