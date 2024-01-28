namespace ImageBorderTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var app = new App();
                app.Run(args);
            }
            else
            {
                Console.WriteLine("No images detected.");

                Console.Write("\n\nPlease open the app with one or more images.\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
