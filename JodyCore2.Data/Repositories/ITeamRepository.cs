using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public interface ITeamRepository
    {
        TeamDto GetByName(string name, JodyContext context);
        TeamDto GetByIdentifier(Guid identifier, JodyContext context);
        IList<TeamDto> GetAll(JodyContext context);
        TeamDto Create(TeamDto newTeam, JodyContext context);
        TeamDto Update(TeamDto team, JodyContext context);
    }
}
