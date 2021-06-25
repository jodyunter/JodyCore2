using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class TeamRepository : BaseRepository<TeamDto>, ITeamRepository
    {

        public TeamDto GetByName(string name, JodyContext context)
        {
            return context.Teams.Where(t => t.Name == name).FirstOrDefault();
        }
    }
}
