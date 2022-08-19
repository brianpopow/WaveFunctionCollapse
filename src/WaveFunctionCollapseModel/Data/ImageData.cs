namespace WaveFunctionCollapseModel.Data
{
    public class ImageData
    {
        public ImageData(string filePath)
        {
            this.FilePath = filePath;
            this.Transform = ImageTransform.None;

            var (pixelData, sx, sy) = BitmapHelper.LoadBitmap(filePath);
            this.PixelData = pixelData;
            this.Width = sx;
            this.Height = sy;
        }

        public ImageData(ImageData other, ImageTransform transform)
        {
            this.FilePath = other.FilePath;
            this.Transform = transform;
            this.Width = other.Width;
            this.Height = other.Height;

            switch (transform)
            {
                case ImageTransform.Rotate:
                    this.PixelData = Rotate(other.PixelData, other.Width);
                    break;
                case ImageTransform.Reflect:
                    this.PixelData = Reflect(other.PixelData, other.Width);
                    break;
                default:
                    throw new ArgumentException("Invalid transform: " + transform);
            }
        }

        public string FilePath { get; }

        public ImageTransform Transform { get; }

        public int[] PixelData { get; }

        public int Width { get; }

        private int Height { get; }

        private static int[] Tile(Func<int, int, int> f, int size)
        {
            int[] result = new int[size * size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    result[x + (y * size)] = f(x, y);
                }
            }

            return result;
        }

        private static int[] Rotate(int[] array, int size) => Tile((x, y) => array[size - 1 - y + (x * size)], size);

        private static int[] Reflect(int[] array, int size) => Tile((x, y) => array[size - 1 - x + (y * size)], size);
    }
}
