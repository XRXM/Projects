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
    [Authorize]
    public class VendorController : Controller
    {
        private IUserDAO userDAO;
        private IHomeDAO homeDAO;
        private IVendorDAO vendorDAO;

        public VendorController(IHomeDAO homeDAO, IUserDAO userDAO, IVendorDAO vendorDAO)
        {
            this.homeDAO = homeDAO;
            this.userDAO = userDAO;
            this.vendorDAO = vendorDAO;
        }
        [HttpGet("/map/{id}")]
        public List<Vendor> GetVendors(string id)
        {
            int vendorId = Convert.ToInt32(id.Substring(1));
            List<Vendor> vendors = new List<Vendor>(); 

            if (id.Contains("r"))
            {
                vendors = vendorDAO.GetVendors(vendorId);
                
            }
            else
            {
                vendors = vendorDAO.GetStores(vendorId);
            }

            return vendors;

        }

        [HttpGet("/stores/{id}")]
        public List<Vendor> GetStores(int id)
        {

            List<Vendor> vendors = new List<Vendor>();            
            
                vendors = vendorDAO.GetStoreList(id);
           

            return vendors;

        }

        [HttpPost("/stores/:homeId")]

        public ActionResult AddNewStore(Vendor vendor)
        {

            vendorDAO.AddNewStore(vendor);
            return Created(vendor.VendorName, null);
        }

        [HttpPost("/stores/{homeId}")]
        public ActionResult AddStore(Vendor store)
        {
            vendorDAO.AddNewStore(store);
            return Created(store.VendorName, null);

        }

        [HttpPut("/stores/{homeId}")]
        public ActionResult UpdateStore(Vendor store)
        {
            vendorDAO.UpdateStore(store);
            return Ok();

        }

        [HttpDelete("/stores/{storeId}")]
        public ActionResult DeleteStore(int storeId)
        {
            vendorDAO.DeleteStore(storeId);
            return Ok();
        }
        [HttpGet("/services/{homeid}")]
        public List<Vendor> GetServiceList(int homeid)
        {
            List<Vendor> vendors = new List<Vendor>();
            vendors = vendorDAO.GetServiceList(homeid);

            return vendors;
        }
        [HttpPost("/services/{homeId}")]
        public ActionResult AddService(Vendor service)
        {
            vendorDAO.AddNewService(service);
            return Created(service.VendorName, null);

        }

        [HttpPut("/services/{homeId}")]
        public ActionResult UpdateService(Vendor service)
        {
            vendorDAO.UpdateService(service);
            return Ok();

        }

        [HttpDelete("/services/{serviceId}")]
        public ActionResult DeleteService(int serviceId)
        {
            vendorDAO.DeleteService(serviceId);
            return Ok();

        }
    }

}
