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
                 "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations and this is the method that is most udes to mislead people.",
                 "Hover over links to see where they lead before clicking. Phishing links often mimic real websites to confuse you into thinking that the website is authentic.",
                 "Watch out for spelling mistakes and urgent language in emails – these are red flags for phishing.",
                 "Be cautious of the content you want to download on the internet. Never download attachments or click on links from unknown senders.",
                 "Verify suspicious messages by contacting the sender through official channels, through this you can prevent the chances of being a victim of phishing."                
            },
            ["password"] = new[]
            {
                 "Use a mix of letters, numbers, and symbols in your passwords. Avoid using easily guessable information like birthdays or names.",
                 "Use a password manager to generate and store complex passwords. Reusing passwords increases the risk if one site gets compromised.",
                 "Change your passwords regularly, especially for sensitive accounts. Strong passwords are a critical first defense against cyber threats.",
                 "Enusure that all your passwords are atleast 8 characters long and include special characters in them as well to increase the diffuculty of a security breach.",
                 "Don't share your passwords with anyone, even if they claim to be from a trusted source. Always keep your passwords confidential."
            },
            ["scam"] = new[]
            {
                "Scammers often impersonate trusted sources to trick you. Always verify the identity of the person or organization before sharing information.",
                "Be cautious of messages that create urgency, like saying your account will be locked. Scammers use fear to make you act quickly without thinking.",
                "Never click on suspicious links or download unknown attachments. Scams can come through email, SMS, or even phone calls.",
                "Research unfamiliar companies before engaging with them. A quick search can reveal red flags or scam warnings from others.",
                "Always trust your instincts. If something feels off or too good to be true, then it is most likely a scam."
            },
            ["privacy"] = new[]
            {
                "Limit what you share on social media and review your privacy settings. Oversharing can expose you to identity theft or stalking.",
                "Use encrypted messaging apps to protect your conversations. Privacy-focused tools give you better control over your data.",
                "Avoid using public Wi-Fi for sensitive transactions. Your data can be easily intercepted on unsecured networks.",
                "Use private browsing or incognito mode to minimize tracking. While not foolproof, it helps reduce your online footprint.",
                "Turn off location sharing for apps that don’t need it. Constant GPS tracking can reveal patterns about your movements."
            },
            ["encryption"] = new[]
            {
                "Encryption scrambles your data so only authorized parties can read it. It’s essential for protecting sensitive information.",
                "Use end-to-end encrypted services for messaging and file storage. Without encryption, your data is vulnerable during transmission.",
                "Always encrypt your device if it contains sensitive data. This ensures that even if stolen, the information remains protected.",
                "Look for HTTPS in your browser's address bar when entering sensitive info. This means your connection is encrypted.",
                "Consider full disk encryption for laptops and portable drives. This protects all files, not just selected ones."
            },
            ["firewall"] = new[]
            {
                "A firewall filters incoming and outgoing traffic, blocking potentially dangerous connections. It acts as a protective barrier for your network.",
                "Configure your firewall to block unauthorized access while allowing legitimate communication. Many systems include built-in firewalls—make sure they're turned on.",
                "Firewalls can prevent malware from communicating with command centers. Keeping them active adds a critical layer of defense.",
                "Use both software and hardware firewalls for layered protection. This reduces your risk if one layer fails.",
                "Review your firewall logs regularly. This can reveal unusual activity or attempted intrusions."
            },
            ["antivirus"] = new[]
            {
                "Antivirus software scans your system for malicious programs. It helps prevent, detect, and remove threats like viruses and trojans.",
                "Keep your antivirus updated so it can recognize the latest threats. New malware appears daily, so updates are essential.",
                "Use real-time scanning and run full scans periodically. Antivirus alone isn’t foolproof, but it significantly reduces risk.",
                "Avoid disabling your antivirus, even temporarily. Threats can strike at any time, especially when downloading files.",
                "Choose antivirus software that includes web protection. It can block dangerous websites before they harm your system."
            },
            ["backup"] = new[]
            {
                "Regularly back up your data to an external drive or secure cloud service. This protects your information in case of hardware failure or cyberattack.",
                "Use automated backups to ensure consistency. Don't wait until disaster strikes to realize your important files are gone.",
                "Test your backups to ensure they can be restored. A backup is only useful if it actually works when you need it.",
                "Follow the 3-2-1 rule: three copies of data, on two types of media, with one offsite. This strategy minimizes data loss.",
                "Encrypt your backups, especially if stored in the cloud. This protects them from unauthorized access."
            },
            ["twofactor"] = new[]
            {
                "Two-factor authentication (2FA) adds an extra layer of security by requiring a second form of verification. This makes it harder for attackers to access your account even if they have your password.",
                "Use app-based 2FA like Google Authenticator instead of SMS when possible. It's more secure and less vulnerable to SIM swapping.",
                "Enable 2FA on all accounts that support it, especially email, banking, and social media. It’s a simple way to strengthen your security.",
                "Do not share your 2FA codes with anyone. Legitimate services will never ask for them.",
                "Back up your 2FA codes or recovery keys. Losing access to your authenticator app can lock you out of your accounts."
            },
            ["malware"] = new[]
            {
                "Malware is software designed to harm or exploit any programmable device or network. It includes viruses, ransomware, spyware, and more.",
                "Avoid downloading files or clicking links from untrusted sources. Malware often disguises itself as legitimate software.",
                "Keep your operating system and software up to date. Security patches fix vulnerabilities that malware can exploit.",
                "Be cautious of fake updates or pop-ups claiming your device is infected. These are often tricks to install malware.",
                "Use behavior-based malware protection when possible. It can detect threats based on unusual activity, not just known signatures."
            },
            ["vpn"] = new[]
            {
                "A VPN encrypts your internet connection, making your online activity private. It’s especially useful when using public Wi-Fi.",
                "Choose a trustworthy VPN provider with a no-logs policy. Some free VPNs may log or sell your data.",
                "VPNs help you bypass geo-restrictions and reduce tracking, but they don’t make you invincible. Combine them with other security practices.",
                "Avoid using free VPNs for sensitive activities. They may lack proper encryption or contain ads and tracking software.",
                "Always connect to servers in privacy-friendly jurisdictions when possible. Some countries have laws requiring data retention."
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

                if (detectedTopic == "help")
                {
                    ShowHelpMenu();
                    continue;
                }

                if (!string.IsNullOrEmpty(detectedTopic) && topicTips.ContainsKey(detectedTopic))
                {
                    lastTopic = detectedTopic;
                    ProvideTipByTopic(detectedTopic, name);
                }
                else
                {
                    Program.PrintWithDelay($"\nSorry {name}, I didn't understand that. " +
                        $"Please rephrase or ask about a different topic.", 13, ConsoleColor.Red);
                }
            }
        }
        //Method to detect the topic based on user input
        private static string DetectTopic(string input)
        {
            foreach (var pair in keywordToTopic)
            {
                if (input.Contains(pair.Key))
                    return pair.Value;
            }

            if (input.Contains("exit") || input.Contains("quit"))
                return "exit";

            // Detect help-related phrases like "what can I ask" or "help"
            if (input.Contains("what can i ask") || input.Contains("help") || input.Contains("ask you"))
                return "help";

            return null!;
        }
        //Method to provide a tip based on the topic
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
        // Method to remember user information
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

        // Method to respond to follow-up questions
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

        // Method to detect the user's sentiment
        private static void DetectSentiment(string input)
        {
            if (input.Contains("worried"))
                Program.PrintWithDelay("\nIt's okay to feel worried. Cerain topics can be hard to understand, here's something that might help you understand better.", 13, ConsoleColor.Magenta);
            else if (input.Contains("curious"))
                Program.PrintWithDelay("\nCuriosity is great thing! Here's something to aid you in exploring your curiousity.", 13, ConsoleColor.Yellow);
            else if (input.Contains("frustrated"))
                Program.PrintWithDelay("\nCybersecurity can be frustrating at times. Here's a helpful tip to ease your frustration.", 13, ConsoleColor.Blue);            
            else if (input.Contains("thankful") || input.Contains("thanks") || input.Contains("thank you"))
                Program.PrintWithDelay("\nThats good to hear! being thankful for knowledge is a great thing. Here's another tip you might like.", 13, ConsoleColor.Green);
            else if (input.Contains("anxious"))
                Program.PrintWithDelay("\nI am so sorry to hear that. Learning helps with anxiety, here's something reassuring.", 13, ConsoleColor.DarkYellow);
            else if (input.Contains("excited"))
                Program.PrintWithDelay("\nIt's awesome to see your excitement! Here's something to keep that energy going!", 13, ConsoleColor.Gray);
            else if (input.Contains("scared"))
                Program.PrintWithDelay("\nBeing scared is a natural thing in cybersecurity! heres a tip to help you be less scared.", 13, ConsoleColor.Cyan);
            else if (input.Contains("uninterested"))
                Program.PrintWithDelay("\nThats not good. But its understandable to lose interest about this topic, maybe this can reignite your interest.", 13, ConsoleColor.Red);
            else if (input.Contains("unsure"))
                Program.PrintWithDelay("\nThats perfectly fine! These types of topics are quite complex, lets help you understand further.", 13, ConsoleColor.DarkGreen);
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
            Program.PrintWithDelay("║    thankful, anxious, excited, scared,                              ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║    uninterested and unsure                                          ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - Cybersecurity topics you're interested in                        ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - 'more', 'another', or 'explain' to get another tip on a topic    ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - 'exit' or 'quit' to close the chatbot                            ║", 0, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("╚═════════════════════════════════════════════════════════════════════╝", 0, ConsoleColor.DarkGreen);
        }
    }
}
