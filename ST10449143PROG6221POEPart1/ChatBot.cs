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
                    ShowHelpMenu();
                    Program.PrintWithDelay($"\nHi {name}, ask me something: ", 13, ConsoleColor.DarkYellow);
                    isFirstPrompt = false;
                }
                else
                {
                    Program.PrintWithDelay($"\nAsk me something else {name}: ", 13, ConsoleColor.DarkYellow);
                }

                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("’", "").Replace("'", "").Trim();

                // Keyword recognition
                if (input.Contains("how are you"))
                {
                    RespondWithKeyword("you", name);
                }
                else if (input.Contains("your purpose") || input.Contains("what is your purpose"))
                {
                    RespondWithKeyword("purpose", name);
                }
                else if (input.Contains("phishing"))
                {
                    RespondWithKeyword("phishing", name);
                }
                else if (input.Contains("password") || input.Contains("create a password"))
                {
                    RespondWithKeyword("password", name);
                }
                else if (input.Contains("links") || input.Contains("identify links"))
                {
                    RespondWithKeyword("links", name);
                }
                else if (input.Contains("internet") || input.Contains("browse the internet"))
                {
                    RespondWithKeyword("internet", name);
                }
                else if (input.Contains("what can i ask"))
                {
                    ShowHelpMenu();
                }
                else if (input == "exit")
                {
                    Program.PrintWithDelay($"\nThank you for using the Cybersecurity Awareness Assistant {name}. Please stay safe online!", 13, ConsoleColor.Blue);
                    return;
                }
                else
                {
                    Program.PrintWithDelay($"\nSorry {name}, I didn’t quite understand that.", 13, ConsoleColor.Red);
                    Program.PrintWithDelay("Please either rephrase, ask a different question or exit.", 13, ConsoleColor.Red);
                }
            }
        }

        private static void RespondWithKeyword(string keyword, string name)
        {
            switch (keyword)
            {
                case "you":
                    Program.PrintWithDelay($"\nThank you for asking {name}, I'm functioning perfectly fine and ready to help you stay cyber safe!", 13, ConsoleColor.Blue);
                    break;

                case "purpose":
                    Program.PrintWithDelay($"\nMy purpose, {name}, is to guide you through cybersecurity awareness and help you protect yourself from online threats.", 13, ConsoleColor.Blue);
                    break;

                case "phishing":
                    Program.PrintWithDelay($"\nPhishing is a cyberattack where attackers pretend to be trustworthy entities to steal personal information. Stay vigilant, {name}.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("\nKey Tips to Avoid Phishing:", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Always double-check URLs before clicking.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Be cautious of unsolicited emails or texts.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Never provide passwords or sensitive data to unverified sources.", 13, ConsoleColor.Blue);
                    break;

                case "password":
                    Program.PrintWithDelay($"\nCreating a strong password is essential for your security, {name}.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("\nTips for a Strong Password:", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Use at least 12 characters.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Include a mix of upper/lowercase letters, numbers, and symbols.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Avoid personal information like names or birthdates.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Use a password manager to keep track of your passwords.", 13, ConsoleColor.Blue);
                    break;

                case "links":
                    Program.PrintWithDelay($"\nGreat question, {name}. Identifying suspicious links helps avoid scams.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("\nWays to Recognize Suspicious Links:", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Hover over links to preview the destination.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Look for misspellings or strange domain names.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Avoid links in urgent/unexpected messages.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Always verify the source.", 13, ConsoleColor.Blue);
                    break;

                case "internet":
                    Program.PrintWithDelay($"\nBrowsing the internet safely is crucial for online security, {name}.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("\nTips for Safe Browsing:", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Use a secure and updated browser.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Enable pop-up and ad blockers.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Avoid downloading from unknown sources.", 13, ConsoleColor.Blue);
                    Program.PrintWithDelay("- Use a VPN for added privacy.", 13, ConsoleColor.Blue);
                    break;
            }
        }

        private static void ShowHelpMenu()
        {
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
        }
    }
}
