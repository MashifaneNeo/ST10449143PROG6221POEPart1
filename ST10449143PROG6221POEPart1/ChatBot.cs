using System;
using System.Threading;

namespace ST10449143PROG6221POEPart1
{
    public class ChatBot
    {
        private static Random random = new Random();
        private static string lastTopic = "";
        private static string favoriteTopic = "";

        // --- Arrays of tips per topic ---
        private static string[] phishingTips = new string[]
        {
            "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
            "Hover over links to see where they lead before clicking. Phishing links often mimic real websites.",
            "Watch for spelling mistakes and urgent language in emails – these are red flags for phishing.",
            "Never download attachments or click on links from unknown senders.",
            "Verify suspicious messages by contacting the sender through official channels, not by replying."
        };

        private static string[] passwordTips = new string[]
        {
            "Use a mix of upper/lowercase letters, numbers, and symbols in your passwords.",
            "Avoid using the same password across multiple sites.",
            "Consider using a password manager to store complex passwords securely.",
            "Do not include your name or birthday in your password.",
            "Change your passwords regularly to reduce risk."
        };

        private static string[] scamTips = new string[]
        {
            "Be skeptical of messages promising free money or prizes.",
            "Scammers often create fake websites that look legitimate—check URLs carefully.",
            "Don't share sensitive info over phone or email unless you're absolutely sure who you're talking to.",
            "Never feel pressured to act immediately—pause and verify.",
            "Check grammar and sender email addresses to spot scam attempts."
        };

        private static string[] privacyTips = new string[]
        {
            "Limit the amount of personal information you share online.",
            "Review app permissions regularly and revoke unnecessary access.",
            "Use private browsing modes to avoid trackers.",
            "Be careful with location sharing on social media.",
            "Turn off ad personalization features in your browser and apps."
        };

        private static string[] encryptionTips = new string[]
        {
            "Use messaging apps that support end-to-end encryption.",
            "Always check for HTTPS when entering sensitive data online.",
            "Encrypt files and backups for sensitive data.",
            "Disk encryption can protect data on stolen laptops.",
            "VPNs also use encryption to secure data in transit."
        };

        private static string[] firewallTips = new string[]
        {
            "A firewall monitors incoming and outgoing traffic and blocks threats.",
            "Use both hardware and software firewalls for better protection.",
            "Don't disable your firewall when troubleshooting unless advised.",
            "Firewalls help protect against unauthorized access from the internet.",
            "Set strict firewall rules for better security on business networks."
        };

        private static string[] antivirusTips = new string[]
        {
            "Keep your antivirus definitions up to date.",
            "Run regular system scans for threats.",
            "Avoid pirated software—it may bypass antivirus protection.",
            "Use real-time protection for instant detection.",
            "Free antivirus can work, but paid versions usually offer more features."
        };

        private static string[] backupTips = new string[]
        {
            "Use the 3-2-1 backup rule: 3 copies, 2 different media, 1 offsite.",
            "Schedule automatic backups to avoid forgetting.",
            "Test your backups by restoring files occasionally.",
            "Cloud backups protect you from physical disasters.",
            "Use encryption for backups containing sensitive information."
        };

        private static string[] twoFactorTips = new string[]
        {
            "2FA adds an extra layer of security beyond just your password.",
            "Use an authenticator app instead of SMS when possible.",
            "Enable 2FA on your email and financial accounts first.",
            "Keep backup codes in a safe place.",
            "2FA prevents access even if your password is stolen."
        };

        private static string[] malwareTips = new string[]
        {
            "Avoid downloading software from unknown sources.",
            "Don't click on suspicious ads or pop-ups.",
            "Keep your system and software updated to patch vulnerabilities.",
            "Use antivirus software to detect malware.",
            "Run full scans if your device behaves strangely."
        };

        private static string[] vpnTips = new string[]
        {
            "VPNs encrypt your internet traffic to keep your data private.",
            "Use a VPN on public Wi-Fi networks to prevent snooping.",
            "Choose a VPN provider with a strict no-logs policy.",
            "A VPN can help you access geo-restricted content.",
            "Avoid free VPNs unless you've researched them well."
        };

