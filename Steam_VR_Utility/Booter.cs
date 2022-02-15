using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steam_VR_Utility
{
    class Booter
    {

        
        public void LaunchSteam(string imagename)
        {
            string argument = "steam://rungameid/881100";
            //let's just test this out and load up Noita
            Process P = Process.Start(@"C:\Program Files (x86)\Steam\steam.exe",argument);

            //let's start up the processpauser, pause it after 30 seconds

            ProcessManager Test = new ProcessManager();
            Test.SuspendProcess(imagename, 10).Wait();
            Test.ResumeProcess(imagename).Wait();



            




        }

       

    }
}
