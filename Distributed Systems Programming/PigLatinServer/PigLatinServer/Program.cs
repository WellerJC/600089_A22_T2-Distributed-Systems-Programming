using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PigLatinServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a new TcpListener instance bound to localhost on port 5000
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
                listener.Start();
                Console.WriteLine("Server started. Waiting for connections...");

                // Accept a new client connection
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                // Retrieve the network stream from the client
                NetworkStream stream = client.GetStream();

                // Read the length of the incoming message
                int messageLength = stream.ReadByte();

                // Read the message from the stream and convert it to a string
                byte[] buffer = new byte[messageLength];
                int bytesRead = stream.Read(buffer, 0, messageLength);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Translate the message to Pig Latin
                string translatedMessage = TranslateToPigLatin(message);

                // Serialize the translated message and write it back to the stream
                byte[] translatedBytes = Encoding.ASCII.GetBytes(translatedMessage);
                stream.WriteByte((byte)translatedBytes.Length);
                stream.Write(translatedBytes, 0, translatedBytes.Length);

                // Close the client and listener
                client.Close();
                listener.Stop();
                Console.WriteLine("Client disconnected. Server stopped.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private static string TranslateToPigLatin(string message)
        {
            // TODO: Implement Pig Latin translation logic here
            string[] words = message.Split(' ');

            // Translate each word to Pig Latin
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = TranslateWord(words[i]);
            }

            // Join translated words back into a string
            string output = string.Join(" ", words);

            return output;
        }

        private static string TranslateWord(string word)
        {
            // If the word starts with a vowel, just add "way" to the end
            if (IsVowel(word[0]))
            {
                return word + "way";
            }

            // Find the index of the first vowel
            int index = -1;
            for (int i = 0; i < word.Length; i++)
            {
                if (IsVowel(word[i]))
                {
                    index = i;
                    break;
                }
            }
            // If there are no vowels, just return the word
            if (index == -1)
            {
                return word;
            }

            // Move the consonant cluster to the end of the word and add "ay"
            return word.Substring(index) + word.Substring(0, index) + "ay";
        }

        private static bool IsVowel(char c)
        {
            return "AEIOUaeiou".IndexOf(c) != -1;
        }
    }
}