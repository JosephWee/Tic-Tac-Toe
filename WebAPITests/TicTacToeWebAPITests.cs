using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using TicTacToe.Extensions;
using T3Mod = TicTacToe.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http;

namespace UnitTests
{
    public class TicTacToeWebApiTests
    {
        private string _TicTacToeWebApiEndPoint = string.Empty;

        [SetUp]
        public void TestSetup()
        {
            //Trace.Listeners.Add(new ConsoleTraceListener());

            string solutionDir =
                new DirectoryInfo(
                    Path.Combine(
                        TestContext.CurrentContext.TestDirectory,
                        "..", "..", "..", "..")).FullName;

            var TicTacToeWebApiEndPoint =
                TestContext
                .Parameters
                .Get("TicTacToeWebApiEndPoint", string.Empty);

            Assert.IsNotEmpty(TicTacToeWebApiEndPoint);

            _TicTacToeWebApiEndPoint = TicTacToeWebApiEndPoint;
        }

        [TearDown]
        public void EndTest()
        {
            //Trace.Flush();
        }

        [Test]
        public async Task TestGetTicTacToeResult()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var tsStart = DateTime.UtcNow;

                List<Task<string>> getResultTasks = new List<Task<string>>();

                for (int i = 1; i <= 300; i++)
                {
                    string url = _TicTacToeWebApiEndPoint + $"?pageSize=10&pageNum=1";
                    getResultTasks.Add(
                        GetTicTacToeResult(url, httpClient)
                    );
                }

                List<string> results = new List<string>();
                while (getResultTasks.Any())
                {
                    var completedTask = await Task.WhenAny(getResultTasks);
                    getResultTasks.Remove(completedTask);

                    string result = await completedTask;
                    if (!string.IsNullOrEmpty(result))
                    {
                        //Console.WriteLine(result);
                        results.Add(result);
                    }
                }

                var tsEnd = DateTime.UtcNow;

                string elapsedTime =
                        string.Format("Elapsed time (ms): {0}\n", (tsEnd - tsStart).TotalMilliseconds);

                Console.WriteLine(elapsedTime);

                List<T3Mod.TicTacToeResultSet> t3Results = new List<T3Mod.TicTacToeResultSet>();
                foreach (var jsonString in results)
                {
                    T3Mod.TicTacToeResultSet t3Result = JsonSerializer.Deserialize<T3Mod.TicTacToeResultSet>(jsonString);
                    if (t3Result != null)
                        t3Results.Add(t3Result);
                }

                var groupByAppInstanceId = t3Results.GroupBy(x => x.AppInstanceId);
                foreach (var grp in groupByAppInstanceId)
                {
                    string AppInstanceIdCount =
                        string.Format("AppInstanceId: {0}\r\n\tCount: {1}", grp.Key, grp.Count());

                    Console.WriteLine(AppInstanceIdCount);
                }
            }
        }

        public async Task<string> GetTicTacToeResult(string url, HttpClient httpClient)
        {
            var httpResponseMessage = await httpClient.GetAsync(url);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return content;
            }

            return string.Empty;
        }
    }
}