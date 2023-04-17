using NUnit.Framework;
using System;

namespace UnitTests
{
    public class MLDataTests
    {
        [Test]
        public void PrepData()
        {
            string msbuildDir = new DirectoryInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..")).FullName;
            var SourceConnString = TestContext.Parameters.Get("SourceConnString", string.Empty).Replace("$(MSBuildProjectDirectory)", msbuildDir);
            var DestinationConnString = TestContext.Parameters.Get("DestinationConnString", string.Empty).Replace("$(MSBuildProjectDirectory)", msbuildDir);

            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData(SourceConnString, DestinationConnString);
        }

        [Test]
        public void PrepData2()
        {
            string msbuildDir = new DirectoryInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..")).FullName;
            var SourceConnString = TestContext.Parameters.Get("SourceConnString", string.Empty).Replace("$(MSBuildProjectDirectory)", msbuildDir);
            var DestinationConnString = TestContext.Parameters.Get("DestinationConnString", string.Empty).Replace("$(MSBuildProjectDirectory)", msbuildDir);

            TicTacToe
                .BusinessLogic
                .TicTacToe
                .PrepData2(SourceConnString, DestinationConnString);
        }
    }
}
