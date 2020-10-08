using Capstone.Class;
using System;
using System.Collections.Generic;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Environment.CurrentDirectory;
            IEnumerable<Product> products = Stocker.StockProducts(@"..\..\..\..\vendingmachine.csv");

          
        }
    }
}
