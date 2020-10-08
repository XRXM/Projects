using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Class
{
    class VendingMachine
    {
        private SortedDictionary<string, List<Product>> inventory;

        public VendingMachine(IEnumerable<Product> productlist)
        {
            inventory = new SortedDictionary<string, List<Product>>();

            foreach (Product product in productlist)
            {
                if (!inventory.ContainsKey(product.Location))
                {
                    inventory[product.Location] = new List<Product>();
                }
                List<Product> list = inventory[product.Location];
                list.Add(product);


            }
        }
        public string[] Locations
        {
            get
            {
                List<string> locations = new List<string>();

                foreach (string location in inventory.Keys)
                {
                
                    locations.Add(location);
                }
                return locations.ToArray();
            }
        }
        public Product[] GetProductsForLocation(string location)
        {
            if (inventory.ContainsKey(location))
            {
                return inventory[location].ToArray();
            }
            return new List<Product>().ToArray();
        }

    }
}
