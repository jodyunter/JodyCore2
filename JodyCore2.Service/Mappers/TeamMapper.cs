using JodyCore2.Domain.Bo;
using JodyCore2.Service.ViewModels;

namespace JodyCore2.Service.Mappers
{
    public class TeamMapper
    {

        public static ITeamViewModel TeamToTeamViewModel(ITeam team)
        {
            return new TeamViewModel(team.Identifier, team.Name, team.Skill);
        }

    }
}
