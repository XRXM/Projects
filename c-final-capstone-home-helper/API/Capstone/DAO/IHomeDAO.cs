using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.DAO
{
    public interface IHomeDAO
    {
        Home GetHome(int userId, int homeid);
        List<Home> GetHomes(int userId);
        int AddHome(Home home);
        Milestones GetMilestones(int homeId);
        bool AddMileStones(int homeId, int year, int purchaseDate);
        bool UpdateMilestones(int homeId, int buildYear, int purchaseYear);
        bool UpdateHome(Home home);
        bool DeleteHome(int userId, int homeId);
        bool DeleteMilestones(int homeId);

    }
}
