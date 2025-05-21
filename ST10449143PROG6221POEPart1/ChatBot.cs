using System;
using System.Collections.Generic;

namespace ST10449143PROG6221POEPart1
{
    public class ChatBot
    {
        // Random number generator for selecting tips
        private static Random random = new Random();
        // Stores the most recent topic discussed
        private static string lastTopic = "";
        // Stores the user's favorite topic
        private static string favoriteTopic = "";

        // Dictionary of topics and their respective tips
        private static Dictionary<string, string[]> topicTips = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            ["phishing"] = new[]
            {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                "Hover over links to see where they lead before clicking. Phishing links often mimic real websites.",
                "Watch for spelling mistakes and urgent language in emails – these are red flags for phishing.",
                "Never download attachments or click on links from unknown senders.",
                "Verify suspicious messages by contacting the sender through official channels, not by replying."
            },
            ["password"] = new[]
            {
                "Use a mix of upper/lowercase letters, numbers, and symbols in your passwords.",
                "Avoid using the same password across multiple sites.",
                "Consider using a password manager to store complex passwords securely.",
                "Do not include your name or birthday in your password.",
                "Change your passwords regularly to reduce risk."
            },
            ["scam"] = new[]
            {
                "Be skeptical of messages promising free money or prizes.",
                "Scammers often create fake websites that look legitimate—check URLs carefully.",
                "Don't share sensitive info over phone or email unless you're absolutely sure who you're talking to.",
                "Never feel pressured to act immediately—pause and verify.",
                "Check grammar and sender email addresses to spot scam attempts."
            },
            ["privacy"] = new[]
            {
                "Limit the amount of personal information you share online.",
                "Review app permissions regularly and revoke unnecessary access.",
                "Use private browsing modes to avoid trackers.",
                "Be careful with location sharing on social media.",
                "Turn off ad personalization features in your browser and apps."
            },
            ["encryption"] = new[]
            {
                "Use messaging apps that support end-to-end encryption.",
                "Always check for HTTPS when entering sensitive data online.",
                "Encrypt files and backups for sensitive data.",
                "Disk encryption can protect data on stolen laptops.",
                "VPNs also use encryption to secure data in transit."
            },
            ["firewall"] = new[]
            {
                "A firewall monitors incoming and outgoing traffic and blocks threats.",
                "Use both hardware and software firewalls for better protection.",
                "Don't disable your firewall when troubleshooting unless advised.",
                "Firewalls help protect against unauthorized access from the internet.",
                "Set strict firewall rules for better security on business networks."
            },
            ["antivirus"] = new[]
            {
                "Keep your antivirus definitions up to date.",
                "Run regular system scans for threats.",
                "Avoid pirated software—it may bypass antivirus protection.",
                "Use real-time protection for instant detection.",
                "Free antivirus can work, but paid versions usually offer more features."
            },
            ["backup"] = new[]
            {
                "Use the 3-2-1 backup rule: 3 copies, 2 different media, 1 offsite.",
                "Schedule automatic backups to avoid forgetting.",
                "Test your backups by restoring files occasionally.",
                "Cloud backups protect you from physical disasters.",
                "Use encryption for backups containing sensitive information."
            },
            ["twofactor"] = new[]
            {
                "2FA adds an extra layer of security beyond just your password.",
                "Use an authenticator app instead of SMS when possible.",
                "Enable 2FA on your email and financial accounts first.",
                "Keep backup codes in a safe place.",
                "2FA prevents access even if your password is stolen."
            },
            ["malware"] = new[]
            {
                "Avoid downloading software from unknown sources.",
                "Don't click on suspicious ads or pop-ups.",
                "Keep your system and software updated to patch vulnerabilities.",
                "Use antivirus software to detect malware.",
                "Run full scans if your device behaves strangely."
            },
            ["vpn"] = new[]
            {
                "VPNs encrypt your internet traffic to keep your data private.",
                "Use a VPN on public Wi-Fi networks to prevent snooping.",
                "Choose a VPN provider with a strict no-logs policy.",
                "A VPN can help you access geo-restricted content.",
                "Avoid free VPNs unless you've researched them well."
            }
        };

