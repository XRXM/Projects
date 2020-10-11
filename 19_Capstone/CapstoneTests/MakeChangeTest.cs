using Capstone.Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass()]
    public class MakeChangeTest
    {

        [DataTestMethod]
        [DataRow("a1",5,15,0,0)]
        public void ChangeTest(string select,int feed, int expectedQ, int expectedD, int expectedN)
        {
            //arrange
            List<Product> products = new List<Product>()
            {
                new Product("A1", "Mountain Dew", 1.25m, "Drink", 5),
                new Product("A2", "Scotch", 5.00m, "Drink", 5),
                new Product("D1", "Double Mint", 0.25m, "Gum", 5),
            };


            VendingMachine ex = new VendingMachine(products);
            ex.FeedMoney(feed);
            ex.SelectProduct(select);

            //Act
           Change actual = ex.FinishTransaction();

            //assert
            Assert.AreEqual(expectedQ, actual.Quarters);
            Assert.AreEqual(expectedD, actual.Dimes);
            Assert.AreEqual(expectedN, actual.Nickles);





        }
    }
}
