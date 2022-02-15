using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steam_VR_Utility
{
    class Booter
    {

        /* https://stackoverflow.com/questions/38147552/c-sharp-application-to-launch-a-steam-game
         * Steam games are launched by steam:// uri scheme which maps to steam.exe executable. 
         * So you pretty much need to start steam.exe process passing uri
         * like steam://rungameid/game_id where game_id is numerical id of that game.
         */


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
