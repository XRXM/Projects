using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IVendorDAO
    {
        List<Vendor> GetStores(int appId);
        bool AddVendor(Home home, int homeId);
        bool AddStore(Home home, int homeId);
        List<Vendor> GetVendors(int homeId);
        bool AddNewStore(Vendor vendor);
        List<Vendor> GetStoreList(int homeId);
        bool UpdateStore(Vendor vendor);
        bool DeleteStore(int storeId);
        List<Vendor> GetServiceList(int homeId);
        bool AddNewService(Vendor service);
        bool UpdateService(Vendor service);
        bool DeleteService(int serviceId);
    }
}
