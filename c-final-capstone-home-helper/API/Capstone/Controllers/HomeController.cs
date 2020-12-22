using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private IUserDAO userDAO;
        private IHomeDAO homeDAO;
        private IVendorDAO vendorDAO;
        private int homeid;
        private int UserId
        {
            get
            {
                return Convert.ToInt32(User.Claims.FirstOrDefault(cl => cl.Type == "sub").Value);
            }
        }
        private string UserName
        {
            get
            {
                return User.Identity.Name;
            }
        }

        public HomeController(IHomeDAO homeDAO, IUserDAO userDAO, IVendorDAO vendorDAO)
        {

            this.homeDAO = homeDAO;
            this.userDAO = userDAO;
            this.vendorDAO = vendorDAO;
        }

        [HttpPut("/homedetail/{homeid}")]
        [Authorize]
        public ActionResult<List<Reminder>> UpdateHome(Home home)
        {
            if (home.UserId != UserId)
            {
                return Forbid();
            }
            homeDAO.UpdateHome(home);
            homeDAO.UpdateMilestones(home.HomeId, home.BuildYear, home.PurchaseYear);
            return Ok();
        }


        [HttpDelete("/homedetail/{homeid}")]
        [Authorize]
        public ActionResult DeleteHome(int homeId)
        {
            if (homeDAO.GetHome(UserId, homeId).HomeId == homeId)
            {
                if (homeDAO.DeleteMilestones(homeId))
                {
                    if (homeDAO.DeleteHome(UserId, homeId))
                    {
                        return NoContent();
                    }
                }
            }
                        
            
                return NotFound();
                     
        }

        [HttpGet("/myhomes")]
        [Authorize]
        public List<Home> GetHomes()
        {
            return homeDAO.GetHomes(UserId);
        }

        [HttpPost("/addhome")]

        [Authorize]
        public IActionResult AddHome(Home home)
        {
            home.UserId = this.UserId;
            this.homeid = homeDAO.AddHome(home);
            homeDAO.AddMileStones(homeid, home.BuildYear, home.PurchaseYear);
            vendorDAO.AddVendor(home, homeid);
            vendorDAO.AddStore(home, homeid);
            return Created(home.StreetAddress, null);
        }



    }
}
