using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public interface IStandingsViewModel
    {
        public Guid Identifier { get; }
        public string Name { get; }
        public int StartYear { get; }
        public int StartDay { get; }        
        public string Description { get; }
        public string Division { get; }
        public IList<IStandingsRecordViewModel> Records { get; }

    }
}
