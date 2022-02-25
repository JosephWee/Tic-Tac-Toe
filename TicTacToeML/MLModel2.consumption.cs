﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace TicTacToeML
{
    public partial class MLModel2
    {
        /// <summary>
        /// model input class for MLModel2.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"MoveNumber")]
            public float MoveNumber { get; set; }

            [ColumnName(@"Cell0")]
            public float Cell0 { get; set; }

            [ColumnName(@"Cell1")]
            public float Cell1 { get; set; }

            [ColumnName(@"Cell2")]
            public float Cell2 { get; set; }

            [ColumnName(@"Cell3")]
            public float Cell3 { get; set; }

            [ColumnName(@"Cell4")]
            public float Cell4 { get; set; }

            [ColumnName(@"Cell5")]
            public float Cell5 { get; set; }

            [ColumnName(@"Cell6")]
            public float Cell6 { get; set; }

            [ColumnName(@"Cell7")]
            public float Cell7 { get; set; }

            [ColumnName(@"Cell8")]
            public float Cell8 { get; set; }

            [ColumnName(@"GameResultCode")]
            public float GameResultCode { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLModel2.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName("PredictedLabel")]
            public float Prediction { get; set; }

            public float[] Score { get; set; }
        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("MLModel2.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
