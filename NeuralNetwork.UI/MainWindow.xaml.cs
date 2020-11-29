using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using NeuralNetwork.Core.ImageProcessing;

namespace NeuralNetwork.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string path = @"E:\Studing\Магістратура\НМШІ\Курсова\letters\letters2\02_52.png";
            //string path = @"C:\Users\Vitalii Rozbyiholova\Downloads\8-pink-butterfly-png-image-butterflies.png";
            Bitmap imageBitmap = new Bitmap(path);
            ConvolutionParams convolutionParams = new ConvolutionParams(imageBitmap, Convolution.Matrix.Edge3x3);
            Bitmap outBitmap = Convolution.ConvolutionFilter(convolutionParams);
            OutImage.Source = BitmapToImageSource(outBitmap);
        }
        
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
