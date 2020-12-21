using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoClient.Data;
using TenmoServer.DAO;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountsDAO accntDAO;

        public AccountController(IAccountsDAO accountDAO)
        {
            this.accntDAO = accountDAO;
        }

        [HttpGet("{id}")]
        public ActionResult<Accounts> GetAccountBalance(int id)         //change to get MY account balance 
        {
            Accounts account = accntDAO.GetAccountBalance(id);
            if (account == null)
            {
                return NotFound();
            }
            else
            {
                return account;          
            }

        }


    }
}
