using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

HttpClient client = new HttpClient();

RunAsync(client).Wait();
Console.ReadKey();


static async Task RunAsync(HttpClient client)
{
    //"Data/Name?name=John"
    //client.BaseAddress = new Uri("https://localhost:44394/api/");
    client.BaseAddress = new Uri("https://150.237.94.9/2804877/api/");
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
                                    string[] strArray = inputArray[2].Split(",");
                                    int[] intArray = new int[strArray.Length];

                                    for (int i = 0; i < strArray.Length; i++)
                                        intArray[i] = int.Parse(strArray[i]);

                                    foreach (int i in intArray)
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
                                    Task<string> task = GetStringAsyncBody($"user/new", inputArray[2], client);
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
                            case "Set":
                                {
                                    break;
                                }
                            case "Delete":
                                {
                                    break;
                                }
                            case "Role":
                                {
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

static async Task<string> GetStringAsyncBody(string path, string body, HttpClient client)
{
    string responseString = "";
    HttpResponseMessage response = await client.PostAsJsonAsync(path, body);
    responseString = await response.Content.ReadAsStringAsync(); return responseString;
}