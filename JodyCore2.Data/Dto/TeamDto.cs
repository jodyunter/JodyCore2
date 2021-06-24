using JodyCore2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class TeamDto : Team, ITeam, BaseDto
    {
        public int Id { get; set; }

        public TeamDto() { }

        public TeamDto(ITeam team):base(team.Identifier, team.Name, team.Skill)
        {
            
        }
    }
}
