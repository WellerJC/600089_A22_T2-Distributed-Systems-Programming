using Client;
using System;
using System.Threading.Tasks;

// Add code here to send requests
Task one = ClientBehaviours.SendRequest("Hello World", "TaskOne");
Task two = ClientBehaviours.SendRequest("TaskTwo", "TaskTwo");
Task three = ClientBehaviours.SendRequest("TaskThree", "TaskThree");

one.Wait(); two.Wait(); three.Wait();   

Console.WriteLine("Execution finished");
Console.ReadLine();