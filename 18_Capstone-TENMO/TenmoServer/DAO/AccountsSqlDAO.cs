using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoClient.Data;

namespace TenmoServer.DAO
{
    public class AccountsSqlDAO : IAccountsDAO
    {
        //Accounts account = new Accounts();

        private readonly string connectionString;
        public AccountsSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Accounts GetAccountBalance(int user_Id)
        {
            Accounts account = new Accounts();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"select a.account_id,  a.user_id, a.balance from accounts a join users u on u.user_id = a.user_id where a.user_id = @user_id";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@user_id", user_Id);


                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        //Accounts account = new Accounts();
                        account.AccountId = Convert.ToInt32(rdr["account_id"]);
                        account.UserId = Convert.ToInt32(rdr["user_id"]);
                        account.Balance = Convert.ToDecimal(rdr["balance"]);
                    }
                }
            
            }
            catch (SqlException)
            {
                throw;
            }
                return account;
            
        }
    }
}
