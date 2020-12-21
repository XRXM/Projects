using Capstone.Class;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class MainMenu : ConsoleMenu
    {
        private VendingMachine vendingMachine;

        public MainMenu(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
            
            AddOption("\t(1) Display Vending Machine Items", DisplayVend);
            AddOption("\t(2) Purchase", Purchase);
            AddOption("\t(3) Exit", Exit);
            Configure(cfg =>
            {
                cfg.Title = "\n\t************ Main Menu ************";
                cfg.ItemForegroundColor = ConsoleColor.Cyan;
                cfg.SelectedItemForegroundColor = ConsoleColor.White;
                cfg.Selector = " ~~>";
            });
        }

        private MenuOptionResult DisplayVend()
        {
            string[] headings = { " Location", "Product Name", "Amount", "Stock" };

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($" {headings[0],-20} {headings[1],20} {headings[2],20} {headings[3],20}");
            Console.WriteLine($"{new string('_', 20)} {new string('_', 20)} {new string('_', 20)} {new string('_', 20)} ");
            Console.WriteLine();
            Console.WriteLine($"{new string('_', 85)}");
            foreach (Product product in vendingMachine.GetProducts())
            {
                Console.WriteLine($" {product.Location,-20}{product.ProductName,20}{product.Price,20:C}{product.Stock,20} ");
            }
            Console.WriteLine($"{new string('_', 85)}");
            return MenuOptionResult.WaitAfterMenuSelection;



        }

        private MenuOptionResult Purchase()
        {



            // Show the categories menu
            PurchaseMenu purch = new PurchaseMenu(vendingMachine);
            purch.Show();

            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        
    }
}
