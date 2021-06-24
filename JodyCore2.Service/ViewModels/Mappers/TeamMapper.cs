using JodyCore2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels.Mappers
{
    public class TeamMapper
    {
        public static ITeam TeamViewModelToTeam(ITeamViewModel model)
        {
            return new Team(model.Identifier, model.Name, model.Skill);
        }

        public static ITeamViewModel TeamToTeamViewModel(ITeam team)
        {
            return new TeamViewModel(team.Identifier, team.Name, team.Skill);
        }
    }
}