        // Dictionary mapping keywords to topics
        private static Dictionary<string, string> keywordToTopic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["phishing"] = "phishing",
            ["password"] = "password",
            ["scam"] = "scam",
            ["privacy"] = "privacy",
            ["encryption"] = "encryption",
            ["firewall"] = "firewall",
            ["antivirus"] = "antivirus",
            ["anti-virus"] = "antivirus",
            ["backup"] = "backup",
            ["backups"] = "backup",
            ["2fa"] = "twofactor",
            ["two factor"] = "twofactor",
            ["two-factor"] = "twofactor",
            ["malware"] = "malware",
            ["vpn"] = "vpn",
            ["virtual private network"] = "vpn"
        };

        // Method to launch the chatbot
        public static void Launch(string name)
        {
            ShowHelpMenu();
            // Print a welcome message
            bool isFirstPrompt = true;
            ConsoleColor[] promptColors = { ConsoleColor.DarkYellow, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkGreen };
            int colorIndex = 0;

            // Main loop for the chatbot
            while (true)
            {
                ConsoleColor currentColor = promptColors[colorIndex % promptColors.Length];
                colorIndex++;

                Program.PrintWithDelay($"\n{(isFirstPrompt ? $"Hi {name}, ask me something: " : $"Ask me something else {name}: ")}", 13, currentColor);
                isFirstPrompt = false;

                string input = (Console.ReadLine() ?? "").ToLower().Replace("?", "").Trim();

                DetectSentiment(input);

                if (TryRememberUserInfo(input, name)) continue;

                if ((input.Contains("more") || input.Contains("another") || input.Contains("again") || input.Contains("explain") || input.Contains("confused")) && !string.IsNullOrEmpty(lastTopic))
                {
                    RespondToFollowUp(name);
                    ProvideTipByTopic(lastTopic, name);
                    continue;
                }
                //detect if the user wants help
                string detectedTopic = DetectTopic(input);

                if (detectedTopic == "exit")
                {
                    Program.PrintWithDelay($"\nGoodbye, {name}! Stay safe online!", 13, ConsoleColor.Green);
                    return;
                }

                if (!string.IsNullOrEmpty(detectedTopic) && topicTips.ContainsKey(detectedTopic))
                {
                    lastTopic = detectedTopic;
                    ProvideTipByTopic(detectedTopic, name);
                }
                else
                {
                    Program.PrintWithDelay($"\nSorry {name}, I didn't understand that. You can also say 'more' or 'explain' to continue our last topic.", 13, ConsoleColor.Red);
                }
            }
        }
        /// Method to detect the topic based on user input
        private static string DetectTopic(string input)
        {
            foreach (var pair in keywordToTopic)
            {
                if (input.Contains(pair.Key))
                    return pair.Value;
            }

            if (input.Contains("exit") || input.Contains("quit"))
                return "exit";

            return null!;
        }
        /// Method to provide a tip based on the topic
        private static void ProvideTipByTopic(string topic, string name)
        {
            var tips = topicTips[topic];
            string tip = tips[random.Next(tips.Length)];

            if (!string.IsNullOrEmpty(favoriteTopic) && topic.Equals(favoriteTopic, StringComparison.OrdinalIgnoreCase))
                Program.PrintWithDelay($"\nSince you're interested in {favoriteTopic}, here's something you might like:", 13, ConsoleColor.Red);

            ConsoleColor[] tipColors = { ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Magenta };
            ConsoleColor color = tipColors[random.Next(tipColors.Length)];
            Program.PrintWithDelay($"\n{tip}", 13, color);
        }
        /// Method to remember user information
        private static bool TryRememberUserInfo(string input, string name)
        {
            string[] phrases = { "i'm interested in ", "i am interested in ", "im interested in " };
            foreach (var phrase in phrases)
            {
                if (input.StartsWith(phrase))
                {
                    string topic = input.Replace(phrase, "").Trim();
                    if (topicTips.ContainsKey(topic))
                    {
                        favoriteTopic = topic;
                        Program.PrintWithDelay($"\nGreat! I'll remember that you're interested in {favoriteTopic}. It's a crucial part of staying safe online.", 13, ConsoleColor.Red);
                        return true;
                    }
                }
            }
            return false;
        }

        /// Method to respond to follow-up questions
        private static void RespondToFollowUp(string name)
        {
            string[] followUpResponses =
            {
                $"Certainly, {name}. Here's another useful insight:",
                $"Absolutely, let me explain more with this next one:",
                $"Sure thing, {name}. This should help clarify:",
                $"Of course! Here’s something more to consider:",
                $"Let me give you another helpful point, {name}:"
            };
            Program.PrintWithDelay($"\n{followUpResponses[random.Next(followUpResponses.Length)]}", 13, ConsoleColor.DarkCyan);
        }

        /// Method to detect the user's sentiment
        private static void DetectSentiment(string input)
        {
            if (input.Contains("worried"))
                Program.PrintWithDelay("\nIt's okay to feel worried. Here's something that might help.", 13, ConsoleColor.Magenta);
            else if (input.Contains("curious"))
                Program.PrintWithDelay("\nCuriosity is great! Here's something to explore.", 13, ConsoleColor.Yellow);
            else if (input.Contains("frustrated"))
                Program.PrintWithDelay("\nCybersecurity can be frustrating. Here's a helpful tip.", 13, ConsoleColor.Blue);
            else if (input.Contains("confused"))
                Program.PrintWithDelay("\nIt’s okay to be confused. Let me clarify with a tip.", 13, ConsoleColor.Cyan);
            else if (input.Contains("thankful") || input.Contains("thanks") || input.Contains("thank you"))
                Program.PrintWithDelay("\nYou're welcome! Here's another tip you might like.", 13, ConsoleColor.Green);
            else if (input.Contains("anxious"))
                Program.PrintWithDelay("\nLearning helps with anxiety. Here's something reassuring.", 13, ConsoleColor.DarkYellow);
        }

        private static void ShowHelpMenu()
        {
            Program.PrintWithDelay("╔═════════════════════════════════════════════════════════════════════╗", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║ You can ask me about the following cybersecurity topics:            ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - password, phishing, scam, privacy, encryption                    ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - firewall, antivirus, backup, two-factor authentication           ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - malware, vpn                                                     ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║                                                                     ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║ You can also tell me:                                               ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How you feel about Cybersecurity like:                           ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║    worried, curious, frustrated, confused,                          ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║    thankful, anxious and excited                                    ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - Cybersecurity topics you're interested in                        ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - 'more', 'another', or 'explain' to get another tip on a topic    ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - 'exit' or 'quit' to close the chatbot                            ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("╚═════════════════════════════════════════════════════════════════════╝", 0, ConsoleColor.DarkGreen);
        }
    }
}
