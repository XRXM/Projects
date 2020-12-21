using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Capstone.Class
{
    public class Change
    {
        public int Quarters { get; set; }
        public int Dimes { get; set; }
        public int Nickles { get; set; }

        public Change(int quarters, int dimes, int nickles)
        {
            Quarters = quarters;
            Dimes = dimes;
            Nickles = nickles;
        }
    }

   
}
