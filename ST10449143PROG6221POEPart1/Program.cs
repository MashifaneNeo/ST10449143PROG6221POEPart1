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
            // Define the paths for the image and audio files
            string imagePath = @"C:\\Users\\lab_services_student\\Desktop\\PROG6221POEPart1\\ST10449143PROG6221POEPart1\\cyber.jpg";
            string audioFilePath = @"C:\\Users\\lab_services_student\\Desktop\\PROG6221POEPart1\\ST10449143PROG6221POEPart1\\greeting.wav";

            //The title of the console window
            Console.Title = "Cybersecurity Awareness Assistant";

            //Play voice greeting and show ASCII art
            PlayGreetingAudio(audioFilePath);
            ShowAsciiTitle(imagePath);

            // Prompt the user for their name
            string userName = "";
            while (string.IsNullOrWhiteSpace(userName))
            {
                //Delay priniting to simulate conversation
                PrintWithDelay("Please enter your name: ", 13, ConsoleColor.DarkYellow);
                userName = Console.ReadLine()?.Trim();

                //Check if the name is empty or contains only whitespace
                if (string.IsNullOrWhiteSpace(userName))
                {
                    PrintWithDelay("Name cannot be empty. Please try again.", 13, ConsoleColor.Red);
                }
            }

            // Print a message to indicate the start of the chat session
            PrintWithDelay("------------------------------------------------", 13, ConsoleColor.Cyan);
            PrintWithDelay("              Chatbox Session Started           ", 13, ConsoleColor.Cyan);
            PrintWithDelay("------------------------------------------------\n", 13, ConsoleColor.Cyan);

            // Start the chat session
            ChatBot.Launch(userName);
        }

        //Method to show the ASCII art title
        static void ShowAsciiTitle(string imagePath)
        {
            // Check if the image file exists
            if (!File.Exists(imagePath))
            {
                PrintWithDelay("ASCII image not found.\n");
                return;
            }

            // Clear the console and set the background color
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Call the AsciiConverter to convert the image to ASCII art
            string asciiArt = AsciiConverter.ConvertToAsciiSimple(imagePath);
            string[] lines = asciiArt.Split(Environment.NewLine);

            // Print the ASCII art line by line
            foreach (var line in lines)
            {
                // Center the ASCII art in the console
                int consoleWidth = Console.WindowWidth;
                int padding = Math.Max((consoleWidth - line.Length) / 2, 0);
                Console.WriteLine(new string(' ', padding) + line);
            }

            Console.ResetColor();

            // Print the title with a delay
            PrintWithDelay("===============================================", 13, ConsoleColor.Magenta);
            PrintWithDelay("        Welcome to the Cybersecurity Bot       ", 13, ConsoleColor.Magenta);
            PrintWithDelay("===============================================\n", 13, ConsoleColor.Magenta);
        }

        //Method to play the greeting audio
        static void PlayGreetingAudio(string audioFilePath)
        {
            if (File.Exists(audioFilePath))
            {
                //A warning is suppressed for the SoundPlayer class, as it may not be supported on all platforms
#pragma warning disable CA1416
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    // Play the audio file synchronously
                    player.PlaySync();
                }
#pragma warning restore CA1416
            }
            else
            {
                PrintWithDelay("Audio file not found.\n");
            }
        }

        //Method to print text with a delay
        public static void PrintWithDelay(string text, int delay = 13, ConsoleColor? color = null)
        {
            if (color.HasValue)
                Console.ForegroundColor = color.Value;

            // Print each character with a delay
            foreach (char c in text)
            {
                // Check if the character is a new line
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();

            // Reset the color if it was changed
            if (color.HasValue)
                Console.ResetColor();
        }
    }
}