        public static void Launch(string name)
        {
            ShowHelpMenu();

            bool isFirstPrompt = true;
            ConsoleColor[] promptColors = { ConsoleColor.DarkYellow, ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta, ConsoleColor.DarkGreen };
            int colorIndex = 0;

            while (true)
            {
                ConsoleColor currentColor = promptColors[colorIndex % promptColors.Length];
                colorIndex++;

                Program.PrintWithDelay($"\n{(isFirstPrompt ? $"Hi {name}, ask me something: " : $"Ask me something else {name}: ")}", 13, currentColor);
                isFirstPrompt = false;

                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("?", "").Trim();

                // Respond empathetically to sentiment
                DetectSentiment(input);

                if (TryRememberUserInfo(input, name)) continue;

                if ((input.Contains("more") || input.Contains("another") || input.Contains("again") || input.Contains("explain") || input.Contains("confused")) && !string.IsNullOrEmpty(lastTopic))
                {
                    ProvideTipByTopic(lastTopic, name);
                    continue;
                }

                if (input.Contains("password")) lastTopic = "password";
                else if (input.Contains("phishing")) lastTopic = "phishing";
                else if (input.Contains("scam")) lastTopic = "scam";
                else if (input.Contains("privacy")) lastTopic = "privacy";
                else if (input.Contains("encryption")) lastTopic = "encryption";
                else if (input.Contains("firewall")) lastTopic = "firewall";
                else if (input.Contains("antivirus") || input.Contains("anti-virus")) lastTopic = "antivirus";
                else if (input.Contains("backup") || input.Contains("backups")) lastTopic = "backup";
                else if (input.Contains("two-factor") || input.Contains("two factor") || input.Contains("2fa")) lastTopic = "twofactor";
                else if (input.Contains("malware")) lastTopic = "malware";
                else if (input.Contains("vpn") || input.Contains("virtual private network")) lastTopic = "vpn";
                else if (input.Contains("exit") || input.Contains("quit"))
                {
                    Program.PrintWithDelay($"\nGoodbye, {name}! Stay safe online!", 13, ConsoleColor.Green);
                    return;
                }
                else
                {
                    Program.PrintWithDelay($"\nSorry {name}, I didn't understand that." +
                        $" Please rephrase or ask a about a different topic.", 13, ConsoleColor.Red);
                    continue;
                }

                ProvideTipByTopic(lastTopic, name);
            }
        }

        private static bool TryRememberUserInfo(string input, string name)
        {
            if (input.StartsWith("i'm interested in ") || input.StartsWith("i am interested in ") || input.StartsWith("im interested in "))
            {
                string topic = input.Replace("i'm interested in ", "").Replace("i am interested in ", "").Replace("im interested in ", "").Trim();
                if (IsKnownTopic(topic))
                {
                    favoriteTopic = topic;
                    Program.PrintWithDelay($"\nGreat! I'll remember that you're interested in {favoriteTopic}. It's a crucial part of staying safe online.", 13, ConsoleColor.Red);
                    return true;
                }
            }
            return false;
        }

        private static void DetectSentiment(string input)
        {
            if (input.Contains("worried"))
            {
                Program.PrintWithDelay("\nIt's completely understandable to feel that way. Cyber threats can seem overwhelming. Let me share some tips to help you stay safe.", 13, ConsoleColor.Magenta);
            }
            else if (input.Contains("curious"))
            {
                Program.PrintWithDelay("\nI love curiosity! Exploring cybersecurity is the first step to becoming more informed and protected.", 13, ConsoleColor.Yellow);
            }
            else if (input.Contains("frustrated"))
            {
                Program.PrintWithDelay("\nI'm here to help, even when things get frustrating. Cybersecurity can be tricky, but you're not alone!", 13, ConsoleColor.Blue);
            }
            else if (input.Contains("confused"))
            {
                Program.PrintWithDelay("\nNo worries! Cybersecurity concepts can be tricky at first. Feel free to ask me to explain anything in more detail.", 13, ConsoleColor.Cyan);
            }
            else if (input.Contains("thankful") || input.Contains("thanks") || input.Contains("thank you"))
            {
                Program.PrintWithDelay("\nYou're welcome! I'm glad I could help you learn about cybersecurity.", 13, ConsoleColor.Green);
            }
            else if (input.Contains("anxious"))
            {
                Program.PrintWithDelay("\nIt's normal to feel anxious about cybersecurity. Taking small steps to learn can make you feel more in control.", 13, ConsoleColor.DarkYellow);
            }
            else if (input.Contains("excited"))
            {
                Program.PrintWithDelay("\nThat's awesome! Staying excited about learning helps you stay ahead in cybersecurity.", 13, ConsoleColor.Magenta);
            }
        }

        private static bool IsKnownTopic(string topic)
        {
            string t = topic.ToLower();
            return t == "password" || t == "phishing" || t == "scam" || t == "privacy" || t == "encryption" ||
                   t == "firewall" || t == "antivirus" || t == "backup" || t == "two-factor" ||
                   t == "2fa" || t == "malware" || t == "vpn";
        }

        private static void ProvideTipByTopic(string topic, string name)
        {
            ConsoleColor[] tipColors = { ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.White, ConsoleColor.Magenta };
            ConsoleColor currentTipColor = tipColors[random.Next(tipColors.Length)];

            if (!string.IsNullOrEmpty(favoriteTopic) && topic.ToLower().Contains(favoriteTopic.ToLower()))
            {
                Program.PrintWithDelay($"\nSince you're interested in {favoriteTopic}, here's a tip:", 13, ConsoleColor.Red);
            }

            string[] tips = topic switch
            {
                "phishing" => phishingTips,
                "password" => passwordTips,
                "scam" => scamTips,
                "privacy" => privacyTips,
                "encryption" => encryptionTips,
                "firewall" => firewallTips,
                "antivirus" => antivirusTips,
                "backup" => backupTips,
                "twofactor" => twoFactorTips,
                "malware" => malwareTips,
                "vpn" => vpnTips,
                _ => new string[] { "Sorry, I don’t have tips for that topic." }
            };

            string selectedTip = tips[random.Next(tips.Length)];
            Program.PrintWithDelay($"\nTip: {selectedTip}", 13, currentTipColor);
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
