using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
   public class PurchaseMenu : ConsoleMenu
    {
        public PurchaseMenu()
        {
            AddOption("Feed Money", FeedMoney);
            //AddOption("Select Product", SelectProduct);
            //AddOption("Finish Transaction", FinishTransaction);
        }

        private MenuOptionResult FeedMoney()
        {
            Decimal moneyDec = 0;
            //// Show the categories menu
            PurchaseMenu feedMoney = new PurchaseMenu();
            feedMoney.Show();
         
            Console.WriteLine("Enter a Whole Dollar Amount");
            string money =  Console.ReadLine();
            moneyDec = System.Convert.ToDecimal(money);
            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}
