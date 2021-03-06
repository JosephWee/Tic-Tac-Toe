﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace TicTacToeML
{
    public partial class MLModel1
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
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
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(@"GameResultCode", @"GameResultCode"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.LightGbm(new LightGbmMulticlassTrainer.Options(){NumberOfLeaves=56,MinimumExampleCountPerLeaf=34,NumberOfIterations=471,MaximumBinCountPerFeature=10,LearningRate=0.319428290683458F,LabelColumnName=@"GameResultCode",FeatureColumnName=@"Features",Booster=new GradientBooster.Options(){SubsampleFraction=0.977056318807547F,FeatureFraction=0.987876727278169F,L1Regularization=3.80524208106251E-06F,L2Regularization=1.01311440576802E-06F}}))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(@"PredictedLabel", @"PredictedLabel"));

            return pipeline;
        }
    }
}
