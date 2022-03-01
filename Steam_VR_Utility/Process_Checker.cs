using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Linq;


namespace Steam_VR_Utility
{
    class Process_Checker
    {
        public List<string> ImageNames_list()
        {
            string path = "C:\\Imagenamecsv.txt";

            string[] image_list = System.IO.File.ReadAllLines(path);
            List<string> ig_list = new List<string>();




            foreach (string line in image_list)
            {
                string[] images = line.Split(',');
                foreach (string imagenames in images)
                {
                    ig_list.Add(imagenames);
                }

            }



            return ig_list;
            


        }

        public string process_check(List<string> image_names)
        {
            bool detected = false;
         
            string running_game = "default";
            if (image_names.Count!=0)
            {
                Console.WriteLine("Searching for new process");
                while (!detected)
                {
                    

                    Process[] processes = new Process[] { };


                    for (int i = 1; i <= image_names.Count; i++)
                    {
                        //break the loop once processes is greater than one
                        if (Process.GetProcessesByName(image_names[i - 1]).Length > 0) 
                        {
                            running_game = image_names[i - 1];
                            detected = true;
                            Console.WriteLine("Game found, now starting session for {0}", running_game);
                            break;

                        }
                        
                    }

                }

            }

            return running_game;


        }

        public void kill_processes(List<string> image_names)
        {
            ProcessManager testrun = new ProcessManager();

            string running_game = "default";
            Process[] processes = new Process[] { };


                for (int i = 1; i <= image_names.Count; i++)
                {
                    //break the loop once processes is greater than one
                    if (Process.GetProcessesByName(image_names[i - 1]).Length > 0)
                    {
                        running_game = image_names[i - 1];

                        System.Diagnostics.Process.Start("pskill.exe", "\"" + running_game + "\"");

                        Console.WriteLine("Killed process for {0}", running_game);
                    testrun.BigClear();
                    testrun.BigClear();
                    testrun.BigClear();
                    break;
                    Console.WriteLine("Detecting credits...");

                }

                }
        }

       

    }
}
