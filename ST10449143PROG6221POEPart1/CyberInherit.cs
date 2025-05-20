using System;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ST10449143PROG6221POEPart1
{
    // Base class providing shared features
    class CyberInherit
    {
        //Method to print text with a delay
        public void PrintWithDelay(string text, int delay = 13, ConsoleColor? color = null)
        {
            if (color.HasValue)
                Console.ForegroundColor = color.Value;

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();

            if (color.HasValue)
                Console.ResetColor();
        }

        //Method to play the greeting audio
        public void PlayGreetingAudio(string audioFilePath)
        {
            if (File.Exists(audioFilePath))
            {
#pragma warning disable CA1416
                try
                {
                    using (SoundPlayer player = new SoundPlayer(audioFilePath))
                    {
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    PrintWithDelay($"Error playing audio: {ex.Message}\n", 13, ConsoleColor.Red);
                }
#pragma warning restore CA1416
            }
            else
            {
                PrintWithDelay("Audio file not found.\n");
            }
        }

        //Method to show the ASCII art title
        public void ShowAsciiTitle(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                PrintWithDelay("ASCII image not found.\n");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            string asciiArt = AsciiConverter.ConvertToAsciiSimple(imagePath);
            string[] lines = asciiArt.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                int consoleWidth = Console.WindowWidth;
                int padding = Math.Max((consoleWidth - line.Length) / 2, 0);
                Console.WriteLine(new string(' ', padding) + line);
            }

            Console.ResetColor();

            PrintWithDelay("===============================================", 13, ConsoleColor.Magenta);
            PrintWithDelay("        Welcome to the Cybersecurity Bot       ", 13, ConsoleColor.Magenta);
            PrintWithDelay("===============================================\n", 13, ConsoleColor.Magenta);
        }
    }
}