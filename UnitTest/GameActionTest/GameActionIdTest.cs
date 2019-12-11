using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OurGameName.DoMain.Entity.GameAction;

namespace UnitTest.GameActionTest
{
    [TestClass]
    public class GameActionIdTest
    {
        [TestMethod]
        public void ActionIDTest()
        {
            ActionID ID = new ActionID(ActionID.ActionTypeCode.Execution, 255, 65535);
            string s = $"{ID.UID:x}";
            Assert.AreEqual<ActionID.ActionTypeCode>(ActionID.ActionTypeCode.Execution, ID.ActionType);
            Assert.AreEqual<ushort>(65535, ID.ID);
            Assert.AreEqual<byte>(255, ID.RunType);
            Assert.AreEqual<uint>(4294967041, ID.UID);
        }
    }
}
