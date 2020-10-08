using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Class
{
    public class Product
    {
        public string Location { get; set; }
        public string ProductName { get; set; }
        public decimal  Price { get; set; }
        public string ProductType { get; set; }
        public int Stock { get; set; } = 5;

        public  Product(string location, string productName, decimal price, string productType, int stock)
        {
            Location = location;
            ProductName = productName;
            Price = price;
            ProductType = productType;
            Stock = stock;
        }
    }
}
