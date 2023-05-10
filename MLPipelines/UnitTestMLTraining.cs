using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.FastTree;
using TicTacToe.ML;

namespace MLPipelines
{
    public class UnitTestMLTraining
    {
        bool skipTest = false;
        private static FileInfo _MLModelFileInfo = null;
        private static string _MLTrainingDataPath = string.Empty;
        private static string _MLTrainingDataQueuePath = string.Empty;
        private static string _MLTrainingDataProcessedPath = string.Empty;
        private static string _MLTrainingDataRejectedPath = string.Empty;
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

            _MLModelFileInfo = new FileInfo(MLModelPath);

            Assert.IsTrue(_MLModelFileInfo.Exists);
            Assert.IsTrue(_MLModelFileInfo.Extension == ".zip");

            _MLTrainingDataPath =
                TestContext
                .Parameters
                .Get("MLTrainingDataPath", string.Empty)
                .Replace("$(SolutionDir)", solutionDir)
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar); ;

            Assert.IsTrue(Directory.Exists(_MLTrainingDataPath));

            _MLTrainingDataQueuePath =
                Path.Combine(_MLTrainingDataPath, "Queue");

            Assert.IsTrue(Directory.Exists(_MLTrainingDataQueuePath));

            inputFiles =
                new DirectoryInfo(_MLTrainingDataQueuePath)
                .GetFiles("*.csv")
                .ToList();

            if (!inputFiles.Any())
            {
                skipTest = true;
                return;
            }

            _MLTrainingDataProcessedPath =
                Path.Combine(_MLTrainingDataPath, "Processed");

            if (!Directory.Exists(_MLTrainingDataProcessedPath))
                Directory.CreateDirectory(_MLTrainingDataProcessedPath);

            _MLTrainingDataRejectedPath =
                Path.Combine(_MLTrainingDataPath, "Rejected");

            if (!Directory.Exists(_MLTrainingDataRejectedPath))
                Directory.CreateDirectory(_MLTrainingDataRejectedPath);
        }

        [Test]
        public void ProcessTrainingData()
        {
            if (skipTest)
                return;

            MLContext mlContext = new MLContext();
            DataViewSchema modelSchema;

            // Load Pipeline
            IEstimator<ITransformer> pipeline =
                mlContext
                .Transforms
                .ReplaceMissingValues(
                    new[] {
                            new InputOutputColumnPair(@"MoveNumber", @"MoveNumber"),
                            new InputOutputColumnPair(@"Cell0", @"Cell0"),
                            new InputOutputColumnPair(@"Cell1", @"Cell1"),
                            new InputOutputColumnPair(@"Cell2", @"Cell2"),
                            new InputOutputColumnPair(@"Cell3", @"Cell3"),
                            new InputOutputColumnPair(@"Cell4", @"Cell4"),
                            new InputOutputColumnPair(@"Cell5", @"Cell5"),
                            new InputOutputColumnPair(@"Cell6", @"Cell6"),
                            new InputOutputColumnPair(@"Cell7", @"Cell7"),
                            new InputOutputColumnPair(@"Cell8", @"Cell8")
                    })
                    .Append(
                        mlContext
                        .Transforms
                        .Concatenate(
                            @"Features",
                            new[] {
                                    @"MoveNumber",
                                    @"Cell0",
                                    @"Cell1",
                                    @"Cell2",
                                    @"Cell3",
                                    @"Cell4",
                                    @"Cell5",
                                    @"Cell6",
                                    @"Cell7",
                                    @"Cell8"
                            })
                    )
                    .Append(
                        mlContext
                        .Transforms
                        .Conversion
                        .MapValueToKey(
                            outputColumnName: @"GameResultCode",
                            inputColumnName: @"GameResultCode")
                    )
                    .Append(
                        mlContext
                        .MulticlassClassification
                        .Trainers
                        .OneVersusAll(
                            binaryEstimator:
                                mlContext
                                .BinaryClassification
                                .Trainers
                                .FastTree(
                                    new FastTreeBinaryTrainer.Options()
                                    {
                                        NumberOfLeaves = 19,
                                        MinimumExampleCountPerLeaf = 22,
                                        NumberOfTrees = 4,
                                        MaximumBinCountPerFeature = 491,
                                        FeatureFraction = 0.664945832649753,
                                        LearningRate = 0.999999776672986,
                                        LabelColumnName = @"GameResultCode",
                                        FeatureColumnName = @"Features"
                                    }),
                            labelColumnName: @"GameResultCode")
                    )
                    .Append(
                        mlContext
                        .Transforms
                        .Conversion
                        .MapKeyToValue(
                            outputColumnName: @"PredictedLabel",
                            inputColumnName: @"PredictedLabel")
                    );

            // Load New Data
            List<MLModel1.ModelInput> inputs = new List<MLModel1.ModelInput>();
            List<FileInfo> processedFiles = new List<FileInfo>();
            List<FileInfo> rejectedFiles = new List<FileInfo>();
            for (int i = 0; i < inputFiles.Count; i++)
            {
                var inputFile = inputFiles[i];
                
                try
                {
                    bool fileHasError = false;
                    List<MLModel1.ModelInput> fileInputs = new List<MLModel1.ModelInput>();

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

                            MLModel1.ModelInput newRow = new MLModel1.ModelInput()
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

            // Train new ML Model
            IDataView newData = mlContext.Data.LoadFromEnumerable<MLModel1.ModelInput>(inputs);

            var newModel = pipeline.Fit(newData);

            Assert.IsNotNull(_MLModelFileInfo.Directory);

            // Save New ML Model
            var newMLModelFilePath =
                Path.Combine(
                    _MLModelFileInfo.Directory.FullName,
                    $"MLModel1_{DateTime.UtcNow.ToString("yyyy_MM_dd_HHmmss")}.zip");

            mlContext.Model.Save(newModel, newData.Schema, newMLModelFilePath);

            //Do Clean Up
            for (int i = 0; i < processedFiles.Count; i++)
            {
                var processedFile = processedFiles[i];
                var fileDestination =
                    Path.Combine(
                        _MLTrainingDataProcessedPath,
                        processedFile.Name );
                File.Move(processedFile.FullName, fileDestination);
            }

            for (int i = 0; i < rejectedFiles.Count; i++)
            {
                var rejectedFile = rejectedFiles[i];
                var fileDestination =
                    Path.Combine(
                        _MLTrainingDataRejectedPath,
                        rejectedFile.Name);
                File.Move(rejectedFile.FullName, fileDestination);
            }
        }
    }
}