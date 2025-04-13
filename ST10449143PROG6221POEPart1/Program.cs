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
            string audioFilePath = @"C:\Users\lab_services_student\Desktop\PROG6221POEPart1\ST10449143PROG6221POEPart1\greeting.wav";

            Console.Title = "Cybersecurity Awareness Assistant";

            PlayGreetingAudio(audioFilePath);
            ShowAsciiTitle(imagePath);
            
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine()?.Trim();

            Console.Clear();
            ShowAsciiTitle(imagePath);
            LaunchChatbot(userName);
        }

        static void ShowAsciiTitle(string imagePath)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===============================================");
            Console.WriteLine("        Welcome to the Cybersecurity Bot       ");
            Console.WriteLine("===============================================\n");
            Console.ResetColor();

            if (!File.Exists(imagePath))
            {
                Console.WriteLine("ASCII image not found.\n");
                return;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ConvertToAsciiSimple(imagePath));
            Console.ResetColor();
        }

        static string ConvertToAsciiSimple(string imagePath)
        {
            string shades = "@#S%?*+;:,. ";
            List<string> lines = new List<string>();

            using (var image = Image.Load<Rgba32>(imagePath))
            {
                int width = 80;
                int height = (int)(image.Height / (double)image.Width * width * 0.5);
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

        static void PlayGreetingAudio(string audioFilePath)
        {
            if (File.Exists(audioFilePath))
            {
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    Console.WriteLine("Playing voice greeting...\n");
                    player.PlaySync();
                }
            }
            else
            {
                Console.WriteLine("Audio file not found.\n");
            }
        }

        static void LaunchChatbot(string name)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Hi {name}, welcome to the Cybersecurity Awareness Assistant!");
            Console.WriteLine("I'm your virtual assistant to help you stay safe online.\n");

            }
        }
    }
