using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace NeuralNetwork.Core.ImageProcessing
{
    public static class Convolution
    {
        public static class Matrix
        {
            public static double[,] Laplacian3x3
            {
                get
                {
                    return new double[,]
                    {   
                        { -1, -1, -1, },
                        { -1,  8, -1, },
                        { -1, -1, -1, },
                    };
                }
            }

            public static double[,] Laplacian5x5
            {
                get
                {
                    return new double[,]
                    { 
                        { -1, -1, -1, -1, -1, },
                        { -1, -1, -1, -1, -1, },
                        { -1, -1, 24, -1, -1, },
                        { -1, -1, -1, -1, -1, },
                        { -1, -1, -1, -1, -1  }
                    };
                }
            }

            public static double[,] LaplacianOfGaussian
            {
                get
                {
                    return new double[,]
                    { 
                        {  0,  0, -1,  0,  0 },
                        {  0, -1, -2, -1,  0 },
                        { -1, -2, 16, -2, -1 },
                        {  0, -1, -2, -1,  0 },
                        {  0,  0, -1,  0,  0 }
                    };
                }
            }

            public static double[,] Gaussian3x3
            {
                get
                {
                    return new double[,]
                    { 
                        { 1, 2, 1, },
                        { 2, 4, 2, },
                        { 1, 2, 1, }
                    };
                }
            }

            public static double[,] Edge3x3
            {
                get
                {
                    return new double[,]
                    {
                        {0, -1, 0 },
                        {-1, 4, -1 },
                        {0, -1, 0 },
                    };
                }
            }
        }

        public static Bitmap ConvolutionFilter(ConvolutionParams convolutionParams)
        {
            BitmapData sourceData =
                convolutionParams.Source.LockBits(new Rectangle(0, 0,
                        convolutionParams.Source.Width, convolutionParams.Source.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];
            
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                pixelBuffer.Length);
            
            convolutionParams.Source.UnlockBits(sourceData);

            if (convolutionParams.GrayScaleMode == true)
            {
                MakeGray(ref pixelBuffer);
            }
            
            int filterWidth = convolutionParams.FilterMatrix.GetLength(1);
            int filterOffset = (filterWidth - 1) / 2;
            
            for (int offsetY = filterOffset; offsetY < convolutionParams.Source.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < convolutionParams.Source.Width - filterOffset; offsetX++)
                {
                    double blue = 0;
                    double green = 0;
                    double red = 0;
                    
                    int byteOffset = offsetY * sourceData.Stride + 
                                     offsetX * 4;
                    
                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            int calcOffset = byteOffset +
                                             (filterX * 4) +
                                             (filterY * sourceData.Stride);
                            
                            blue += (double)(pixelBuffer[calcOffset]) *
                                    convolutionParams.FilterMatrix[filterY + filterOffset,
                                        filterX + filterOffset];
                            
                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     convolutionParams.FilterMatrix[filterY + filterOffset,
                                         filterX + filterOffset];
                            
                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   convolutionParams.FilterMatrix[filterY + filterOffset,
                                       filterX + filterOffset];
                        }
                    }
                    
                    blue = convolutionParams.Factor * blue + convolutionParams.Bias;
                    green = convolutionParams.Factor * green + convolutionParams.Bias;
                    red = convolutionParams.Factor * red + convolutionParams.Bias;
                    
                    if (blue > 255)
                    {
                        blue = 255;
                    }
                    else if (blue < 0)
                    {
                        blue = 0;
                    }
                    
                    if (green > 255)
                    {
                        green = 255;
                    }
                    else if (green < 0)
                    {
                        green = 0;
                    }
                    
                    if (red > 255)
                    {
                        red = 255;
                    }
                    else if (red < 0)
                    {
                        red = 0;
                    }
                    
                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(convolutionParams.Source.Width,
                convolutionParams.Source.Height);
            
            BitmapData resultData =
                resultBitmap.LockBits(new Rectangle(0, 0,
                        resultBitmap.Width, resultBitmap.Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);
            
            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);
            
            return resultBitmap;
        }

        private static void MakeGray(ref byte[] pixelBuffer)
        {
            for (int offset = 0; offset < pixelBuffer.Length; offset += 4)
            {
                float rgb = pixelBuffer[offset] * 0.11f;
                rgb += pixelBuffer[offset + 1] * 0.59f;
                rgb += pixelBuffer[offset + 2] * 0.3f;

                pixelBuffer[offset] = (byte) rgb;
                pixelBuffer[offset + 1] = pixelBuffer[offset];
                pixelBuffer[offset + 2] = pixelBuffer[offset];
                pixelBuffer[offset + 3] = 255;
            }
        }
    }
}