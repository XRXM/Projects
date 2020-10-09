using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Capstone.Class
{
    public class VendingMachine
    {
        public decimal Balance { get; set; } = 0;

        public VendingMachine(decimal balance)
        {
            Balance = balance;
        }

        internal static string[] location;
        private SortedDictionary<string, Product> inventory;

        public VendingMachine(IEnumerable<Product> productlist)
        {
            this.inventory = new SortedDictionary<string, Product>();

            foreach (Product product in productlist)
            {


                inventory[product.Location] = product;




            }

        }
        public IEnumerable<Product> GetProducts()
        {
            return inventory.Values.ToArray();
        }

        public void FeedMoney(int money)
        {
            Balance += (decimal)money;
        }


        public void SelectProduct(string letter, int number)
        {
            string checker;
            checker = letter + number.ToString();
            //foreach (KeyValuePair kvp in inventory)
            //{
            //    if (inventory.ContainsKey(checker))
            //    {
                    


            //    }
            //    else
            //    {
            //        Console.WriteLine("Selection does not Exsist: Try Again.");
            //    }
            //}




        }
    }
}
