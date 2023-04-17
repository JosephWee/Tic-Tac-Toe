using NUnit.Framework;
using System;

namespace UnitTests
{
    public class MLDataTests
    {
        [Test]
        public void PrepData()
        {
            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData("name=SourceConnString", "name=DestinationConnString");
        }

        [Test]
        public void PrepData2()
        {
            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData2("name=SourceConnString", "name=DestinationConnString");
        }
    }
}
