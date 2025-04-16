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
            //Ascii characters used to represent different brightness levels
            string shades = "@#S%?*+;:,. ";
            List<string> lines = new List<string>();

            //convert the image to ASCII
            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = 60;
                int height = (int)(image.Height / (double)image.Width * width * 0.4);
                image.Mutate(x => x.Resize(width, height));

                // Loop through each pixel in the image
                for (int y = 0; y < image.Height; y++)
                {
                    // Create a new line for each row of pixels
                    StringBuilder line = new StringBuilder();
                    for (int x = 0; x < image.Width; x++)
                    {
                        // Get the pixel color and calculate its brightness
                        var pixel = image[x, y];
                        int brightness = (pixel.R + pixel.G + pixel.B) / 3;
                        int index = brightness * (shades.Length - 1) / 255;
                        line.Append(shades[index]);
                    }
                    // Add the line to the list of lines
                    lines.Add(line.ToString());
                }
            }

            // Remove empty lines from the start and end of the list
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0].Replace(" ", "").Replace(".", "")))
                lines.RemoveAt(0);
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1].Replace(" ", "").Replace(".", "")))
                lines.RemoveAt(lines.Count - 1);

            // Add a border around the ASCII art
            return string.Join(Environment.NewLine, lines);
        }
    }
}
