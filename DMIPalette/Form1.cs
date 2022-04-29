using DMISharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DMIPalette
{
    public partial class Form1 : Form
    {
        private DMIFile _dmiFile;
        private Bitmap _sourceBitmap;
        private Bitmap _paletteBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            textPath.Text = openFileDialog1.FileName;

            _dmiFile = new(openFileDialog1.FileName);

            List<Rgba32> palette = new();
            int totalPixels = 0;

            foreach(DMIState state in _dmiFile.States)
            {
                for(int i = 0; i < state.Frames; i++)
                {
                    foreach (StateDirection dir in Enum.GetValues(typeof(StateDirection)))
                    {
                        try
                        {
                            Image<Rgba32> image = state.GetFrame(i);
                            foreach (Rgba32 pixel in GetPixels(image))
                            {
                                totalPixels++;
                                if (!palette.Contains(pixel))
                                    palette.Add(pixel);
                            }
                        }
                        catch { }
                    }
                }
            }

            _sourceBitmap = Draw(palette);
            pictureSrcPalette.Image = ResizeImage(_sourceBitmap, pictureSrcPalette.Width, pictureSrcPalette.Height);
        }

        private IEnumerable<Rgba32> GetPixels(Image<Rgba32> image)
        {
            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                    yield return image[x, y];
        }

        private Bitmap Draw(List<Rgba32> palette)
        {
            int side = (int)Math.Ceiling(Math.Sqrt(palette.Count));
            var bitmap = new Bitmap(side, side);

            int i = 0;
            for (int y = 0; y < side; y++)
            {
                for (int x = 0; x < side; x++)
                {
                    if (i == palette.Count)
                        break;

                    var color = System.Drawing.Color.FromArgb(palette[i].A, palette[i].R, palette[i].G, palette[i].B);
                    bitmap.SetPixel(x, y, color);
                    
                    i++;
                }
            }

            // bitmap.Save("1.png", ImageFormat.Png);
            return bitmap;
        }

        public static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void pictureNewPalette_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK)
                return;

            _paletteBitmap = (Bitmap)System.Drawing.Image.FromFile(openFileDialog2.FileName);
            pictureNewPalette.BackgroundImage = null;
            pictureNewPalette.Image = ResizeImage(_paletteBitmap, pictureNewPalette.Width, pictureNewPalette.Height);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (_sourceBitmap == null || _paletteBitmap == null)
                return;

            if (_paletteBitmap.Width != _sourceBitmap.Width || _paletteBitmap.Height != _sourceBitmap.Height)
            {
                MessageBox.Show($"Dimensions of selected palette and source is not equal.\nW: {_sourceBitmap.Width} != {_paletteBitmap.Width} | H: {_sourceBitmap.Height} != {_paletteBitmap.Height}.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DMIState state in _dmiFile.States)
            {
                for (int i = 0; i < state.Frames; i++)
                {
                    var pairs = BuildPaletteDictionary();
                    foreach (StateDirection dir in Enum.GetValues(typeof(StateDirection)))
                    {
                        try
                        {
                            Image<Rgba32> image = state.GetFrame(dir, i);

                            Bitmap swapped;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                image.SaveAsBmp(ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                swapped = SwapPalette(new Bitmap(ms), pairs);
                            }

                            using (MemoryStream ms = new MemoryStream())
                            {
                                swapped.Save(ms, ImageFormat.Png);
                                ms.Seek(0, SeekOrigin.Begin);

                                state.SetFrame(SixLabors.ImageSharp.Image.Load<Rgba32>(ms), dir, i);
                            }
                        }
                        catch { }
                    }
                }
            }

            _dmiFile.Save("out.dmi");
        }

        private Bitmap SwapPalette(Bitmap image, Dictionary<Rgba32, Rgba32> pairs)
        {
            for(int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    System.Drawing.Color pixel = image.GetPixel(x, y);
                    Rgba32 srcColor = new Rgba32(pixel.R, pixel.G, pixel.B, pixel.A);

                    var newColor = System.Drawing.Color.FromArgb(pairs[srcColor].A, pairs[srcColor].R, pairs[srcColor].G, pairs[srcColor].B);
                    image.SetPixel(x, y, newColor);
                }
            }

            return image;
        }

        private Dictionary<Rgba32, Rgba32> BuildPaletteDictionary()
        {
            Dictionary<Rgba32, Rgba32> pairs = new();
            for (int y = 0; y < _paletteBitmap.Height; y++)
            {
                for (int x = 0; x < _paletteBitmap.Width; x++)
                {
                    System.Drawing.Color source = _sourceBitmap.GetPixel(x, y);
                    System.Drawing.Color palette = _paletteBitmap.GetPixel(x, y);

                    // Rgba32(192, 192, 192, 255)
                    if (source.R == 192 && source.G == 192 && source.B == 192)
                        source = System.Drawing.Color.FromArgb(255, source.R, source.G, source.B);
                    
                    Rgba32 sourceRgba = new Rgba32(source.R, source.G, source.B, source.A);
                    if (!pairs.ContainsKey(sourceRgba))
                        pairs.Add(sourceRgba, new Rgba32(palette.R, palette.G, palette.B, source.A));
                }
            }

            return pairs;
        }

        private void buttonSavePalette_Click(object sender, EventArgs e)
        {
            if (_sourceBitmap == null)
                return;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            _sourceBitmap.Save(saveFileDialog1.FileName);
        }
    }
}