using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoClient.Data;

namespace TenmoServer.DAO
{
    public interface IAccountsDAO
    {
        Accounts GetAccountBalance(int AccountId);
    }
}
