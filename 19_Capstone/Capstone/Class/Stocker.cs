using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Class
{
    public static class Stocker 
    {
       

        public static IEnumerable<Product> StockProducts(string filePath)
        {
            int stock = 5;
            try
            {
                List<Product> products = new List<Product>();
                
                using (StreamReader rdr = new StreamReader(filePath))
                {
                    while (!rdr.EndOfStream)
                    {
                        string input = rdr.ReadLine();
                        string[] fields = input.Split("|");                       
                        string location = fields[0];
                        string productName = fields[1];
                        decimal price = decimal.Parse(fields[2]);
                        string productType = fields[3];
                        Product prod = new Product(location, productName, price, productType, stock);
                       
                        products.Add(prod);
                    }
                }
               
                return products;
            }
            catch
            {
                return new List<Product>();
            }

        }
    }
}
