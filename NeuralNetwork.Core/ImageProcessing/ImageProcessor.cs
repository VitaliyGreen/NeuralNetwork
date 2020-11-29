using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace NeuralNetwork.Core.ImageProcessing
{
    /// <summary>
    /// Processes image file
    /// </summary>
    public class ImageProcessor
    {
        /// <summary>
        /// Receives brightness of each pixel
        /// </summary>
        /// <param name="bmp">Path to the image file</param>
        /// <returns>Matrix of brightness for each pixel</returns>
        public float[] GetBrightnessVector(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;

            List<float> imagePixels = new List<float>(height * width);

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    float imagePixelBrightness = bmp.GetPixel(row, column).GetBrightness();
                    imagePixels.Add(imagePixelBrightness);
                }
            }

            return imagePixels.ToArray();
        }
    }
}