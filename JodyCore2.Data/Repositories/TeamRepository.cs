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

        public IList<TeamDto> GetAll(JodyContext context)
        {
            return context.Teams.ToList();
        }

        public TeamDto GetByIdentifier(Guid identifier, JodyContext context)
        {
            return context.Teams.Where(t => t.Identifier == identifier).FirstOrDefault();
        }

        public TeamDto GetByName(string name, JodyContext context)
        {
            return context.Teams.Where(t => t.Name == name).FirstOrDefault();
        }
    }
}
