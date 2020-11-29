using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork.Core.ImageProcessing;

namespace NeuralNetwork.Test
{
    [TestClass]
    public class ConvolutionTest
    {
        private readonly string _imagePath =
            @"C:\Users\Vitalii Rozbyiholova\Downloads\8-pink-butterfly-png-image-butterflies.png";

        private Bitmap _sourceImage;
        private ConvolutionParams _convolutionParams;

        [TestInitialize]
        public void TestInit()
        {
            _sourceImage = new Bitmap(_imagePath);
            _convolutionParams = new ConvolutionParams(_sourceImage, Convolution.Matrix.Edge3x3);
        }

        [TestMethod]
        public void ConvolutionDoesNotChangeSize()
        {
            Size originalSize = _sourceImage.Size;

            Bitmap newImage = Convolution.ConvolutionFilter(_convolutionParams);
            Size actualSize = newImage.Size;

            Assert.AreEqual(originalSize, actualSize);
        }

        [TestMethod]
        public void ConvolutionDecreaseImageBrightness()
        {
            int amount = _sourceImage.Width * _sourceImage.Height / 10;
            ICollection<Tuple<int, int>> randomCoordinates =
                GetRandomCoordinates(_sourceImage.Height, _sourceImage.Width, amount);
            
            float[] originalBrightness = GetBrightnessByCoordinates(randomCoordinates, _sourceImage);
            int originalBlackCount = originalBrightness.Count(brightness => brightness == 0f);

            Bitmap newImage = Convolution.ConvolutionFilter(_convolutionParams);

            float[] actualBrightness = GetBrightnessByCoordinates(randomCoordinates, newImage);
            int actualBlackCount = actualBrightness.Count(brightness => brightness == 0f);

            Assert.IsTrue(actualBlackCount > originalBlackCount);
        }

        private float[] GetBrightnessByCoordinates(ICollection<Tuple<int, int>> coordinates, Bitmap image)
        {
            float[] brightness = new float[coordinates.Count];

            for (int i = 0; i < coordinates.Count; i++)
            {
                Tuple<int, int> coordinate = coordinates.ElementAt(i);
                brightness[i] = image.GetPixel(coordinate.Item1, coordinate.Item2).GetBrightness();
            }

            return brightness;
        }

        private ICollection<Tuple<int, int>> GetRandomCoordinates(int height, int width, int pixelsAmount)
        {
            Random random = new Random();
            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();

            for (int i = 0; i < pixelsAmount; i++)
            {
                int row = random.Next(0, height);
                int column = random.Next(0, width);

                coordinates.Add(new Tuple<int, int>(column, row));
            }

            return coordinates;
        }
    }
}