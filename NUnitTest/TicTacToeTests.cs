using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class TicTacToeTests
    {
        [SetUp]
        public void TestSetup()
        {
            string msbuildDir = new DirectoryInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..")).FullName;
            var TicTacToeDataConnString = TestContext.Parameters.Get("TicTacToeDataConnString", string.Empty).Replace("$(MSBuildProjectDirectory)", msbuildDir);
            Assert.IsNotEmpty(TicTacToeDataConnString);
            TicTacToe.Entity.DbContextConfig.AddOrReplace("TicTacToeData", TicTacToeDataConnString);
        }

        [Test]
        public void TestPlayer1Wins()
        {
            var games = new List<List<List<int>>>()
            {
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      2,1,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      1,0,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      1,0,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      1,0,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,0,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      1,2,2
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,0,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,0,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,0,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,2,0,
                      1,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      1,0,0
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      1,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      1,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,2,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      1,1,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      2,2,0,
                      0,1,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,1,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      1,2,0
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,1,
                      0,2,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      1,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      1,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,2,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,1,1,
                      2,2,0,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,1,1,
                      0,2,2,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,0,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,1,0,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,0,
                      2,2,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,0,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,0,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,0,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      1,2,0
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      1,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,2,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,2,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      1,2,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,2,
                      1,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,0,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,0,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      1,1,2,
                      2,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,2,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,1,0,
                      0,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,2,
                      0,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      2,1,0,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,0,
                      1,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,2,
                      1,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,0,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,1,
                      2,0,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,0,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,2,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,2,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,0,
                      1,0,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,0,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,2,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,2,
                      1,2,1
                   }
                }
            };

            int countMoves = 0;
            int countInProgress = 0;
            int countPlayer1Wins = 0;
            int countPlayer2Wins = 0;
            int countDraws = 0;

            foreach (var game in games)
            {
                foreach (var move in game)
                {
                    string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")} - M{countMoves + 1}";

                    TicTacToe.Models.TicTacToeUpdateRequest request =
                        new TicTacToe.Models.TicTacToeUpdateRequest();
                    request.InstanceId = InstanceId;
                    request.GridSize = 3;
                    request.CellStates = move;

                    TicTacToe.Models.TicTacToeUpdateResponse response = null;

                    response =
                        TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);

                    Assert.IsNotNull(response);
                    countMoves++;

                    if (response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress)
                        countInProgress++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Draw)
                        countDraws++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Player1Wins)
                        countPlayer1Wins++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Player2Wins)
                        countPlayer2Wins++;
                }
            }

            Assert.AreEqual(games.Sum(g => g.Count), countMoves);
            Assert.AreEqual(0, countDraws);
            Assert.AreEqual(games.Count, countPlayer1Wins);
            Assert.AreEqual(0, countPlayer2Wins);
        }

        [Test]
        public void TestPlayer2Wins()
        {
            var games = new List<List<List<int>>>()
            {
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,2,0,
                      2,1,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,2,0,
                      2,1,1,
                      0,1,0
                   },
                   new List<int>()
                   {
                      2,2,0,
                      2,1,1,
                      0,1,0
                   },
                   new List<int>()
                   {
                      2,2,1,
                      2,1,1,
                      0,1,0
                   },
                   new List<int>()
                   {
                      2,2,1,
                      2,1,1,
                      2,1,0
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,2,
                      1,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      1,0,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      1,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      1,0,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,0,
                      1,0,0
                   },
                   new List<int>()
                   {
                      2,0,0,
                      1,2,0,
                      1,0,0
                   },
                   new List<int>()
                   {
                      2,1,0,
                      1,2,0,
                      1,0,0
                   },
                   new List<int>()
                   {
                      2,1,0,
                      1,2,0,
                      1,0,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      2,1,1,
                      0,1,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      2,1,1,
                      2,1,0,
                      2,0,0
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,0,
                      0,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,2,
                      1,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,2,
                      1,2,0,
                      2,2,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      2,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      2,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,1,0,
                      2,1,1,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,1,0,
                      2,1,1,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,0,0
                   },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      1,0,0
                   },
                   new List<int>()
                   {
                      2,0,2,
                      1,1,2,
                      1,0,0
                   },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,0,0
                   },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,0,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,0,
                      1,2,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,2,
                      1,2,1
                   },
                   new List<int>()
                   {
                      0,0,1,
                      1,2,2,
                      1,2,1
                   },
                   new List<int>()
                   {
                      0,2,1,
                      1,2,2,
                      1,2,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,1,
                      0,2,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,1,
                      0,2,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,1,
                      1,2,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,1,
                      1,2,2
                   },
                   new List<int>()
                   {
                      0,1,0,
                      1,2,1,
                      1,2,2
                   },
                   new List<int>()
                   {
                      2,1,0,
                      1,2,1,
                      1,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,1,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      1,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,2,2,
                      1,2,0,
                      2,1,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,1,2
                   },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,1,2
                   },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      1,1,2
                   },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,0,
                      1,1,2
                   },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,1,
                      1,1,2
                   },
                   new List<int>()
                   {
                      2,2,2,
                      0,1,1,
                      1,1,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      0,0,2
                   },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,0,2
                   },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,1,
                      0,0,2
                   },
                   new List<int>()
                   {
                      0,0,1,
                      2,1,1,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,1,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,1,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,1
                   },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      2,0,1
                   },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,1,
                      2,0,1
                   },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,1,
                      2,0,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,1,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,0,1,
                      2,1,1,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,2,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,2,2,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      1,2,2,
                      0,1,0
                   },
                   new List<int>()
                   {
                      2,1,0,
                      1,2,2,
                      0,1,0
                   },
                   new List<int>()
                   {
                      2,1,1,
                      1,2,2,
                      0,1,0
                   },
                   new List<int>()
                   {
                      2,1,1,
                      1,2,2,
                      0,1,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      1,1,0,
                      2,2,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      1,1,0,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,1,0,
                      0,1,0,
                      2,0,2
                   },
                   new List<int>()
                   {
                      1,1,0,
                      0,1,0,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      1,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      1,2,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,0,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,0
                   },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,1
                   },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,0,
                      2,2,1
                   },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,1,
                      2,2,1
                   },
                   new List<int>()
                   {
                      2,1,0,
                      2,1,1,
                      2,2,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      0,1,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      1,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      0,0,2,
                      1,2,0,
                      2,1,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      1,1,0
                   },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      1,1,2
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,2,
                      1,1,2
                   },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,2,
                      1,1,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      0,1,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      0,1,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,2,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,2,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,0,1,
                      2,2,2,
                      2,1,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,1,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      0,1,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,0,
                      0,2,2
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,1,
                      0,2,2
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,1,1,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,2,2
                   },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,0,
                      0,2,2
                   },
                   new List<int>()
                   {
                      1,0,1,
                      0,1,0,
                      2,2,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,2,1,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,2,1,
                      0,0,0,
                      1,0,0
                   },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,0,0
                   },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,1,0
                   },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,1,2
                   },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,1,
                      1,1,2
                   },
                   new List<int>()
                   {
                      2,2,1,
                      0,2,1,
                      1,1,2
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,0,
                      0,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,1,
                      0,2,1
                   },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,1,
                      2,2,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,1,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,1,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,2,2,
                      0,2,1,
                      2,1,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,2,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,2,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,2,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      1,2,2,
                      0,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      1,2,2,
                      2,0,1
                   },
                   new List<int>()
                   {
                      1,0,0,
                      1,2,2,
                      2,1,1
                   },
                   new List<int>()
                   {
                      1,0,2,
                      1,2,2,
                      2,1,1
                   }
               },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                   },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      1,0,2
                   },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,2,
                      1,0,2
                   }
                }
            };

            int countMoves = 0;
            int countInProgress = 0;
            int countPlayer1Wins = 0;
            int countPlayer2Wins = 0;
            int countDraws = 0;

            foreach (var game in games)
            {
                foreach (var move in game)
                {
                    string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")} - M{countMoves + 1}";

                    TicTacToe.Models.TicTacToeUpdateRequest request =
                        new TicTacToe.Models.TicTacToeUpdateRequest();
                    request.InstanceId = InstanceId;
                    request.GridSize = 3;
                    request.CellStates = move;

                    TicTacToe.Models.TicTacToeUpdateResponse response = null;

                    response =
                        TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);

                    Assert.IsNotNull(response);
                    countMoves++;

                    if (response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress)
                        countInProgress++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Draw)
                        countDraws++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Player1Wins)
                        countPlayer1Wins++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Player2Wins)
                        countPlayer2Wins++;
                }
            }

            Assert.AreEqual(games.Sum(g => g.Count), countMoves);
            Assert.AreEqual(0, countDraws);
            Assert.AreEqual(0, countPlayer1Wins);
            Assert.AreEqual(games.Count, countPlayer2Wins);
        }

        [Test]
        public void TestDraw()
        {
            var games = new List<List<List<int>>>()
            {
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      2,2,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,1,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      1,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,2,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,2,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,2,2,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,2,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,2,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,2,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,0,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      2,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      2,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      2,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      0,2,2
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,0,
                      1,2,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,1,0,
                      2,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,1,0,
                      2,2,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,2,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,2,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,2,1,
                      1,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,2,1,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,0,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      0,1,0,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      1,1,2,
                      2,2,0
                    },
                   new List<int>()
                   {
                      0,1,0,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      2,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,2,2,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,1,
                      2,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      2,1,1,
                      2,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,0
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,2,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,0
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      0,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,0,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,0,
                      1,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,2,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      1,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      1,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,0,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,1,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      2,1,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      2,1,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,0,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,1,1
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      0,1,1
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,2,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,1,0,
                      0,2,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,0,
                      1,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,0,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      0,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      0,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,0,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,0
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      1,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,1,2,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      0,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      1,2,2,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      1,2,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,0,1,
                      1,2,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,2,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,0,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,0,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,1,0,
                      0,2,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      0,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,1,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,0,
                      1,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,2,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,1,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      0,2,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      0,0,1,
                      1,2,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,2,2,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,2,2,
                      1,0,1
                    },
                   new List<int>()
                   {
                      2,0,1,
                      1,2,2,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,2,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,1,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      1,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,2,1,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      0,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      1,0,1
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,2,1,
                      1,2,1
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,2,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      0,1,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,0,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,0,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,0,
                      1,0,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,0,0,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,0,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      0,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,0,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      2,2,1
                    },
                   new List<int>()
                   {
                      2,1,1,
                      1,1,2,
                      2,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,0,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      0,2,0
                    },
                   new List<int>()
                   {
                      2,1,0,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      1,1,2,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,0
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      2,1,1,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      2,1,1,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,0,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      0,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      0,1,2
                    },
                   new List<int>()
                   {
                      1,2,2,
                      2,1,1,
                      1,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      0,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,0,2
                    },
                   new List<int>()
                   {
                      1,0,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      0,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,0,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,0,
                      1,1,2,
                      2,1,2
                    },
                   new List<int>()
                   {
                      1,2,1,
                      1,1,2,
                      2,1,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,0,
                      1,1,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      0,2,1,
                      0,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      0,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,0,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      2,0,1
                    },
                   new List<int>()
                   {
                      2,0,0,
                      1,1,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,0,
                      1,1,2,
                      2,1,1
                    },
                   new List<int>()
                   {
                      2,2,1,
                      1,1,2,
                      2,1,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,0,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,0,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,0,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      0,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      0,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      0,2,0
                    },
                   new List<int>()
                   {
                      0,0,0,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      0,0,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,2,0
                    },
                   new List<int>()
                   {
                      1,0,2,
                      2,1,1,
                      1,2,2
                    },
                   new List<int>()
                   {
                      1,1,2,
                      2,1,1,
                      1,2,2
                   }
                },
                new List<List<int>>()
                {
                   new List<int>()
                   {
                      0,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,0,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,0,
                      0,0,0
                    },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,0,
                      0,2,0
                    },
                   new List<int>()
                   {
                      2,1,0,
                      0,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      2,1,0,
                      0,2,1
                    },
                   new List<int>()
                   {
                      2,1,0,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,0,
                      1,2,1
                    },
                   new List<int>()
                   {
                      2,1,2,
                      2,1,1,
                      1,2,1
                   }
                }
            };

            int countMoves = 0;
            int countInProgress = 0;
            int countPlayer1Wins = 0;
            int countPlayer2Wins = 0;
            int countDraws = 0;

            foreach (var game in games)
            {
                foreach (var move in game)
                {
                    string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")} - M{countMoves + 1}";
                    
                    TicTacToe.Models.TicTacToeUpdateRequest request =
                        new TicTacToe.Models.TicTacToeUpdateRequest();
                    request.InstanceId = InstanceId;
                    request.GridSize = 3;
                    request.CellStates = move;

                    TicTacToe.Models.TicTacToeUpdateResponse response = null;

                    response =
                        TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);

                    Assert.IsNotNull(response);
                    countMoves++;

                    if (response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress)
                        countInProgress++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Draw)
                        countDraws++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Player1Wins)
                        countPlayer1Wins++;
                    else if (response.Status == TicTacToe.Models.TicTacToeGameStatus.Player2Wins)
                        countPlayer2Wins++;
                }
            }

            Assert.AreEqual(games.Count * 9, countMoves);
            Assert.AreEqual(games.Count, countDraws);
            Assert.AreEqual(0, countPlayer1Wins);
            Assert.AreEqual(0, countPlayer2Wins);
        }

        [Test]
        public void TestAnomalousData()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            TicTacToe.Models.TicTacToeGameStatus expectedGameStatus = TicTacToe.Models.TicTacToeGameStatus.InProgress;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                0, 0, 0,
                0, 3, 0,
                0, 0, 0
            };
            response = null;

            bool exceptionFound = false;
            try
            {
                response =
                    TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            }
            catch (ArgumentException ex)
            {
                exceptionFound = true;
            }

            Assert.IsTrue(exceptionFound);
        }

        [Test]
        public void TestDataRetrievalAndValidation()
        {
            string InstanceId = $"UnitTest @ {DateTime.UtcNow.ToString("o")}";
            TicTacToe.Models.TicTacToeUpdateRequest request;
            TicTacToe.Models.TicTacToeUpdateResponse response;
            int MoveNumber = 0;

            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 0, 0,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 2, 0,
                0, 0, 0
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 2, 0,
                0, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 0,
                0, 2, 0,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 0, 1,
                0, 2, 0,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 2, 1,
                0, 2, 0,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.InProgress);
            MoveNumber++;



            request = new TicTacToe.Models.TicTacToeUpdateRequest();
            request.InstanceId = InstanceId;
            request.GridSize = 3;
            request.CellStates = new System.Collections.Generic.List<int>()
            {
                1, 2, 1,
                0, 2, 1,
                2, 0, 1
            };
            response = null;

            response =
                TicTacToe.BusinessLogic.TicTacToe.EvaluateResult(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Status == TicTacToe.Models.TicTacToeGameStatus.Player1Wins);
            MoveNumber++;


            var ds =
                TicTacToe.BusinessLogic.TicTacToe.GetAndValidatePreviousMove(InstanceId);

            int FetchedMoveNumber = ds.Max(x => x.MoveNumber);
            Assert.AreEqual(MoveNumber, FetchedMoveNumber);
        }
    }
}