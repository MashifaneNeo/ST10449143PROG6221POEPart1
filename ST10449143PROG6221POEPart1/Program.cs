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
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("ASCII image not found.\n");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            string asciiArt = ConvertToAsciiSimple(imagePath);
            string[] lines = asciiArt.Split(Environment.NewLine);

            foreach (var line in lines)
            {
                int consoleWidth = Console.WindowWidth;
                int padding = Math.Max((consoleWidth - line.Length) / 2, 0);
                Console.WriteLine(new string(' ', padding) + line);
            }

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===============================================");
            Console.WriteLine("        Welcome to the Cybersecurity Bot       ");
            Console.WriteLine("===============================================\n");
            Console.ResetColor();            
        }

        static string ConvertToAsciiSimple(string imagePath)
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

        static void PlayGreetingAudio(string audioFilePath)
        {
            if (File.Exists(audioFilePath))
            {
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
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
            bool isFirstPrompt = true;

            while (true)
            {
                if (isFirstPrompt)
                {
                    Console.Write($"\nHi {name}, ask me something: ");
                    isFirstPrompt = false;
                }
                else
                {
                    Console.Write($"\nAsk me something else {name}: ");
                }

                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("’", "").Replace("'", "").Trim();

                switch (input)
                {
                    case "how are you":
                    case "how are you?":
                    case "How are you":
                    case "How are you?":
                        Console.WriteLine($"\nThank you for asking {name}, I'm functioning perfectly fine and in the best state to help you stay cyber safe!");
                        break;

                    case "what's your purpose":
                    case "what is your purpose":
                        Console.WriteLine($"\nMy purpose {name} is to guide you through the understanding of cybersecurity and teach you how to properly protect yourself from online threats.");
                        break;

                    case "what can i ask you about":
                    case "what can i ask you":
                        Console.WriteLine("\n╔════════════════════════════════════════════╗");
                        Console.WriteLine("║ You can ask me questions like:             ║");
                        Console.WriteLine("║  - How are you?                            ║");
                        Console.WriteLine("║  - What’s your purpose?                    ║");
                        Console.WriteLine("║  - What is phishing?                       ║");
                        Console.WriteLine("║  - How to create a strong password?        ║");
                        Console.WriteLine("║  - How to recognize suspicious links?      ║");
                        Console.WriteLine("║  - How to safely browse the internet?      ║");
                        Console.WriteLine("║  - Exit                                    ║");
                        Console.WriteLine("╚════════════════════════════════════════════╝");
                        break;

                    case "what is phishing":
                    case "what's phishing":
                        Console.WriteLine($"\nPhishing is a form of cyberattack where malicious actors impersonate trustworthy " +
                            $"sources or people to trick you into    revealing personal or sensitive information. You should always" +
                            $"stay vigilant while revealing personal information {name}.");

                        Console.WriteLine("\nKey Tips to Avoid Phishing:");
                        Console.WriteLine("- Always double-check URLs before clicking.");
                        Console.WriteLine("- Be cautious of unsolicited emails or texts.");
                        Console.WriteLine("- Never provide passwords or sensitive data to unverified sources.");
                        break;

                    case "how to create a strong password":
                    case "how to create a password":
                        Console.WriteLine($"\nCreating a strong password is essential for protecting your online accounts {name}.");
                        Console.WriteLine("\nTips for a Strong Password:");
                        Console.WriteLine("- Use at least 12 characters.");
                        Console.WriteLine("- Include a mix of uppercase, lowercase, numbers, and symbols.");
                        Console.WriteLine("- Avoid using personal information such as names or birthdates.");
                        Console.WriteLine("- Consider using a password manager to keep track of your passwords.");
                        break;

                    case "how to recognize suspicious links":
                    case "how to identify suspicious links":
                        Console.WriteLine($"\nThat’s a great question, {name}. Identifying suspicious links is key to avoiding scams.");
                        Console.WriteLine("\nWays to Recognize Suspicious Links:");
                        Console.WriteLine("- Hover over links to preview the destination URL.");
                        Console.WriteLine("- Look for misspellings or unusual domain names.");
                        Console.WriteLine("- Avoid clicking on links in urgent or unexpected messages.");
                        Console.WriteLine("- Verify the source before taking any action.");
                        break;

                    case "exit":
                        Console.WriteLine($"\nThank you for using the Cybersecurity Awareness Assistant {name}. Please stay safe online!");
                        return;

                    default:
                        Console.WriteLine($"\nSorry {name}, I didn’t quite understand that.");
                        Console.WriteLine("Try asking something like: 'What is phishing?' or 'How to create a strong password?'");
                        break;

                }
            }
        }
    }
}
