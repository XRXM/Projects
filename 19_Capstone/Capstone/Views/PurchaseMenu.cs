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
            
            AddOption("Feed Money", FeedMoney);
            AddOption("Select Product", SelectProduct);
            //AddOption("Finish Transaction", FinishTransaction);
            AddOption("Back To Purchase Menu", Exit);
           


        }
        protected override void OnBeforeShow()
        {
            Console.WriteLine($"Current Balance {vendingMachine.Balance :C}");
        }
        private MenuOptionResult FeedMoney()
        {
         
           
           

            int money = GetInteger("Enter a Whole Dollar Amount");
            vendingMachine.FeedMoney(money);
            //vending machine
            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        private MenuOptionResult SelectProduct()
        {
            foreach (Product product in vendingMachine.GetProducts())
            {
                Console.WriteLine($"{product.Location,-15} {product.ProductName,10} {product.Price,20:C} ");
            }
            Console.WriteLine($"{new string('_', 56)}");
            string select = GetString("Make selection by choosing location :Letter,number",true,null);
            vendingMachine.SelectProduct(select.ToUpper());
            
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        
    }
}
