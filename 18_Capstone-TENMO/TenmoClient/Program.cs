using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Views;

namespace TenmoClient
{
    class Program
    {

        static void Main(string[] args)
        {
            TransferService transferService = new TransferService();
            AuthService authService = new AuthService();
            UserNameResponse userNameResponse = new UserNameResponse();
            new LoginRegisterMenu(authService, transferService, userNameResponse).Show();

            Console.WriteLine("\r\nThank you for using TEnmo!!!\r\n");
        }
    }
}
