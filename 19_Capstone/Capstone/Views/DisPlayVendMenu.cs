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
        protected override void RebuildMenuOptions()
        {
            ClearOptions();
            string[] location = vendingMachine.Locations;
            foreach (string locations in vendingMachine.Locations)
            {
                AddOption<string>, (locations, );
            }
            AddOption("Back to Main", Exit);
        }
        //private MenuOptionResult ShowProductsForCategory(string locations)
        //{
        //    // Launch the products menu
        //   PurchaseMenu purchMenu = new ProductsMenu(this.store, this.cart, category);
        //    prodMenu.Show();
        //    return MenuOptionResult.DoNotWaitAfterMenuSelection;
        //}
    }
}