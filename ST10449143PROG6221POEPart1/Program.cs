using System;
using System.Media;
using System.IO;

namespace ST10449143PROG6221POEPart1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the audio file 
            string audioFilePath = @"C:\Users\lab_services_student\Desktop\PROG6221POEPart1\ST10449143PROG6221POEPart1\greeting.wav";

            // Check if the file exists
            if (File.Exists(audioFilePath))
            {
                // Create a SoundPlayer object and play the sound as a voice greeting
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.Load();  // Loads the audio file
                    Console.WriteLine("Playing voice greeting...");

                    player.PlaySync(); // PlaySync waits for the audio to finish before continuing
                }
            }
            else
            {
                Console.WriteLine("Audio file not found. Please check the file path.");
            }

            // Once the greeting has finished, proceed with the chatbot introduction
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nHello! Welcome to the Cybersecurity Awareness Assistant.");
            Console.WriteLine("I'm here to help you stay safe online. Let's begin!\n");
            Console.ResetColor();
        }
    }
}