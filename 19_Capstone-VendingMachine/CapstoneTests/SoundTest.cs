using Capstone.Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass()]
    public class SoundTest
    {

        
       

        [DataTestMethod]
        [DataRow("Chip", "Crunch Crunch, Yum!")]
        [DataRow("Candy", "Munch Munch, Yum!")]
        [DataRow("drink", "Glug Glug, Yum!")]
        [DataRow("chiP", "Crunch Crunch, Yum!")]
        [DataRow("gum", "Chew Chew, Yum!")]
        public void MakeSoundTest(string str, string expected)
        {
            List<Product> products = new List<Product>()
            {
                new Product("A1", "Mountain Dew", 1.25m, "Drink", 5),
                new Product("A2", "Scotch", 5.00m, "Drink", 5),
                new Product("D1", "Double Mint", 0.25m, "Gum", 5),
            };


            VendingMachine ex = new VendingMachine(products);

            string actual = ex.MakeSound(str);

            Assert.AreEqual(expected, actual);

        }
    }
}
