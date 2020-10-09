using Capstone.Class;
using Capstone.Views;
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

            VendingMachine vendingMachine = new VendingMachine(products);

            MainMenu mainMenu = new MainMenu(vendingMachine);
            mainMenu.Show();

            Console.WriteLine("Have a nice day!");
            Console.ReadKey();
          
        }
    }
}
