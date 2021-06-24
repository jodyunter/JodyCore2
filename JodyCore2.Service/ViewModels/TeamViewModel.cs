using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class TeamViewModel : ITeamViewModel
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int Skill { get; set; }

        public TeamViewModel() { }

        public TeamViewModel(Guid identifier, string name, int skill)
        {
            Identifier = identifier;
            Name = name;
            Skill = skill;
        }
    }
}
