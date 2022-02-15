using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace Steam_VR_Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }

            /* we'll add this back in later 
             Booter testrun = new Booter();
             testrun.LaunchSteam();
             Console.WriteLine("Cross your fingers!"); */
        }

        private static bool MainMenu()
        {

            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Play Game");
            Console.WriteLine("2) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CreditMenu();
                    return true;
                case "2":
                    return false;
                default:
                    return true;
            }



        }

        private async static void CreditMenu()
        {
            //This screen starts up when  you get the credit signal. Should run at least once
            bool hasCredit = true;
            while (hasCredit)
            {
                Console.Clear();
                Console.WriteLine("What game are you playing?");
                var game_image = Console.ReadLine();
                GameSession(game_image);
                Console.Clear();
                Console.WriteLine("Try another game");
                Console.WriteLine("1. Continue");
                Console.WriteLine("2. Quit");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        break;
                    case "2":
                        hasCredit = false;
                        break;
                    default:
                        break;
                }

            }

        }

        private static void GameSession(string image)
        {
            ProcessManager testrun = new ProcessManager();
            bool internal_credit = true;
            Console.WriteLine("\r\n Input number of seconds for program suspension");
            string line = Console.ReadLine();
            int value;
            while(!(Int32.TryParse(line, out value)))
            {
                Console.Clear();
                Console.WriteLine("\r\n Input number of seconds for program suspension");
                line = Console.ReadLine();


            }
            while (internal_credit) {
                Console.Clear();
                Console.WriteLine("Session Beginning....");
                testrun.SuspendProcess(image, value).Wait();
                Console.Clear();
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.WriteLine("\r\n================================================");
                Console.Clear();
                Console.WriteLine("\r\nSession ended. Continue?");
                Console.WriteLine("1. Continue");
                Console.WriteLine("2. Quit");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        testrun.ResumeProcess(image).Wait();
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("Quitting");
                        testrun.KillProcess(image);
                        Console.Clear();
                        internal_credit = false;
                        break;
                    default:
                        Console.WriteLine("Quitting");
                        testrun.KillProcess(image);
                        Console.Clear();
                        internal_credit = false;
                        break;
                }

            }
           









        }





    }
}
