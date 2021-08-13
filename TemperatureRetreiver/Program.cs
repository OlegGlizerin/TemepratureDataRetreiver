using Service;
using Service.Interfaces;
using System;
using System.IO;

namespace TemperatureRetreiver
{
    public class Program
    {
        public static IAmazonService _amazonService;

        public static void Main(string[] args)
        {

            


            bool endApp = false;
            _amazonService = new AmazonService();
            
            PrintWelcome();

            while (!endApp)
            {
                PrintAvailableCommandFirstStep();
                var userInput = Console.ReadLine();

                switch(userInput)
                {
                    case "1":
                        HandleGetWeatherByDate();
                            break;
                    case "2":
                        HandleUserStop(userInput);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }
            return;
        }

        private static void PrintWelcome()
        {
            // Display title as the C# console calculator app.
            Console.WriteLine("Welcome to weather science task\r");
            Console.WriteLine("------------------------\n");
        }

        private static void PrintAvailableCommandFirstStep()
        {
            Console.Write("Available Commands\n");
            Console.Write("[1] -> Get weather by date\n");
            Console.Write("[3] -> Exit\n");
        }

        private static void PrintAvailableCommandSecondStep()
        {
            Console.Clear();
            Console.Write("Please wait, Downloading the file...\n");
            Console.Write("Available Commands\n");
            Console.Write("[2] -> Stop Remote Process and go to previus menu\n");
        }

        private static void PrintEnterDate()
        {
            Console.Write("Enter date for calc degrees Example -> ('YYYY/MM/DD HH:MM'):\n");
        }

        private static bool HandleGetWeatherByDate()
        {
            var userAborted = false;
            PrintEnterDate();
            var userInput = Console.ReadLine();
            _amazonService.SendMessage("Start", userInput);
            while (!SuccessFileExist() && !userAborted)
            {
                PrintAvailableCommandSecondStep();
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "2":
                        _amazonService.SendMessage("Stop", userInput);
                        userAborted = true;
                        break;
                }
            }
            if (SuccessFileExist())
            {
                String line;
                try
                {
                    var successFilePath = $"{Directory.GetCurrentDirectory()}\\Success.txt";
                    Console.WriteLine($"Start SuccessFileExist -> successFilePath: {successFilePath}");
                    StreamReader sr = new StreamReader(successFilePath);
                    line = sr.ReadLine();
                    sr.Close();
                    Console.WriteLine($"Finished retrive data successfully with path: {successFilePath}.\n");
                    PrintHappyEnd(line);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    File.Delete($"{Directory.GetCurrentDirectory()}\\Success.txt");
                }
            }
            return userAborted;
        }

        private static bool SuccessFileExist()
        {
            var fileExists = File.Exists($"{Directory.GetCurrentDirectory()}\\Success.txt");
            return fileExists;
        }

        private static void HandleUserStop(string userInput)
        {
            _amazonService.SendMessage("Stop", userInput);
        }

        private static void PrintHappyEnd(string celcius)
        {
            Console.WriteLine($"**********************************\n");
            Console.WriteLine($"**** Result in celcius is: {celcius} ****\n");
            Console.WriteLine($"**********************************\n");
        }
    }
}
