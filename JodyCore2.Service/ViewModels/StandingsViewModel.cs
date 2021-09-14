using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.ViewModels
{
    public class StandingsViewModel : IStandingsViewModel
    {
        public Guid Identifier { get; }

        public string Name { get; }

        public int StartYear { get; }        

        public int StartDay { get; }


        public string Description { get; }

        public string Division { get; }

        public IList<IStandingsRecordViewModel> Records { get; }

        public StandingsViewModel(Guid identifier, string name, int startYear, int startDay, string description, string division, IList<IStandingsRecordViewModel> records)
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;            
            StartDay = startDay;            
            Description = description;
            Division = division;
            Records = records;
        }

    }
}
