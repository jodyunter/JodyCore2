using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class RankingGroupViewModel:IRankingGroupViewModel
    {
        public Guid Identifier { get; set; }

        public string Name { get; set; }
        
        public IList<IRankingViewModel> Rankings { get; set; }

        public RankingGroupViewModel(Guid identifier, string name, IList<IRankingViewModel> rankings)
        {
            Identifier = identifier;
            Name = name;            
            Rankings = rankings.ToList<IRankingViewModel>();
        }
    }
}
