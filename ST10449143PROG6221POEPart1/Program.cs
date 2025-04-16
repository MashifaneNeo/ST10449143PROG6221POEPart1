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

            LaunchChatbot(userName);
        }

        static void ShowAsciiTitle(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                PrintWithDelay("ASCII image not found.\n");
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

            PrintWithDelay("===============================================", 13, ConsoleColor.Magenta);
            PrintWithDelay("        Welcome to the Cybersecurity Bot       ", 13, ConsoleColor.Magenta);
            PrintWithDelay("===============================================\n", 13, ConsoleColor.Magenta);
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
                PrintWithDelay("Audio file not found.\n");
            }
        }

        static void PrintWithDelay(string text, int delay = 13, ConsoleColor? color = null)
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

        static void LaunchChatbot(string name)
        {
            bool isFirstPrompt = true;

            while (true)
            {
                if (isFirstPrompt)
                {
                    PrintWithDelay($"\nHi {name}, ask me something: ", 13, ConsoleColor.DarkYellow);
                    isFirstPrompt = false;
                }
                else
                {
                    PrintWithDelay($"\nAsk me something else {name}: ", 13, ConsoleColor.DarkYellow);
                }

                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("’", "").Replace("'", "").Trim();

                switch (input)
                {
                    case "how are you":
                    case "how are you?":
                        PrintWithDelay($"\nThank you for asking {name}, I'm functioning perfectly fine and in the " +
                                       $"best state to help you stay cyber safe!", 13, ConsoleColor.Blue);
                        break;

                    case "whats your purpose":
                    case "what is your purpose":
                        PrintWithDelay($"\nMy purpose {name} is to guide you through the understanding of " +
                                       $"cybersecurity and teach you how to properly protect " +
                                       $"yourself from online threats.", 13, ConsoleColor.Blue);
                        break;

                    case "what can i ask you about":
                    case "what can i ask you":
                        PrintWithDelay("╔════════════════════════════════════════════╗", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║ You can ask me questions like:             ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - How are you?                            ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - What is your purpose?                   ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - What is phishing?                       ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - How to create a strong password?        ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - How to recognize suspicious links?      ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - How to safely browse the internet?      ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("║  - Exit                                    ║", 13, ConsoleColor.DarkGreen);
                        PrintWithDelay("╚════════════════════════════════════════════╝", 13, ConsoleColor.DarkGreen);
                        break;

                    case "what is phishing":
                    case "whats phishing":
                        PrintWithDelay($"\nPhishing is a form of cyberattack where malicious actors act us trustworthy" +
                            $" people to trick you into revealing personal or sensitive information. " +
                            $"You should always stay vigilant while revealing personal information {name}.", 13, ConsoleColor.Blue);

                        PrintWithDelay("\nKey Tips to Avoid Phishing:", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Always double-check URLs before clicking.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Be cautious of unsolicited emails or texts.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Never provide passwords or sensitive data to unverified sources.", 13, ConsoleColor.Blue);
                        break;

                    case "how to create a strong password":
                    case "how to create a password":
                        PrintWithDelay($"\nCreating a strong password is essential for protecting your online accounts {name}.", 13, ConsoleColor.Blue);

                        PrintWithDelay("\nTips for a Strong Password:", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Use at least 12 characters.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Include a mixture of uppercase, lowercase, numbers, and symbols.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Avoid using personal information such as birthdates.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Consider using a password manager to keep track of your passwords.", 13, ConsoleColor.Blue);
                        break;

                    case "how to recognize suspicious links":
                    case "how to identify suspicious links":
                        PrintWithDelay($"\nThat’s a great question, {name}. Identifying suspicious links is key to avoiding scams.", 13, ConsoleColor.Blue);

                        PrintWithDelay("\nWays to Recognize Suspicious Links:", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Hover over links to preview the destination URL.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Look for misspellings of domain names.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Avoid clicking on links in unexpected messages.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Verify the source before taking any action.", 13, ConsoleColor.Blue);
                        break;

                    case "how to safely browse the internet":
                    case "how to browse the internet safely":
                    case "how to browse the internet":
                        PrintWithDelay($"\nBrowsing the internet safely is crucial for your online security {name}.", 13, ConsoleColor.Blue);
                        PrintWithDelay("\nTips for Safe Browsing:", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Use a secure and updated web browser.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Enable pop-up blockers and ad blockers.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Avoid downloading files from untrusted sources.", 13, ConsoleColor.Blue);
                        PrintWithDelay("- Use a VPN for added privacy and security.", 13, ConsoleColor.Blue);
                        break;

                    case "exit":
                    case "Exit":
                        PrintWithDelay($"\nThank you for using the Cybersecurity Awareness Assistant {name}. " +
                            $"Please stay safe online!", 13, ConsoleColor.Cyan);
                        return;

                    default:
                        PrintWithDelay($"\nSorry {name}, I didn’t quite understand that.", 13, ConsoleColor.Red);
                        PrintWithDelay("Please either rephrase, ask a different question or exit.", 13, ConsoleColor.Red);
                        break;
                }
            }
        }
    }
}