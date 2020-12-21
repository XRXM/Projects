using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private TransferService transferService = new TransferService();
        private UserNameResponse userNameResponse = new UserNameResponse();

        public MainMenu(TransferService transferService, UserNameResponse userNameResponse)
        {
            AddOption("View your current balance", ViewBalance)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View your pending requests", ViewRequests)
                .AddOption("Send TE bucks", SendTEBucks)
                .AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);
        }

        protected override void OnBeforeShow()
        {
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
        }

        private MenuOptionResult ViewBalance()
        {
            int userId = UserService.GetUserId();

            Console.WriteLine($"Your balance is: {AccountService.AccountBalance(userId).Balance:c}");

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            int userId = UserService.GetUserId();
            foreach (Transfers transfers in transferService.ViewTransfers(userId))
            {
                Console.WriteLine(userNameResponse.UserNameResponses(userId, transfers));
            }
            Console.WriteLine("Please enter transfer ID to view details (0 to cancel):");
            int transferIDSelection = Convert.ToInt32(Console.ReadLine());
            if (transferIDSelection == 0)
            {
                return MenuOptionResult.CloseMenuAfterSelection;
            }
            Transfers trans = transferService.GetTransfer(transferIDSelection);
            Console.WriteLine($"Id:{trans.transfer_id}\nFrom: {TransferService.GetUserById(trans.account_from).Username}\nTo: {TransferService.GetUserById(trans.account_to).Username}\nType: {trans.transfer_type_id}\nStatus: {trans.transfer_status_id}\nAmount: {trans.Amount:c}");

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewRequests()
        {
            int userId = UserService.GetUserId();
            foreach(Transfers transfers in transferService.ViewPendingTransfers(userId))
            {
                Console.WriteLine(userNameResponse.UserNameResponses(userId, transfers));
            }
            
            int pendingIDSelection = GetInteger("Please enter transfer ID to approve/reject (0 to cancel): ", null, null);
            Transfers transferRequest = transferService.GetTransfer(pendingIDSelection);
            
            //Console.WriteLine("1: Approve \n2: Reject \n0: Don't approve or reject \n--------- \nPlease choose an option: ");
            int requestIDSelection = GetInteger("1: Approve \n2: Reject \n0: Don't approve or reject \n--------- \nPlease choose an option: ", null, null);

            transferService.ApproveRequest(requestIDSelection, transferRequest);
            if (requestIDSelection == 0)        
            {
                return MenuOptionResult.CloseMenuAfterSelection;
            }
            
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            //List all users
            foreach (API_User user in TransferService.GetListUsers())
            {
                Console.WriteLine($"Username: {user.Username}, UserId: {user.UserId}");
            }
            Console.WriteLine("Enter ID of user you are sending to (0 to cancel):");
            int userIDSelection = Convert.ToInt32(Console.ReadLine());
            if (userIDSelection == 0)
            {
                return MenuOptionResult.CloseMenuAfterSelection;
            }
            Console.WriteLine("Enter amount to transfer: ");
            decimal userAmountSelection = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine(transferService.SendMoney(userIDSelection, UserService.GetUserId(), userAmountSelection));

            return MenuOptionResult.WaitAfterMenuSelection;

        }

        private MenuOptionResult RequestTEBucks()
        {
            foreach (API_User user in TransferService.GetListUsers())
            {
                Console.WriteLine($"Username: {user.Username}, UserId: {user.UserId}");
            }
            Console.WriteLine("Enter ID of user you are requesting from (0 to cancel):");
            int userIDSelection = Convert.ToInt32(Console.ReadLine());
            if (userIDSelection == 0)
            {
                return MenuOptionResult.CloseMenuAfterSelection;
            }

            Console.WriteLine("Enter amount to transfer: ");
            decimal userAmountSelection = Convert.ToDecimal(Console.ReadLine());

            transferService.RequestMoney(UserService.GetUserId(), userIDSelection, userAmountSelection);

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new API_User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }

    }
}
