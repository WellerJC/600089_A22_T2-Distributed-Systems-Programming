using System;
using System.Net.Sockets;
using System.Text;

namespace PigLatinClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a message to translate: ");
            string message = Console.ReadLine();

            // Convert message to byte array using ASCII encoding
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            // Add a single byte before the message to identify how many bytes are in the proceeding message
            byte messageLength = (byte)messageBytes.Length;
            byte[] buffer = new byte[messageLength + 1];
            buffer[0] = messageLength;
            messageBytes.CopyTo(buffer, 1);

            // Connect to the server
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            // Send message to server
            stream.Write(buffer, 0, buffer.Length);

            // Receive translated message from server
            byte[] responseLength = new byte[1];
            stream.Read(responseLength, 0, 1);
            byte[] responseBytes = new byte[responseLength[0]];
            stream.Read(responseBytes, 0, responseBytes.Length);

            // Convert response bytes to ASCII and output to console
            string response = Encoding.ASCII.GetString(responseBytes);
            Console.WriteLine($"Translated message: {response}");

            // Close the connection
            client.Close();
        }
    }
}