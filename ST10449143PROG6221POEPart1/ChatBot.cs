using System;
using System.Threading;

namespace ST10449143PROG6221POEPart1
{
    public class ChatBot
    {
        public static void Launch(string name)
        {
            bool isFirstPrompt = true;

            while (true)
            {
                if (isFirstPrompt)
                {
                    Program.PrintWithDelay($"\nHi {name}, ask me something: ", 13, ConsoleColor.DarkYellow);
                    isFirstPrompt = false;
                }
                else
                {
                    Program.PrintWithDelay($"\nAsk me something else {name}: ", 13, ConsoleColor.DarkYellow);
                }

                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("’", "").Replace("'", "").Trim();

                switch (input)
                {
                    case "how are you":
                    case "how are you?":
                        Program.PrintWithDelay($"\nThank you for asking {name}, I'm functioning perfectly " +
                            $"fine and in the best state to help you stay cyber safe!", 13, ConsoleColor.Blue);
                        break;

                    case "whats your purpose":
                    case "what is your purpose":
                        Program.PrintWithDelay($"\nMy purpose {name} is to guide you through the understanding " +
                            $"of cybersecurity and teach you how to properly protect yourself from " +
                            $"online threats.", 13, ConsoleColor.Blue);
                        break;

                    case "what can i ask you about":
                    case "what can i ask you":
                        Program.PrintWithDelay("╔════════════════════════════════════════════╗", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║ You can ask me questions like:             ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - How are you?                            ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - What is your purpose?                   ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - What is phishing?                       ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - How to create a strong password?        ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - How to recognize suspicious links?      ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - How to safely browse the internet?      ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("║  - Exit                                    ║", 13, ConsoleColor.DarkGreen);
                        Program.PrintWithDelay("╚════════════════════════════════════════════╝", 13, ConsoleColor.DarkGreen);
                        break;

                    case "what is phishing":
                    case "whats phishing":
                        Program.PrintWithDelay($"\nPhishing is a form of cyberattack where malicious actors impersonate trustworthy sources " +
                            $"to trick you into revealing personal or sensitive information. " +
                            $"You should always stay vigilant while revealing personal information {name}.", 13, ConsoleColor.Blue);

                        Program.PrintWithDelay("\nKey Tips to Avoid Phishing:", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Always double-check URLs before clicking.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Be cautious of unsolicited emails or texts.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Never provide passwords or sensitive data to unverified sources.", 13, ConsoleColor.Blue);
                        break;

                    case "how to create a strong password":
                    case "how to create a password":
                        Program.PrintWithDelay($"\nCreating a strong password is essential for protecting your online accounts {name}.", 13, ConsoleColor.Blue);

                        Program.PrintWithDelay("\nTips for a Strong Password:", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Use at least 12 characters.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Include a mix of uppercase, lowercase, numbers, and symbols.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Avoid using personal information such as names or birthdates.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Consider using a password manager to keep track of your passwords.", 13, ConsoleColor.Blue);
                        break;

                    case "how to recognize suspicious links":
                    case "how to identify suspicious links":
                        Program.PrintWithDelay($"\nThat’s a great question, {name}. Identifying suspicious links" +
                            $" is key to avoiding scams.", 13, ConsoleColor.Blue);

                        Program.PrintWithDelay("\nWays to Recognize Suspicious Links:", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Hover over links to preview the destination URL.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Look for misspellings or unusual domain names.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Avoid clicking on links in urgent or unexpected messages.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Verify the source before taking any action.", 13, ConsoleColor.Blue);
                        break;

                    case "how to safely browse the internet":
                    case "how to browse the internet safely":
                    case "how to browse the internet":
                        Program.PrintWithDelay($"\nBrowsing the internet safely is crucial for your" +
                            $" online security {name}.", 13, ConsoleColor.Blue);

                        Program.PrintWithDelay("\nTips for Safe Browsing:", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Use a secure and updated web browser.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Enable pop-up blockers and ad blockers.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Avoid downloading files from untrusted sources.", 13, ConsoleColor.Blue);
                        Program.PrintWithDelay("- Use a VPN for added privacy and security.", 13, ConsoleColor.Blue);
                        break;

                    case "exit":
                        Program.PrintWithDelay($"\nThank you for using the Cybersecurity Awareness Assistant {name}." +
                            $" Please stay safe online!", 13, ConsoleColor.Blue);
                        return;

                    default:
                        Program.PrintWithDelay($"\nSorry {name}, I didn’t quite understand that.", 13, ConsoleColor.Red);
                        Program.PrintWithDelay("Please either rephrase, ask a different question or exit.", 13, ConsoleColor.Red);
                        break;
                }
            }
        }
    }
}
