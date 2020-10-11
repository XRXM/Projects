using Capstone.Class;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass()]
    class SoundTest
    {

        public decimal Balance { get; set; }
       

        [DataTestMethod]
        [DataRow("Chip", "Crunch Crunch, Yum!")]
        [DataRow("Candy", "Munch Munch, Yum!")]
        [DataRow("drink", "Glug Glug, Yum!")]
        [DataRow("chiP", "Crunch Crunch, Yum!")]
        [DataRow("gum", "Chew Chew, Yum!")]
        public void MakeSoundTest(string str, string expected)
        {
            VendingMachine ex = new VendingMachine(Balance);

            string actual = ex.MakeSound(str);

            Assert.AreEqual(expected, actual);

        }
    }
}
