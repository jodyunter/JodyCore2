using System;

namespace JodyCore2.Domain.Bo
{
    public class Team : ITeam, IBO
    {        
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }

        public Team() { }

        public Team(Guid identifier, string name, int skill)
        {
            Identifier = identifier;
            Name = name;
            Skill = skill;
        }

        public override bool Equals(object obj)
        {
            return obj is Team team &&
                   Identifier.Equals(team.Identifier) &&
                   Name == team.Name &&
                   Skill == team.Skill;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Identifier, Name, Skill);
        }
    }
}
