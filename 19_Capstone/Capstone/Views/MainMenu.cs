using Capstone.Class;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class MainMenu : ConsoleMenu
    {
        private VendingMachine VendingMachine;
            
        public MainMenu(VendingMachine vendingMachine)
        {
            VendingMachine = vendingMachine;

            AddOption("Display Vending Machine Items", DisplayVend);
            AddOption("Purchase", Purchase);
            AddOption("Exit", Exit);
        
        }

        private MenuOptionResult DisplayVend()
        {
            // Show the categories menu
            DisPlayVendMenu dvend = new DisPlayVendMenu(VendingMachine);
            dvend.Show();
            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        private MenuOptionResult Purchase()
        {
            // Show the categories menu
           PurchaseMenu purch = new PurchaseMenu();
            purch.Show();
          
            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
    }
}
