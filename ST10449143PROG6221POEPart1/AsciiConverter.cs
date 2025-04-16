using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ST10449143PROG6221POEPart1
{
    public static class AsciiConverter
    {
        public static string ConvertToAsciiSimple(string imagePath)
        {
            string shades = "@#S%?*+;:,. ";
            List<string> lines = new List<string>();

            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = 60;
                int height = (int)(image.Height / (double)image.Width * width * 0.4);
                image.Mutate(x => x.Resize(width, height));

                for (int y = 0; y < image.Height; y++)
                {
                    StringBuilder line = new StringBuilder();
                    for (int x = 0; x < image.Width; x++)
                    {
                        var pixel = image[x, y];
                        int brightness = (pixel.R + pixel.G + pixel.B) / 3;
                        int index = brightness * (shades.Length - 1) / 255;
                        line.Append(shades[index]);
                    }
                    lines.Add(line.ToString());
                }
            }

            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0].Replace(" ", "").Replace(".", "")))
                lines.RemoveAt(0);
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1].Replace(" ", "").Replace(".", "")))
                lines.RemoveAt(lines.Count - 1);

            return string.Join(Environment.NewLine, lines);
        }
    }
}
