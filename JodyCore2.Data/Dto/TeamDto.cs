using JodyCore2.Bo.Domain;
using JodyCore2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class TeamDto:IBaseDto
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
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
    }
}
