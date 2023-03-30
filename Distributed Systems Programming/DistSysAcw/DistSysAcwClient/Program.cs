using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using System.Collections.Generic;

HttpClient client = new HttpClient();

RunAsync(client).Wait();
Console.ReadKey();

static async Task RunAsync(HttpClient client)
{
    //"Data/Name?name=John"
    client.BaseAddress = new Uri("https://localhost:44394/api/");
    //client.BaseAddress = new Uri("http://150.237.94.9/2804877/api/");
    Console.WriteLine("Hello. What would you like to do?");
    
    string input = Console.ReadLine();   
    
    while (true)
    {
        string[] inputArray = input.Split(' ');

        if (input.ToLower() == "exit")
        {
            break;
        }

        try
        {     
            Console.WriteLine("...please wait...");

            switch (inputArray[0])
            {
                #region TalkBack
                case "TalkBack":
                    {
                        switch (inputArray[1])
                        {
                            case "Hello":
                                {
                                    Task<string> task = GetStringAsync("TalkBack/Hello", client);
                                    if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                                    {
                                        Console.WriteLine(task.Result);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Request Timed Out");
                                    }
                                    break;
                                }
                            case "Sort":
                                {
                                    inputArray[2] = inputArray[2].Trim('[', ']');

                                    string output = "";
                                    string[]strArray = inputArray[2].Split(",");
                                    int[] intArray = new int[strArray.Length];

                                    for(int i = 0; i <strArray.Length; i++)
                                     intArray[i] = int.Parse(strArray[i]);

                                    foreach(int i in intArray)
                                     output += $"integers={i}&";
  
                                    Task<string> task = GetStringAsync($"talkback/sort?{output}", client);
                                    if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                                    {
                                        Console.WriteLine(task.Result);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Request Timed Out");
                                    }
                                    break;
                                }                                
                        }
                        break;
                    }
                #endregion

                #region User
                case "User":
                    {
                        switch (inputArray[1])
                        {
                            case "Get":
                                {
                                    Task<string> task = GetStringAsync($"user/new?username={inputArray[2]}", client);
                                    if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                                    {
                                        Console.WriteLine(task.Result);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Request Timed Out");
                                    }
                                    break;
                                    
                                }
                            case "Post":
                                {
                                    string responseString = "";
                                    HttpResponseMessage response = await client.PostAsJsonAsync($"user/new", inputArray[2]);
                                    responseString = await response.Content.ReadAsStringAsync();

                                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        Console.WriteLine("Got API Key");
                                        using (StreamWriter writetext = new StreamWriter("userdetails.txt"))
                                        {
                                            writetext.WriteLine("Username: " + inputArray[2] + " ApiKey: " + responseString);
                                        }
                                    }
                                    else if(response.StatusCode != System.Net.HttpStatusCode.OK){ Console.WriteLine(responseString); }
                                    break;
                                }
                            case "Set":
                                {
                                    
                                    using(StreamWriter writetext = new StreamWriter("userdetails.txt"))
                                    {
                                        writetext.WriteLine("Username: " + inputArray[2] + " ApiKey: " + inputArray[3]);
                                    }
                                    
                                    Console.WriteLine("Stored");

                                    break;
                                }
                            case "Delete":
                                {
                                    bool emptyFile = CheckTextFile("userdetails.txt");
                                    if (emptyFile == true) { Console.WriteLine("You need to do a User Post or User Set First"); break; }
                                    string[] userInfo = LoadDataFromFile("userdetails.txt"); string username = userInfo[0]; string apiKey = userInfo[1];   
                                    
                                    apiKey = apiKey.Replace("\r\n", "");  client.DefaultRequestHeaders.Add("Api-Key", apiKey);

                                    string responseBool = ""; HttpResponseMessage response = await client.DeleteAsync($"user/removeuser?username={username}");
                                    responseBool = await response.Content.ReadAsStringAsync();

                                    if (responseBool == "true") {  Console.WriteLine("True"); EmptyFile("userdetails.txt");  }
                                    else { Console.WriteLine("False"); }
                                    
                                    break;
                                }
                            case "Role":
                                {
                                    bool emptyFile = CheckTextFile("userdetails.txt");
                                    if (emptyFile == true) { Console.WriteLine("You need to do a User Post or User Set First"); break; }
                                    string[] userInfo = LoadDataFromFile("userdetails.txt"); string username = userInfo[0]; string apiKey = userInfo[1];

                                    apiKey = apiKey.Replace("\r\n", ""); client.DefaultRequestHeaders.Add("Api-Key", apiKey);

                                    break;
                                }
                        }
                        break;
                    }

                #endregion

                #region Protected
                case "Protected":
                    {
                        switch (inputArray[1])
                        {
                            case "Hello":
                                {
                                    break;
                                }
                            case "SHA1":
                                {
                                    break;
                                }
                            case "SHA256":
                                {
                                    break;
                                }
                        }
                        break;
                    }
                    #endregion
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);

        }

        Console.WriteLine("What would you like to do next?");
        input = Console.ReadLine();
        Console.Clear();
    }
    
}

static async Task<string> GetStringAsync(string path, HttpClient client)
{
    string responsestring = "";
    HttpResponseMessage response = await client.GetAsync(path);
    responsestring = await response.Content.ReadAsStringAsync();
    return responsestring;
}

static bool CheckTextFile(string filename)
{
    string emptyfile = "";
    using (StreamReader readtext = new StreamReader("userdetails.txt")) { emptyfile = readtext.ReadToEnd(); }

    if(emptyfile == "")   return true;    
    else return false;
   
}

static void EmptyFile(string filename)
{
    using (StreamWriter writetext = new StreamWriter("userdetails.txt"))
    {
        writetext.Write(string.Empty);
    }
}

static string[] LoadDataFromFile(string filename)
{
    string textfile = File.ReadAllText("userdetails.txt"); string[] textfileArray = textfile.Split(' ');
    string username = textfileArray[1]; string apiKey = textfileArray[3].Replace("\r\n", "");
    string[] userdetails = new string[] { username, apiKey};
    return userdetails;
}

