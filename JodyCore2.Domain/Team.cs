using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain
{
    public class Team : ITeam
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }
    }
}
