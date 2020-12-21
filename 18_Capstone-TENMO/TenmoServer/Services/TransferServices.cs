using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TenmoClient.Data;
using TenmoServer.DAO;

namespace TenmoServer.Services
{
    public class TransferServices
    {
        private IAccountsDAO accountsDAO;
        private ITransferDAO transferDAO;

        public TransferServices(IAccountsDAO accountsDAO, ITransferDAO transferDAO)
        {
            this.accountsDAO = accountsDAO;
            this.transferDAO = transferDAO;
        }


        public string SendMoney(Transfers transfers)  
        {

            Accounts senderObject = accountsDAO.GetAccountBalance(transfers.account_from);
            Accounts receiverObject = accountsDAO.GetAccountBalance(transfers.account_to);

            if (senderObject.Balance >= transfers.Amount && transfers.account_to != transfers.account_from)
            {
                receiverObject.Balance += transfers.Amount;
                senderObject.Balance -= transfers.Amount;

                transferDAO.UpdateBalance(transfers.account_from, senderObject.Balance);
                transferDAO.UpdateBalance(transfers.account_to, receiverObject.Balance);


                transferDAO.LogTransfers(TransferType.Send, TransferStatus.Approved, senderObject.AccountId, receiverObject.AccountId, transfers.Amount);

                return "Transfer was successful";               //Make a more elegant response if time allows
            }
            else if (senderObject.Balance < transfers.Amount)
            {
                return "Your balance is insufficient to send the desired amount of money";     //CHANGE LATER IF NEEDED
            }
            else if (transfers.account_to == transfers.account_from)        //could take out
            {
                return "You cannot send money to yourself.";
            }
            else
            {
                return "Unsuccessful transfer.";
            }
        }

        public string RequestMoney(Transfers transfers)
        {
            Accounts senderObject = accountsDAO.GetAccountBalance(transfers.account_from);
            Accounts requesterObject = accountsDAO.GetAccountBalance(transfers.account_to);

            if (senderObject.Balance >= transfers.Amount && transfers.account_to != transfers.account_from)
            {

                transferDAO.LogTransfers(TransferType.Request, TransferStatus.Pending, requesterObject.AccountId, senderObject.AccountId, transfers.Amount);   //Approve/reject prior to handling balance updates

                return "Your request was successfully made.";
            }
            else if (senderObject.Balance < transfers.Amount)
            {
                return "Your balance is insufficient to send the desired amount of money.";
            }
            else if (transfers.account_to == transfers.account_from)
            {
                return "You cannot request money from yourself.";
            }
            else
            {
                return "Unsuccessful request.";
            }
        }

        public string ApproveRequest(int statusSelection, Transfers transfers)
        {
            Accounts senderObject = accountsDAO.GetAccountBalance(transfers.account_to);
            Accounts requesterObject = accountsDAO.GetAccountBalance(transfers.account_from);           

            if (statusSelection == 1)
            {
                transferDAO.UpdateStatus(transfers.transfer_id, 2);

                requesterObject.Balance += transfers.Amount;
                senderObject.Balance -= transfers.Amount;

                transferDAO.UpdateBalance(transfers.account_to, senderObject.Balance);
                transferDAO.UpdateBalance(transfers.account_from, requesterObject.Balance);

                return "Your request has been approved.";
            }
            else if (statusSelection == 2)
            {
                transferDAO.UpdateStatus(transfers.transfer_id, 3);

                return "Your request has been denied.";
            }
            else
            {
                return "The request is still pending";
            }
        }
    }
}
