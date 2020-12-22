using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IRepairDAO
    {
        Repair GetRepair(int userId, int repairId);
        List<Repair> GetRepairs(int userId, int homeId);
        bool AddRepair(Repair repair);
        bool DeleteRepair(int userId, int repairId);
        bool UpdateRepair(Repair repair);
    }
}
