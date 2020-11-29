using System.Drawing;

namespace NeuralNetwork.Core.ImageProcessing
{
    public struct ConvolutionParams
    {
        public Bitmap Source { get; set; }
        public double[,] FilterMatrix { get; set; }
        public double Factor { get; set; }
        public int Bias { get; set; }
        public bool GrayScaleMode { get; set; }

        public ConvolutionParams(Bitmap source, double[,] filterMatrix)
        {
            Source = source;
            FilterMatrix = filterMatrix;
            Factor = 1;
            Bias = 0;
            GrayScaleMode = true;
        }

        public ConvolutionParams(Bitmap source, double[,] filterMatrix, double factor, int bias, bool grayScaleMode)
        {
            Source = source;
            FilterMatrix = filterMatrix;
            Factor = factor;
            Bias = bias;
            GrayScaleMode = grayScaleMode;
        }
    }
}