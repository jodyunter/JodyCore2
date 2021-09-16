using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class SimpleCompetitionViewModel : ISimpleCompetitionViewModel
    {
        public Guid Identifier { get; set; }

        public string Name { get; set; }

        public SimpleCompetitionViewModel(Guid identifier, string name)
        {
            Identifier = identifier;
            Name = name;
        }
    }
}
