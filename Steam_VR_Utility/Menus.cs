using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;


namespace Steam_VR_Utility
{
    class Menus
    {


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        public static void BringConsoleToFront()
        {
            string originalTitle = Console.Title;
            string uniqueTitle = Guid.NewGuid().ToString();
            Console.Title = uniqueTitle;
            Thread.Sleep(50);
            IntPtr handle = FindWindowByCaption(IntPtr.Zero, uniqueTitle);


            if (handle == IntPtr.Zero)
            {
                Console.WriteLine("Oops, cant find main window.");
                return;
            }
            Console.Title = originalTitle;

             SetForegroundWindow(handle);
        }
        




        public async void CreditMenu()
        {
            //This screen starts up when  you get the credit signal. Should run at least once
            bool exitkey = false; //temporary - basically keep running this loop until 
            var credits = 0;
            var gamesessioninfo = new List<string>();
            var game_image = "default";
            while (exitkey==false)
            {
                //BringConsoleToFront();
                credits = DetectCredits().Result; //wait for credits, then unpause steamm
                gamesessioninfo = DetectGame();
                GameSession(gamesessioninfo[1], credits, gamesessioninfo[0]);
                Console.Clear();
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.Clear();
                exitkey = true;

            }

        }
        private static List<string> DetectGame()
        {
            Process_Checker image_list = new Process_Checker();
            var listofimages = image_list.ImageNames_list();
            var gamesessionlength = listofimages[0];
            listofimages = listofimages.GetRange(1, listofimages.Count - 1);
            var gamechosen = image_list.process_check(listofimages);
            var gamesessioninfo = new List<string>() { gamesessionlength, gamechosen };
            


            return gamesessioninfo;
        }
        private async static Task<int> DetectCredits()
        {
            BringConsoleToFront();
            Process_Checker image_list = new Process_Checker();
            var listofimages = image_list.ImageNames_list();
            
            var credits = 0;
            Console.WriteLine("Detecting credits...");
            while (credits==0)
            {
              
                var creditInput = Task<int>.Run(() =>
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.D1)
                    {
                        Console.WriteLine("Credit read");
                        credits++;

                    }
                    return credits;

                });

                var killApps = Task.Run(() => { while (credits == 0) {
                        image_list.kill_processes(listofimages); 
                    } return 1;    });

                var creditinput = await creditInput;
                var killAppsHolder = await killApps;



            };
   

            bool tomato = false;

            var addition_credit = Task<int>.Run(() => {
                do
                {



                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.D1)
                    {
                        credits++;
                    }

                } while (tomato != true);
            });
            addition_credit.Wait(2000);
           

            Console.WriteLine("{0} credits read", credits);

            return credits;

        }

        

        private static void GameSession(string image, int credits, string sessionlength)
        {
            //Ok - removing the manual time addition. Will re-add as a component later
            ProcessManager testrun = new ProcessManager();
            bool internal_credit = true;
          


            //we'll fix this later. 660000 is 11 minutes, but we're gonna change this to 10 seconds for now for testing
            int value = Int32.Parse(sessionlength)*1000;
            
            while (internal_credit)
            {
                Console.Clear();
                Console.WriteLine("Session Beginning....");
                testrun.SuspendProcess(image, (value*credits)).Wait();
                testrun.BigClear();
                testrun.BigClear();
                Console.WriteLine("\r\nSession ended. Continue?");
                //var continue_decision = Continue_Process();
                if (Continue_Process()) 
                {
                    Console.Clear();
                    Console.WriteLine("Continuing...");
                    testrun.ResumeProcess(image).Wait();
                    credits = 1;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Session ending....");
                    testrun.KillProcess(image);
                    internal_credit = false;
                }

                 

            }

        }

        public static bool Continue_Process()
            // put the switch case in here. Wait for timespan. make it a part of a loop? next step is quitting
        {
            bool new_credit = false;
            BringConsoleToFront();
            Console.WriteLine("Waiting for new credit...");
            //wait 30 seconds for Console Readline response to be 1. If it comes, continue loop. If it doesn't, quit
             Task<bool>.Run(() => {
      

                    if(Console.ReadKey(true).Key == ConsoleKey.D1)
                    {
                        new_credit = true;
                    }
     
            }).Wait(15000);

            return new_credit;

        }

      

        

    }
}
