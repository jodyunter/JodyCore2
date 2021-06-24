using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public TeamDto Create(TeamDto newTeam, JodyContext context)
        {
            context.Add(newTeam);

            return newTeam;
        }

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

        public TeamDto Update(TeamDto team, JodyContext context)
        {
            context.Update(team);

            return team;
        }
    }
}
