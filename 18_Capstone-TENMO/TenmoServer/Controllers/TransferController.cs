using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoClient.Data;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Services;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private IUserDAO userDAO;
        private TransferServices transferServices;
        private IAccountsDAO accountsDAO;
        private ITransferDAO transferDAO;

        public TransferController(IUserDAO userDAO, TransferServices transferServices, IAccountsDAO accountsDAO, ITransferDAO transferDAO)
        {
            this.userDAO = userDAO;
            this.transferServices = transferServices;     //Don't need a DAO with transferServices in server
            this.accountsDAO = accountsDAO;
            this.transferDAO = transferDAO;
        }

        [HttpGet]
        public List<User> GetListUsers()
        {
            return userDAO.GetUsers();
        }

        [HttpGet("user{id}")]
        public User GetUserById(int id)
        {
            return userDAO.GetUserById(id);
        }

        [HttpGet("{id}")]
        public List<Transfers> ViewTransfers(int id)
        {
            return transferDAO.ViewTransfers(id);
        }

        [HttpGet("transfer{id}")]
        public Transfers GetTransfer(int id)
        {
            return transferDAO.GetTransfer(id);
        }

        [HttpGet("pending{id}")]
        public List<Transfers> ViewPendingTransfers(int id)
        {
            return transferDAO.ViewPendingTransfers(id);
        }

        [HttpPost("send")]
        public string SendMoney(Transfers transfers)     
        {
            return transferServices.SendMoney(transfers);     
        }

        [HttpPost("request")]
        public string RequestMoney(Transfers transfers)
        {
            return transferServices.RequestMoney(transfers);
        }

        [HttpPut("{statusSelection}")]
        public string ApproveRequest(int statusSelection, Transfers transfers)
        {
            return transferServices.ApproveRequest(statusSelection, transfers);
        }
    }
}
