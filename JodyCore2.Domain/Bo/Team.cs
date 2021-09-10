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
    }
}
