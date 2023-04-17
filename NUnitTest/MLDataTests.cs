using NUnit.Framework;
using System;

namespace UnitTests
{
    public class MLDataTests
    {
        [Test]
        public void PrepData()
        {
            var SourceConnString = TestContext.Parameters.Get("SourceConnString", string.Empty);
            var DestinationConnString = TestContext.Parameters.Get("DestinationConnString", string.Empty);

            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData(SourceConnString, DestinationConnString);
        }

        [Test]
        public void PrepData2()
        {
            var SourceConnString = TestContext.Parameters.Get("SourceConnString", string.Empty);
            var DestinationConnString = TestContext.Parameters.Get("DestinationConnString", string.Empty);

            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData2(SourceConnString, DestinationConnString);
        }
    }
}
