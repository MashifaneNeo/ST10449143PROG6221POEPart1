using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10449143PROG6221POEPart1
{
    public class ChatBot
    {
        private static readonly Random random = new Random();
        private static string lastTopic = "";
        private static Dictionary<string, List<string>> topicTips = new Dictionary<string, List<string>>();
        private static Dictionary<string, Queue<string>> tipQueues = new Dictionary<string, Queue<string>>();

        public static void Launch(string name)
        {
            InitializeTips();

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
                    Program.PrintWithDelay($"\nWhat else would you like to ask, {name}?", 13, ConsoleColor.DarkYellow);
                }

                string rawInput = Console.ReadLine() ?? "";
                string input = rawInput.ToLower().Replace("’", "").Replace("'", "").Trim();

                if (IsFollowUpQuestion(input))
                {
                    if (!string.IsNullOrEmpty(lastTopic))
                    {
                        RespondWithKeyword(lastTopic, name, isFollowUp: true);
                    }
                    else
                    {
                        Program.PrintWithDelay("We haven’t started a topic yet. Try asking a cybersecurity-related question!", 13, ConsoleColor.Red);
                    }
                    continue;
                }

                // Now updated to handle help menu questions directly
                if (input.Contains("how are you"))
                {
                    RespondWithKeyword("you", name);
                    lastTopic = "";
                }
                else if (input.Contains("what is your purpose") || input.Contains("your purpose"))
                {
                    RespondWithKeyword("purpose", name);
                    lastTopic = "";
                }
                else if (input.Contains("what is phishing"))
                {
                    lastTopic = "phishing";
                    ResetQueue("phishing");
                    Program.PrintWithDelay($"\nPhishing is when attackers impersonate trustworthy sources to steal your data. Always verify sender details.", 13, ConsoleColor.Blue);
                }
                else if (input.Contains("how to create a strong password") || input.Contains("strong password"))
                {
                    lastTopic = "password";
                    ResetQueue("password");
                    Program.PrintWithDelay($"\nA strong password includes uppercase, lowercase, numbers, and symbols. Avoid personal info and reuse.", 13, ConsoleColor.Blue);
                }
                else if (input.Contains("how to recognize suspicious links") || input.Contains("suspicious links"))
                {
                    lastTopic = "links";
                    ResetQueue("links");
                    Program.PrintWithDelay($"\nSuspicious links often look like legit URLs but may contain odd characters or come from unknown senders. Hover before clicking.", 13, ConsoleColor.Blue);
                }
                else if (input.Contains("how to safely browse the internet") || input.Contains("safely browse"))
                {
                    lastTopic = "internet";
                    ResetQueue("internet");
                    Program.PrintWithDelay($"\nSafe browsing means using secure sites (https), avoiding public Wi-Fi without VPN, and keeping your software updated.", 13, ConsoleColor.Blue);
                }
                else if (input.Contains("phishing"))
                {
                    lastTopic = "phishing";
                    ResetQueue("phishing");
                    RespondWithKeyword("phishing", name);
                }
                else if (input.Contains("password"))
                {
                    lastTopic = "password";
                    ResetQueue("password");
                    RespondWithKeyword("password", name);
                }
                else if (input.Contains("links"))
                {
                    lastTopic = "links";
                    ResetQueue("links");
                    RespondWithKeyword("links", name);
                }
                else if (input.Contains("internet"))
                {
                    lastTopic = "internet";
                    ResetQueue("internet");
                    RespondWithKeyword("internet", name);
                }
                else if (input.Contains("vpn"))
                {
                    lastTopic = "vpn";
                    ResetQueue("vpn");
                    RespondWithKeyword("vpn", name);
                }
                else if (input.Contains("firewall"))
                {
                    lastTopic = "firewall";
                    ResetQueue("firewall");
                    RespondWithKeyword("firewall", name);
                }
                else if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("multi-factor"))
                {
                    lastTopic = "2fa";
                    ResetQueue("2fa");
                    RespondWithKeyword("2fa", name);
                }
                else if (input.Contains("ransomware"))
                {
                    lastTopic = "ransomware";
                    ResetQueue("ransomware");
                    RespondWithKeyword("ransomware", name);
                }
                else if (input.Contains("antivirus"))
                {
                    lastTopic = "antivirus";
                    ResetQueue("antivirus");
                    RespondWithKeyword("antivirus", name);
                }
                else if (input.Contains("social engineering"))
                {
                    lastTopic = "social engineering";
                    ResetQueue("social engineering");
                    RespondWithKeyword("social engineering", name);
                }
                else if (input.Contains("software updates") || input.Contains("update software"))
                {
                    lastTopic = "software updates";
                    ResetQueue("software updates");
                    RespondWithKeyword("software updates", name);
                }
                else if (input.Contains("secure browsing") || input.Contains("browser safety"))
                {
                    lastTopic = "secure browsing";
                    ResetQueue("secure browsing");
                    RespondWithKeyword("secure browsing", name);
                }
                else if (input.Contains("data breach"))
                {
                    lastTopic = "data breach";
                    ResetQueue("data breach");
                    RespondWithKeyword("data breach", name);
                }
                else if (input.Contains("cyberbullying"))
                {
                    lastTopic = "cyberbullying";
                    ResetQueue("cyberbullying");
                    RespondWithKeyword("cyberbullying", name);
                }
                else if (input.Contains("help") || input.Contains("what can i ask"))
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
                    Program.PrintWithDelay("Try rephrasing your question or asking something from the help menu.", 13, ConsoleColor.Red);
                }
            }
        }

        private static bool IsFollowUpQuestion(string input)
        {
            return input.Contains("more") ||
                   input.Contains("details") ||
                   input.Contains("explain") ||
                   input.Contains("what do you mean") ||
                   input.Contains("i don't understand") ||
                   input.Contains("can you repeat") ||
                   input.Contains("again") ||
                   input.Contains("another tip");
        }

        private static void RespondWithKeyword(string keyword, string name, bool isFollowUp = false)
        {
            if (isFollowUp)
            {
                Program.PrintWithDelay($"\nSure {name}, here’s more information about {keyword}:", 13, ConsoleColor.Blue);
            }

            switch (keyword)
            {
                case "you":
                    Program.PrintWithDelay($"\nThank you for asking {name}, I'm functioning perfectly fine and ready to help you stay cyber safe!", 13, ConsoleColor.Blue);
                    break;

                case "purpose":
                    Program.PrintWithDelay($"\nMy purpose, {name}, is to guide you through cybersecurity awareness and help you protect yourself from online threats.", 13, ConsoleColor.Blue);
                    break;

                case "phishing":
                case "password":
                case "links":
                case "internet":
                    PrintNextTip(keyword, name);
                    break;
            }
        }

        private static void PrintNextTip(string topic, string name)
        {
            if (tipQueues[topic].Count == 0)
            {
                ResetQueue(topic);
                Program.PrintWithDelay("You've seen all tips for this topic. Restarting tips...", 13, ConsoleColor.DarkCyan);
            }

            string nextTip = tipQueues[topic].Dequeue();
            Program.PrintWithDelay("- " + nextTip, 13, ConsoleColor.Blue);
        }

        private static void ResetQueue(string topic)
        {
            if (topicTips.ContainsKey(topic))
            {
                tipQueues[topic] = new Queue<string>(topicTips[topic].OrderBy(x => random.Next()));
            }
        }

        private static void InitializeTips()
        {
            topicTips = new Dictionary<string, List<string>>
            {
                ["phishing"] = new List<string>
                {
                    "Don’t open attachments from unknown senders. These could contain malware.",
                    "Phishing scams often use urgent language like 'Your account will be closed!' to scare you into clicking.",
                    "Always double-check URLs — fake sites often use slight misspellings.",
                    "Never click on suspicious pop-ups asking for your login credentials.",
                    "Real companies never ask for passwords or payment info via email."
                },
                ["password"] = new List<string>
                {
                    "Strong passwords include uppercase, lowercase, numbers, and symbols.",
                    "Never reuse passwords. Each account should have a unique one.",
                    "Consider using a password manager like Bitwarden or LastPass.",
                    "A passphrase like 'CorrectHorseBatteryStaple!' is easier to remember and very secure.",
                    "Avoid using personal info like birthdays or pet names in passwords.",
                    "Change your passwords regularly, especially after a data breach."
                },
                ["links"] = new List<string>
                {
                    "Hover your mouse over a link to preview the destination before clicking.",
                    "Shortened URLs (like bit.ly) can hide dangerous sites — be cautious.",
                    "Fake links often look very close to real ones, like go0gle.com instead of google.com.",
                    "If unsure, type the website name into your browser manually.",
                    "Links in text messages can also be dangerous. Only click those from trusted contacts.",
                    "Install browser add-ons that check website safety like Web of Trust (WOT)."
                },
                ["internet"] = new List<string>
                {
                    "Avoid using public Wi-Fi for banking or shopping. Use a VPN when possible.",
                    "Keep your browser and operating system up to date.",
                    "Only download files or extensions from trusted websites.",
                    "Check the lock symbol (🔒) and 'https://' in the address bar when entering sensitive information.",
                    "Log out of accounts when you're done, especially on shared computers.",
                    "Be mindful of the information you share on social media."
                },
                ["malware"] = new List<string>
{
                    "Malware is software designed to harm or exploit devices, services, or networks.",
                    "Avoid downloading files from untrusted sources to prevent malware infections.",
                    "Install antivirus and anti-malware software and keep it updated.",
                    "Malware can include viruses, worms, trojans, ransomware, and spyware.",
                    "Pop-ups or slow computer performance can be signs of malware infection."
},
                ["ransomware"] = new List<string>
{
                    "Ransomware locks your files and demands payment to unlock them.",
                    "Never pay the ransom; there's no guarantee your files will be restored.",
                    "Back up your data regularly to avoid total loss in case of ransomware.",
                    "Most ransomware comes from phishing emails or unsafe downloads.",
                    "Use updated security software to help prevent ransomware attacks."
},
                ["firewall"] = new List<string>
{
                    "A firewall is a security system that monitors and controls network traffic.",
                    "It acts as a barrier between trusted and untrusted networks.",
                    "Enable the firewall on your operating system and router for protection.",
                    "Firewalls can block malicious traffic before it reaches your device."
},
                ["two-factor authentication"] = new List<string>
{
                    "2FA adds a second layer of security to your logins.",
                    "Even if someone knows your password, they can’t log in without the second factor.",
                    "Use apps like Google Authenticator or SMS codes for 2FA.",
                    "Enable 2FA for important accounts like email, banking, and social media."
},
                ["vpn"] = new List<string>
{
                    "A VPN encrypts your internet connection and hides your IP address.",
                    "Use a VPN on public Wi-Fi to keep your data private.",
                    "VPNs help bypass regional restrictions and censorship.",
                    "Choose a trusted VPN provider with a no-logs policy."
},
                ["social engineering"] = new List<string>
{
                    "Social engineering is tricking people into giving up confidential info.",
                    "It relies on human error rather than technical hacking.",
                    "Be skeptical of unexpected phone calls or emails asking for credentials.",
                    "Verify identities before sharing sensitive information."
                },
                ["data breach"] = new List<string>
{
                    "A data breach is when sensitive info is accessed without authorization.",
                    "Use HaveIBeenPwned.com to check if your email was in a breach.",
                    "Change your password immediately if you're part of a breach.",
                    "Enable 2FA to protect breached accounts from being hijacked."
},
                ["spyware"] = new List<string>
                {
                    "Spyware secretly gathers your data without your knowledge.",
                    "It may track keystrokes, browser habits, or steal credentials.",
                    "Use anti-spyware tools and be cautious of free software installs.",
                    "Check for unusual device behavior as a spyware warning."
},
                ["identity theft"] = new List<string>
                {
                    "Identity theft is when someone uses your personal data to impersonate you.",
                    "Shred sensitive documents before throwing them away.",
                    "Monitor bank and credit card statements regularly.",
                    "Be wary of giving your ID number or banking info online."
},
                ["shoulder surfing"] = new List<string>
{
                    "Shoulder surfing is spying on someone entering a password or PIN.",
                    "Always shield your screen and keypad when entering sensitive info.",
                    "Use biometric authentication to reduce keypad use.",
                    "Stay aware of your surroundings in public or crowded places."}
            };

            foreach (var topic in topicTips.Keys)
            {
                ResetQueue(topic);
            }
        }

        private static void ShowHelpMenu()
        {
            Program.PrintWithDelay("╔══════════════════════════════════════════════════════════════════════════════════╗", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║ You can ask me questions like:                                                   ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How are you?                                                                  ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is your purpose?                                                         ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is phishing?                                                             ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How to create a strong password?                                              ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How to recognize suspicious links?                                            ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How to safely browse the internet?                                            ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - Give me another tip about phishing                                            ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is two-factor authentication (2FA)?                                      ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is malware?                                                              ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How can I secure my home Wi-Fi network?                                       ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is a VPN and why should I use one?                                       ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What should I do after a data breach?                                         ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How do I identify a secure website?                                           ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is ransomware and how can I avoid it?                                    ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How do I keep my mobile devices secure?                                       ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - What is social engineering in cybersecurity?                                  ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - How can I tell if an app is safe to install?                                  ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("║  - Exit                                                                           ║", 1, ConsoleColor.DarkGreen);
            Program.PrintWithDelay("╚══════════════════════════════════════════════════════════════════════════════════╝", 1, ConsoleColor.DarkGreen);
        }

    }
}
