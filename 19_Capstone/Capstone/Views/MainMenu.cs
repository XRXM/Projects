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

            AddOption("Display Vending Machine Items", DisplayVend);
            AddOption("Purchase", Purchase);
            AddOption("Exit", Exit);

            Configure(cfg =>
            {
                cfg.Title = "*** Main Menu ***";
                cfg.ItemForegroundColor = ConsoleColor.DarkGray;
                cfg.SelectedItemForegroundColor = ConsoleColor.Green;
                cfg.Selector = "==>";
            });

        }

        private MenuOptionResult DisplayVend()
        {
           foreach (Product product in vendingMachine.GetProducts())
            {
                Console.WriteLine($"{product.Location} {product.ProductName} {product.Price} ");
            }

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
