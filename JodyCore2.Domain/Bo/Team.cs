using JodyCore2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Bo.Domain
{
    public class Team : ITeam
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
