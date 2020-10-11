using Capstone.Class;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class PurchaseMenu : ConsoleMenu
    {

        private VendingMachine vendingMachine;





        public PurchaseMenu(VendingMachine vendingMachine)
        {


            this.vendingMachine = vendingMachine;

            AddOption("\t(1) Feed Money", FeedMoney);
            AddOption("\t(2) Select Product", SelectProduct);
            AddOption("\t(3) Finish Transaction", FinishTransaction);
            AddOption("\t(4) Back To Purchase Menu", Exit);
            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Cyan;
                cfg.SelectedItemForegroundColor = ConsoleColor.White;
                cfg.Selector = " ~~>";
            });


        }
        protected override void OnBeforeShow()
        {
            Console.WriteLine();
            Console.WriteLine($" \t******Current Balance {vendingMachine.Balance:C}******");
        }
        private MenuOptionResult FeedMoney()
        {
            int money = GetInteger("\n\n Enter a Whole Dollar Amount");
            vendingMachine.FeedMoney(money);
            vendingMachine.Log("FEED", "MONEY", (money));
            //vending machine
            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        private MenuOptionResult SelectProduct()
        {
            string[] headings = { " Location", "Product Name", "Amount", "Stock" };

            //Console.WriteLine();
            //Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($" {headings[0],-20} {headings[1],20} {headings[2],20} {headings[3],20}");
            Console.WriteLine($"{new string('_', 20)} {new string('_', 20)} {new string('_', 20)} {new string('_', 20)} ");
            Console.WriteLine($"\t\t\t\tCurrent Balance {vendingMachine.Balance:C}");
            Console.WriteLine($"{new string('_', 85)}");
            foreach (Product product in vendingMachine.GetProducts())
            {
                Console.WriteLine($" {product.Location,-20} {product.ProductName,20} {product.Price,20:C}{product.Stock,20} ");
            }

            Console.WriteLine($"{new string('_', 85)}");

            string select = GetString("\tMake selection by choosing location :Letter,number:  ", true, null);

            vendingMachine.SelectProduct(select.ToUpper());


            return MenuOptionResult.WaitAfterMenuSelection;
        }
        private MenuOptionResult FinishTransaction()
        {
            Console.WriteLine(vendingMachine.Makechange(vendingMachine.Balance));
            vendingMachine.Log("GIVE", "CHANGE", (vendingMachine.Balance));
            vendingMachine.Balance = 0;

            //Console.ReadLine();
            return MenuOptionResult.WaitThenCloseAfterSelection;
        }

    }
}
