using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML;
using Newtonsoft.Json.Linq;
using TicTacToe.Extensions;
using T3BL = TicTacToe.BusinessLogic;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;
using Microsoft.ML.Trainers.FastTree;
using System.Text.RegularExpressions;
using Microsoft.ML.Data;

namespace MLModelMetrics
{
    public class MLModelEvaluation
    {
        bool skipTest = false;
        private static List<FileInfo> _MLModelFiles = null;
        private static List<FileInfo> _MLDataFiles = null;
        private static List<FileInfo> inputFiles = new List<FileInfo>();

        [SetUp]
        public void Setup()
        {
            skipTest = false;

            string solutionDir =
                new DirectoryInfo(
                    Path.Combine(
                        TestContext.CurrentContext.TestDirectory,
                        "..", "..", "..", "..")).FullName;

            var MLModelPath =
                TestContext
                .Parameters
                .Get("MLModelPath", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            Assert.IsFalse(string.IsNullOrWhiteSpace(MLModelPath));

            bool IsMLModelPathValid = false;
            if (Directory.Exists(MLModelPath))
            {
                _MLModelFiles = Directory.GetFiles(MLModelPath, "*.zip").Select(x => new FileInfo(x)).ToList();
                IsMLModelPathValid = true;
            }
            else if (MLModelPath.EndsWith(".zip"))
            {
                List<string> MLModelPathParts = MLModelPath.Split(Path.DirectorySeparatorChar).ToList();
                string fileName = MLModelPathParts.Last();
                string dirName = string.Join(Path.DirectorySeparatorChar, MLModelPathParts.Take(MLModelPathParts.Count - 1));
                if (Directory.Exists(dirName))
                {
                    _MLModelFiles = Directory.GetFiles(dirName, fileName).Select(x => new FileInfo(x)).ToList();
                    IsMLModelPathValid = true;
                }
            }
            
            if (!IsMLModelPathValid)
            {
                throw new FileNotFoundException("MLModelPath must be an existing directory or file path containing the ML Model zip files. Use wildcards * to specify multiple files.");
            }

            var MLDataPath =
                TestContext
                .Parameters
                .Get("MLTrainingDataPath", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar); ;

            Assert.IsFalse(string.IsNullOrWhiteSpace(MLDataPath));

            bool IsMLDataPathValid = false;
            if (Directory.Exists(MLDataPath))
            {
                _MLDataFiles = Directory.GetFiles(MLDataPath, "*.csv").Select(x => new FileInfo(x)).ToList();
                IsMLDataPathValid = true;
            }
            else if (MLDataPath.EndsWith(".csv"))
            {
                List<string> MLDataPathParts = MLDataPath.Split(Path.DirectorySeparatorChar).ToList();
                string fileName = MLDataPathParts.Last();
                string dirName = string.Join(Path.DirectorySeparatorChar, MLDataPathParts.Take(MLDataPathParts.Count - 1));
                if (Directory.Exists(dirName))
                {
                    _MLDataFiles = Directory.GetFiles(dirName, fileName).Select(x => new FileInfo(x)).ToList();
                    IsMLDataPathValid = true;
                }
            }

            if (!IsMLDataPathValid)
            {
                throw new FileNotFoundException("MLDataPath must be an existing directory or file path containing the ML Model zip files. Use wildcards * to specify multiple files.");
            }
        }

        [Test]
        public void Evaluation()
        {
            // Load Data
            List<T3ML.MLModel1.ModelInput> inputs = new List<T3ML.MLModel1.ModelInput>();
            List<FileInfo> processedFiles = new List<FileInfo>();
            List<FileInfo> rejectedFiles = new List<FileInfo>();
            for (int i = 0; i < _MLDataFiles.Count; i++)
            {
                var inputFile = _MLDataFiles[i];

                try
                {
                    bool fileHasError = false;
                    List<T3ML.MLModel1.ModelInput> fileInputs = new List<T3ML.MLModel1.ModelInput>();

                    using (TextReader textReader = inputFile.OpenText())
                    {
                        var line = textReader.ReadLine();
                        while (!string.IsNullOrWhiteSpace(line))
                        {
                            var fields = line.Split(',').ToList();
                            if (fields.Count != 11)
                            {
                                fileHasError = true;
                                break;
                            }

                            fields = fields.Select(x => x.TrimStart('"').TrimEnd('"')).ToList();

                            if (string.Join(",", fields).ToLower() == "movenumber,cell0,cell1,cell2,cell3,cell4,cell5,cell6,cell7,cell8,gameresultcode")
                            {
                                line = textReader.ReadLine();
                                continue;
                            }

                            T3ML.MLModel1.ModelInput newRow = new T3ML.MLModel1.ModelInput()
                            {
                                MoveNumber = float.Parse(fields[0]),
                                Cell0 = float.Parse(fields[1]),
                                Cell1 = float.Parse(fields[2]),
                                Cell2 = float.Parse(fields[3]),
                                Cell3 = float.Parse(fields[4]),
                                Cell4 = float.Parse(fields[5]),
                                Cell5 = float.Parse(fields[6]),
                                Cell6 = float.Parse(fields[7]),
                                Cell7 = float.Parse(fields[8]),
                                Cell8 = float.Parse(fields[9]),
                                GameResultCode = float.Parse(fields[10])
                            };

                            fileInputs.Add(newRow);

                            line = textReader.ReadLine();
                        }
                    }

                    if (fileHasError)
                    {
                        rejectedFiles.Add(inputFile);
                    }
                    else
                    {
                        inputs.AddRange(fileInputs);
                        processedFiles.Add(inputFile);
                    }
                }
                catch (Exception ex)
                {
                    //Parsing the file failed
                    rejectedFiles.Add(inputFile);
                }
            }

            if (processedFiles.Any())
            {
                MLContext mlContext = new MLContext();
                DataViewSchema modelSchema;

                var metrics = new Dictionary<string, MulticlassClassificationMetrics>();
                for (int i = 0; i < _MLModelFiles.Count; i++)
                {
                    //var predEngine = mlContext.Model.CreatePredictionEngine<T3ML.MLModel1.ModelInput, T3ML.MLModel1.ModelOutput>(trainedModel);

                    //List<T3ML.MLModel1.ModelOutput> predictions = new List<T3ML.MLModel1.ModelOutput>();
                    //for (int p = 0; p < inputs.Count; p++)
                    //{
                    //    var prediction = predEngine.Predict(inputs[p]);
                    //    predictions.Add(prediction);
                    //}

                    IDataView testData = mlContext.Data.LoadFromEnumerable<T3ML.MLModel1.ModelInput>(inputs);

                    // Load trained model
                    ITransformer trainedModel = mlContext.Model.Load(_MLModelFiles[i].FullName, out modelSchema);

                    var transformedData = trainedModel.Transform(testData);

                    var testMetrics = mlContext.MulticlassClassification.Evaluate(transformedData, "GameResultCode", "Score", "PredictedLabel");

                    if (testMetrics != null)
                    {
                        metrics.Add(_MLModelFiles[i].Name, testMetrics);
                    }
                }

                var maxMicroAccuracy = metrics.Max(x => x.Value.MicroAccuracy);
                var bestModels =
                    metrics
                    .Where(x => x.Value.MicroAccuracy == maxMicroAccuracy)
                    .ToList();
                
                foreach (var model in bestModels)
                {
                    Console.WriteLine(model.Key);
                }
            }
        }
    }
}