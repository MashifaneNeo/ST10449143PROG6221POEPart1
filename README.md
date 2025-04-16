Cybersecurity Awareness Chatbot  

Welcome to the Cybersecurity Awareness Chatbot, which is a console-based chatbot designed to educate and enlighten users about vital cybersecurity concepts in an interactive way that is engaging for the user. This application operates as a digital assistant that provides direction on relevant cybersecurity topics such as phishing, password security, safe browsing, and recognizing suspicious links.  

The program is built with C# and intuitive libraries such as SixLabors.ImageSharp for ASCII art conversion, this application offers a visually appealing and informative experience to help users stay safe online. 

Features:  
- Inovative user Interface: Engage in user friendly conversations with the bot to learn about cybersecurity and best practices to follow.  
- ASCII Art Display: Visual title rendering for an enhanced user experience.  
- Audio Greeting: Audio playback of a welcome sound to create an welcoming introduction.  
- Unique Responses: The chatbot provides descriptive answers to common cybersecurity questions.  
- User-Friendly Prompts: Clear instructions and error handling for smooth interactions and a seemless user experience.  

Setup & Requirements:
  
- .NET SDK (6.0 or later)  
- SixLabors.ImageSharp (for ASCII art conversion)  
- Audio file support (Windows-only)  

Installation: 
1. Clone or download the repository.  
2. Ensure the required dependencies are installed via NuGet.      
3. Place the required files (`cyber.jpg` for ASCII art and `greeting.wav` for audio) in the correct directory.  

Running the Application: 
Execute the program using:   
dotnet run  

Usage:
1. Launch the Program:  
   - The application starts with an audio greeting and ASCII art title.  
   - Enter your name when prompted.  

2. Interact with the Chatbot:  
   - Ask cybersecurity-related questions (e.g., "What is phishing?").  
   - Type "exit" to end the session.  

3. Available Commands:  
   - General questions (e.g., "How are you?", "What is your purpose?").  
   - Cybersecurity topics.  
   - Type "What can I ask you?" for a list of supported queries.  

Project Structure:  
- Program.cs:  
  - Handles initialization, user input, and console interactions.  
  - Manages ASCII art display and audio playback.  

- ChatBot.cs:  
  - Contains the chatbot logic and predefined responses.  
  - Processes user input and provides relevant cybersecurity advice.  

- AsciiConverter.cs:  
  - Converts an image to ASCII art for the welcome screen.  

License:
This project is open-source and available under the MIT License.  

Acknowledgments  
- SixLabors.ImageSharp for image processing.  
- Microsoft’s .NET framework for cross-platform support.  

Stay safe and secure online!  

For questions or feedback, please open an issue in the repository.
