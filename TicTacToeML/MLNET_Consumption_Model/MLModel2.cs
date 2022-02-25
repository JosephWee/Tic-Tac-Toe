using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.ML
{
    public partial class MLModel2
    {
        public static bool SetMLNetModelPath(string MLModelPath)
        {
            if (!File.Exists(MLModelPath))
                return false;

            FileInfo fileInfo = new FileInfo(MLModelPath);
            if (fileInfo.Extension != ".zip")
                return false;
            
            MLNetModelPath = MLModelPath;
            
            return true;
        }

        public static ModelOutput Predict(ModelInput input, string MLModelPath)
        {
            if (SetMLNetModelPath(MLModelPath))
                return Predict(input);
            else
                throw new FileNotFoundException("Unable to find MLNETModel");
        }
    }
}
