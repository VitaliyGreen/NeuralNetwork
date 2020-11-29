using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork.Core.ImageProcessing;

namespace NeuralNetwork.Test
{
    [TestClass]
    public class ImageProcessorTest
    {
        private readonly string _imagePath = @"E:\Studing\Магістратура\НМШІ\Курсова\letters\letters2\02_62.png";
        private ImageProcessor _imageProcessor;
        private Bitmap _sourceImage;

        [TestInitialize]
        public void TestInit()
        {
            _imageProcessor = new ImageProcessor();
            _sourceImage = new Bitmap(_imagePath);
        }

        [TestMethod]
        public void BrightnessVectorIsNotEmpty()
        {
            float[] result = null;
            result = _imageProcessor.GetBrightnessVector(_sourceImage);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void BrightnessVectorHasNonzeroValue()
        {
            float[] brightness = _imageProcessor.GetBrightnessVector(_sourceImage);
            
            int nonzeroBrightnessCount = (int)(brightness.Length * 0.1);
            int actualNonzeroCount = 0;
            bool hasTenPercentNonzeroPixels = false;

            foreach (float brightnessValue in brightness)
            {
                if (brightnessValue > 0)
                    actualNonzeroCount++;

                if (actualNonzeroCount >= nonzeroBrightnessCount)
                {
                    hasTenPercentNonzeroPixels = true;
                    break;
                }
            }

            Assert.IsTrue(hasTenPercentNonzeroPixels);
        }
    }
}