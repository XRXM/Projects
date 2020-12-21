using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Capstone.Class;
using MenuFramework;
namespace Capstone.Views
{
    public class DisPlayVendMenu : ConsoleMenu
    {
        private VendingMachine vendingMachine;




        public DisPlayVendMenu(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;

            AddOption("Back to Main", Exit);
            Configure(cfg =>
            {
                cfg.Title = "Display Items";
            });
        }
  
    }
}