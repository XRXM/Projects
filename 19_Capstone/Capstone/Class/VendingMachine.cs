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


        public void SelectProduct(string checker)
        {

            //string checker;
            //checker = letter + number.ToString();
            
            foreach (string kvp in inventory.Keys)
            {
                Product myProd = inventory[kvp];

                if (Balance < myProd.Price)
                {
                    Console.WriteLine("Insufficient Funds");
                    break;
                }
                else if (kvp == checker && myProd.Stock > 0)
                {
                    //assign the product to a vailable from the inventory[key]              
                    Balance -= myProd.Price;
                    myProd.Stock -= 1;
                    MakeSound(myProd.ProductType);
                    break;
                }
                else if (kvp == checker && myProd.Stock == 0)
                {
                    Console.WriteLine("Out of stock", 10);
                    break;
                }
                else if (kvp != checker)
                {
                    Console.WriteLine("Selection does not Exsist: Try Again.", 10);
                    break;
                }
            }

            void MakeSound(string type)
            {
                if (type == "Chip")
                {
                    Console.WriteLine("Crunch Crunch, Yum!");
                }
                else if (type == "Candy")
                {
                    Console.WriteLine("Munch Munch, Yum!");
                }
                else if (type == "Drink")
                {
                    Console.WriteLine("Glug Glug, Yum!");
                }
                else
                {
                    Console.WriteLine("Chew Chew, Yum!");
                }
            }


            //public void MakeChange(decimal change)
            //{
            //    const decimal quarters = 25;
            //    const decimal dimes = 10;
            //    const decimal nickles = 1;
            //    change *= 100;


            //}


        }
    }
}
