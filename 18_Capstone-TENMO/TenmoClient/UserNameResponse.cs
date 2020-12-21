using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient
{
    public class UserNameResponse
    {
        
        public string UserNameResponses(int id, Transfers transfers)
        {
            
            if (transfers.account_from == id)
            {
                return $"{transfers.transfer_id} \t {(FromTo)transfers.transfer_type_id}: {TransferService.GetUserById(transfers.account_to).Username} \t {transfers.Amount:c}";
            }
            else
            {
                return $"{transfers.transfer_id} \t {(FromTo)transfers.transfer_type_id}: {TransferService.GetUserById(transfers.account_from).Username} \t {transfers.Amount:c}";
            }
        }
    }
}
