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
    class Program 
    {
        static void Main(string[] args)
        {
            string imagePath = @"C:\\Users\\lab_services_student\\Desktop\\PROG6221POEPart1\\ST10449143PROG6221POEPart1\\cyber.jpg";
            string audioFilePath = @"C:\\Users\\lab_services_student\\Desktop\\PROG6221POEPart1\\ST10449143PROG6221POEPart1\\greeting.wav";

            Console.Title = "Cybersecurity Awareness Assistant";

            PlayGreetingAudio(audioFilePath);
            ShowAsciiTitle(imagePath);

            string userName = "";
            while (string.IsNullOrWhiteSpace(userName))
            {
                PrintWithDelay("Please enter your name: ", 13, ConsoleColor.DarkYellow);
                userName = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userName))
                {
                    PrintWithDelay("Name cannot be empty. Please try again.", 13, ConsoleColor.Red);
                }
            }

            Console.Clear();
            ShowAsciiTitle(imagePath);

            PrintWithDelay("------------------------------------------------", 13, ConsoleColor.Cyan);
            PrintWithDelay("              Chatbox Session Started           ", 13, ConsoleColor.Cyan);
            PrintWithDelay("------------------------------------------------\n", 13, ConsoleColor.Cyan);

            ChatBot.Launch(userName);
        }

        static void ShowAsciiTitle(string imagePath)
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

        static void PlayGreetingAudio(string audioFilePath)
        {
            if (File.Exists(audioFilePath))
            {
#pragma warning disable CA1416
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.PlaySync();
                }
#pragma warning disable CA1416
            }
            else
            {
                PrintWithDelay("Audio file not found.\n");
            }
        }

        public static void PrintWithDelay(string text, int delay = 13, ConsoleColor? color = null)
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

        
            }
        }
    
