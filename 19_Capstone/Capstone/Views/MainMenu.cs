using Capstone.Class;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    class MainMenu : ConsoleMenu
    {
        private VendingMachine VendingMachine;
            
        public MainMenu(VendingMachine VendingMachine)
        {
            VendingMachine = VendingMachine;
        }
    }
}
