using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
namespace ImageBorderTool
{
    public class App
    {
        public void Run(string[] imagePaths)
        {
            foreach (var imagePath in imagePaths)
            {
                string directory = Path.GetDirectoryName(imagePath);
                string borderToolDirectory = Path.Combine(directory, "BorderTool");
                Directory.CreateDirectory(borderToolDirectory);

                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(imagePath);
                string extension = Path.GetExtension(imagePath);
                string fullSizeOutputPath = Path.Combine(borderToolDirectory, $"{filenameWithoutExtension}_FullSize{extension}");
                string webSizeOutputPath = Path.Combine(borderToolDirectory, $"{filenameWithoutExtension}_WebSize{extension}");

                ResizeImageToSquare(imagePath, fullSizeOutputPath, null);
                ResizeImageToSquare(imagePath, webSizeOutputPath, 600);
            }
        }

        private void ResizeImageToSquare(string inputImagePath, string outputImagePath, int? outputSize)
        {
            using (Image image = Image.Load(inputImagePath))
            {
                int size = Math.Max(image.Width, image.Height);

                using (Image<Rgba32> squareImage = new Image<Rgba32>(Configuration.Default, size, size, Color.White))
                {
                    int x = (size - image.Width) / 2;
                    int y = (size - image.Height) / 2;

                    squareImage.Mutate(ctx => ctx.DrawImage(image, new Point(x, y), 1f));
                    if (outputSize != null)
                    {
                        Size newSize = new((int)outputSize, (int)outputSize);
                        squareImage.Mutate(ctx => ctx.Resize(newSize));
                    }

                    squareImage.Save(outputImagePath);
                }
            }
        }
    }
}
