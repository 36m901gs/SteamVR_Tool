using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;


namespace Steam_VR_Utility
{
    class Menus
    {


        

        public async void CreditMenu()
        {
            //This screen starts up when  you get the credit signal. Should run at least once
            bool exitkey = false; //temporary - basically keep running this loop until 
            while (exitkey==false)
            {

                var credits = DetectCredits(); //wait for credits, then unpause steamm
                var game_image = DetectGame();
                GameSession(game_image, credits);
                Console.Clear();
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.Clear();
               
            }

        }
        private static string DetectGame()
        {
            Process_Checker image_list = new Process_Checker();
            var listofimages = image_list.ImageNames_list();
            var gamechosen = image_list.process_check(listofimages);
            Console.WriteLine("{0} selected", gamechosen);


            return gamechosen;
        }
        private static int DetectCredits()
        {
            string keypress = "stock";
            var credits = 0;
            while(keypress!="1")
            {
                Console.WriteLine("Detecting credits...");
                keypress = Console.ReadLine();
                if (keypress == "1")
                {
                    credits++;
                    
                }
                
            }
            bool tomato = false;

            var addition_credit = Task<int>.Run(() => {
                while(tomato!=true)
                {
                    Console.WriteLine("Press 1 for additional credits...");
                    keypress = Console.ReadLine();
                    if (keypress == "1")
                    {
                        credits++;
                    }
                }
            });
            addition_credit.Wait(3000);
            /*
            if (addition_credit.Wait(15000) == false)
            {

            }*/

            /* Stopwatch s = new Stopwatch();
            s.Start();
            Console.WriteLine("Giving time to stack credits...");
            while (s.Elapsed < TimeSpan.FromMilliseconds(700))
            {
                keypress = Console.ReadLine(); //problem with this is you have to press enter,
                                               //so you're gonna have to use the system hook.
                                               //but we'll handle that after 
                                               // we do this shit
                Console.WriteLine("{0} seconds", s.Elapsed);
                if (keypress == "1")
                {
                    credits++;

                }
            }*/

            Console.WriteLine("{0} credits read", credits);

            return credits;

        }

        private static void GameSession(string image, int credits)
        {
            //Ok - removing the manual time addition. Will re-add as a component later
            ProcessManager testrun = new ProcessManager();
            bool internal_credit = true;
            //Console.WriteLine("\r\n Input number of seconds for program suspension");
            //string line = Console.ReadLine();


            //we'll fix this later. 660000 is 11 minutes, but we're gonna change this to 10 seconds for now for testing
            int value = 5000;
            /*
            while (!(Int32.TryParse(line, out value)))
            {
                Console.Clear();
                Console.WriteLine("\r\n Input number of seconds for program suspension");
                line = Console.ReadLine();


            } */
            while (internal_credit)
            {
                Console.Clear();
                Console.WriteLine("Session Beginning....");
                testrun.SuspendProcess(image, (value*credits)).Wait();
                Console.Clear();
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.Clear();
                Console.WriteLine("\r\nSession ended. Continue?");
                var continue_decision = Continue_Process();
                if (continue_decision) 
                {
                    Console.Clear();
                    Console.WriteLine("Continuing...");
                    testrun.ResumeProcess(image).Wait();
                    credits = 1;
                }
                if (!continue_decision)
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

            Console.WriteLine("Waiting for new credit...");
            //wait 30 seconds for Console Readline response to be 1. If it comes, continue loop. If it doesn't, quit
            var credit = Task<int>.Run(() => {
                switch (Console.ReadLine())
                {
                    case "1": // this needs to be time dependent, and default to quitting if nothing is pressed
                        return 1;

                    default:
                        return 0;
                }
            });
            if (credit.Wait(15000) == false || credit.Result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task<int> Continue_Process_Input()
        {
            switch (Console.ReadLine()) //do this 
            {
                case "1": // this needs to be time dependent, and default to quitting if nothing is pressed
                    return 1;

                default:
                    return 0;
            }


        }

    }
}
