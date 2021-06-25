using JodyCore2.Domain;
using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class TeamDto:Team, ITeam, IBaseDto
    {
        public int Id { get; set; }

        public TeamDto() { }

        public TeamDto(Guid identifier, string name, int skill)
        {
            Identifier = identifier;
            Name = name;
            Skill = skill;
        }

        public override bool Equals(object obj)
        {
            return obj is TeamDto dto &&
                   Identifier.Equals(dto.Identifier) &&
                   Name == dto.Name &&
                   Skill == dto.Skill;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identifier, Name, Skill);
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", Id, Identifier, Name, Skill);
        }
    }
}
