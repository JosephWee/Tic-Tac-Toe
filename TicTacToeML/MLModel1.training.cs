﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace TicTacToeML
{
    public partial class MLModel1
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"MoveNumber", @"MoveNumber"),new InputOutputColumnPair(@"Cell0", @"Cell0"),new InputOutputColumnPair(@"Cell1", @"Cell1"),new InputOutputColumnPair(@"Cell2", @"Cell2"),new InputOutputColumnPair(@"Cell3", @"Cell3"),new InputOutputColumnPair(@"Cell4", @"Cell4"),new InputOutputColumnPair(@"Cell5", @"Cell5"),new InputOutputColumnPair(@"Cell6", @"Cell6"),new InputOutputColumnPair(@"Cell7", @"Cell7"),new InputOutputColumnPair(@"Cell8", @"Cell8")})      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"MoveNumber",@"Cell0",@"Cell1",@"Cell2",@"Cell3",@"Cell4",@"Cell5",@"Cell6",@"Cell7",@"Cell8"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"GameResultCode",inputColumnName:@"GameResultCode"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator:mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options(){NumberOfLeaves=19,MinimumExampleCountPerLeaf=22,NumberOfTrees=4,MaximumBinCountPerFeature=491,FeatureFraction=0.664945832649753,LearningRate=0.999999776672986,LabelColumnName=@"GameResultCode",FeatureColumnName=@"Features"}),labelColumnName: @"GameResultCode"))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName:@"PredictedLabel",inputColumnName:@"PredictedLabel"));

            return pipeline;
        }
    }
}
