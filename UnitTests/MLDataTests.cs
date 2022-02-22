using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class MLDataTests
    {
        [TestMethod]
        public void PrepData()
        {
            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData("name=SourceConnString", "name=DestinationConnString");
        }
    }
}
