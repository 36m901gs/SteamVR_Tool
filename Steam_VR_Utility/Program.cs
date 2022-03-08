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
            bool tomato = true;
            Menus steamvr = new Menus();
            while (tomato!=false) {
                BruteForce();
                
            }
            
            

        }

        static void BruteForce()
        {
            bool tomato = true;
            Menus steamvr = new Menus();
            while (tomato != false)
            {
                steamvr.CreditMenu();
                tomato = true;
            }
        }



    }
}
