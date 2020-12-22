using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.DAO
{
    public interface IApplianceDAO
    {
        Appliance GetAppliance(int userId, int applianceId);
        List<Appliance> GetAppliances(int userId, int homeId);
        bool AddAppliance(Appliance appliance);
        bool UpdateAppliance(Appliance appliance);
        bool DeleteAppliance(int user_id, int applianceId);
    }
}
