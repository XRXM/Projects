﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
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
            decimal moneyD = Convert.ToDecimal(money);
            if (moneyD <= 0)
            {
                Console.WriteLine("\n\tEnter a postive amount");
            }
            else
                Balance += moneyD;
        }


        public void SelectProduct(string checker)
        {

            //string checker;
            //checker = letter + number.ToString();

            foreach (string kvp in inventory.Keys)
            {
                Product myProd = inventory[kvp];


                if (!inventory.ContainsKey(checker))
                {

                    Console.WriteLine("\n\tSelection does not exist: try again.", 10);
                    break;
                }
                else if (kvp == checker && Balance < myProd.Price)
                {
                    Console.WriteLine("\n\tInsufficient Funds");
                    break;
                }
                else if (kvp == checker && myProd.Stock > 0)
                {
                    //assign the product to a vailable from the inventory[key]              
                    Balance -= myProd.Price;
                    myProd.Stock -= 1;
                    Console.WriteLine($"\n\t\t\t\t\t{MakeSound(myProd.ProductType)}");
                    Log(myProd.ProductName, kvp, myProd.Price);
                    break;
                }
                else if (kvp == checker && myProd.Stock == 0)
                {
                    Console.WriteLine("\n\tOut of stock", 10);
                    break;
                }



            }
        }
        public string MakeSound(string type)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            ti.ToTitleCase(type);
            if (type == "Chip")
            {
                return "Crunch Crunch, Yum!";
            }
            else if (type == "Candy")
            {
                return "Munch Munch, Yum!";
            }
            else if (type == "Drink")
            {
                return "Glug Glug, Yum!";
            }
            else if (type == "Gum")
            {
                return "Chew Chew, Yum!";
            }
            else
                return null;
        }



        public string Makechange(decimal change)
        {
            const int quarter = 25;
            const int dime = 10;
            const int nickle = 5;

            change *= 100;
            int intChange = (int)change;
            //Decimal.ToInt32((int)change);
            int quarters = intChange / quarter;
            int dimes = (intChange % quarter) / dime;
            int nickles = ((intChange % quarter) % dime) / nickle;

            return $"\n\n Your change is {quarters} quarter(s), {dimes} dime(s), and {nickles} nickle(s).";

        }

        // put the strings by par of menue  make the decimal (balance - balance feedmoney) and  (balance + price for purchase)
        public void Log(string a, string b, decimal num)
        {
            string path = @"..\..\..\..\Log.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                if (a == "FEED")
                {
                    writer.WriteLine($"{DateTime.Now} {a} {b} {Balance - num:C} {Balance:C}");
                }
                else if (a == "GIVE")
                {
                    writer.WriteLine($"{DateTime.Now} {a} {b} {num:C} {0:C}");
                }
                else
                {
                    writer.WriteLine($"{DateTime.Now} {a} {b} {Balance + num:C} {Balance:C}");
                }
            }
        }



    }
}
