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

            AddOption("Display Vending Machine Items", DisplayVend );
            AddOption("Purchase", Purchase);
            AddOption("Exit", Exit);

            Configure(cfg =>
            {
                cfg.Title = "*** Main Menu ***";
                cfg.ItemForegroundColor = ConsoleColor.Gray;
                cfg.SelectedItemForegroundColor = ConsoleColor.Green;
                cfg.Selector = "==>";
            });

        }

        private MenuOptionResult DisplayVend()
        {
            foreach (Product product in vendingMachine.GetProducts())
            {
                Console.WriteLine($"{product.Location,-20}{product.ProductName,8}{product.Price,10 :C}{product.Stock,15} ");
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
