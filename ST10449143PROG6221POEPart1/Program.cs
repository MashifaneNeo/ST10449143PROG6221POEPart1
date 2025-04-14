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

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ConvertToAsciiSimple(imagePath));
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("You can ask me questions like:");
            Console.WriteLine("  - How are you?");
            Console.WriteLine("  - What's your purpose?");
            Console.WriteLine("  - What can I ask you about?");
            Console.WriteLine("  - What is phishing?");
            Console.WriteLine("  - How to create a strong password?");
            Console.WriteLine("  - How to recognize suspicious links?");
            Console.WriteLine("  - Exit");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\nAsk me something: ");
                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("’", "").Replace("'", "").Trim();

                switch (input)
                {
                    case "how are you":
                        Console.WriteLine("\nI'm functioning perfectly—ready to help you stay cyber safe!");
                        break;
                    case "whats your purpose":
                    case "what is your purpose":
                        Console.WriteLine("\nMy purpose is to guide you in understanding cybersecurity and teach you how to protect yourself from online threats.");
                        break;
                    case "what can i ask you about":
                        Console.WriteLine("\n╔════════════════════════════════════════════╗");
                        Console.WriteLine("║ You can ask me about:                      ║");
                        Console.WriteLine("║  • How are you?                             ║");
                        Console.WriteLine("║  • What’s your purpose?                     ║");
                        Console.WriteLine("║  • What is phishing?                        ║");
                        Console.WriteLine("║  • How to create a strong password?         ║");
                        Console.WriteLine("║  • How to recognize suspicious links?       ║");
                        Console.WriteLine("║  • Exit                                     ║");
                        Console.WriteLine("╚════════════════════════════════════════════╝");
                        break;
                    case "what is phishing":
                        Console.WriteLine("\nPhishing is a cyber attack where attackers trick you into revealing personal info by pretending to be someone trustworthy.");
                        Console.WriteLine("Always verify links and never share sensitive info via email or SMS.");
                        break;
                    case "how to create a strong password":
                        Console.WriteLine("\nA strong password is at least 12 characters long and includes a mix of upper/lowercase letters, numbers, and symbols.");
                        Console.WriteLine("Avoid using personal information like your name or birthday.");
                        break;
                    case "how to recognize suspicious links":
                        Console.WriteLine("\nHover over links to preview the actual URL. Be cautious of misspelled websites or strange characters.");
                        Console.WriteLine("Avoid clicking links from unknown senders or urgent messages asking for personal information.");
                        break;
                    case "exit":
                        Console.WriteLine("\nThank you for using the Cybersecurity Awareness Assistant. Stay safe online!");
                        return;
                    default:
                        Console.WriteLine("\nSorry, I didn’t understand that. Try asking a question like 'What is phishing?' or 'How to create a strong password?'");
                        break;
                }                
            }
        }
    }
}
