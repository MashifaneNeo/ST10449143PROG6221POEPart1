using System;
using System.IO;
using System.Media;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ST10449143PROG6221POEPart1
{
    class Program
    {
        static void Main(string[] args)
        {
            string imagePath = @"C:\Users\lab_services_student\Desktop\PROG6221POEPart1\ST10449143PROG6221POEPart1\cyber.jpg";
            ShowAsciiTitle();

            PlayGreetingAudio();

            LaunchChatbot();
        }

        static void ShowAsciiTitle()
        {
            string imagePath = @"C:\Users\lab_services_student\Desktop\PROG6221POEPart1\ST10449143PROG6221POEPart1\cyber.jpg";

            if (!File.Exists(imagePath))
            {
                Console.WriteLine("ASCII image not found.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ConvertToAsciiSimple(imagePath));
            Console.ResetColor();
        }

        static string ConvertToAsciiSimple(string imagePath)
        {
            string shades = "@#S%?*+;:,. ";
            StringBuilder sb = new StringBuilder();

            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = 80;
                int height = (int)(image.Height / (double)image.Width * width * 0.5);
                image.Mutate(x => x.Resize(width, height));

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var pixel = image[x, y];
                        int brightness = (pixel.R + pixel.G + pixel.B) / 3;
                        int index = brightness * (shades.Length - 1) / 255;
                        sb.Append(shades[index]);
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        static void PlayGreetingAudio()
        {
            // Path to the audio file
            string audioFilePath = @"C:\Users\lab_services_student\Desktop\PROG6221POEPart1\ST10449143PROG6221POEPart1\greeting.wav";

            // Check if the file exists
            if (File.Exists(audioFilePath))
            {
                // Create a SoundPlayer object and play the sound as a voice greeting
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    Console.WriteLine("Playing voice greeting...\n");
                    player.PlaySync(); // Play the sound synchronously
                }
            }
            else
            {
                Console.WriteLine("Audio file not found.\n");
            }
        }

        static void LaunchChatbot()
        {
            // Simulate a chatbot launch
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Hello! Welcome to the Cybersecurity Awareness Assistant.");
            Console.WriteLine("I'm here to help you stay safe online. Let's begin!\n");
            Console.ResetColor();
        }
    }
}