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

        public int EndYear { get; }

        public int StartDay { get; }

        public int EndDay { get; }

        public string Description { get; }

        public string Division { get; }

        public IList<IStandingsRecordViewModel> Records { get; }

        public StandingsViewModel(Guid identifier, string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<IStandingsRecordViewModel> records)
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;
            EndYear = endYear;
            StartDay = startDay;
            EndDay = endDay;
            Description = description;
            Division = division;
            Records = records;
        }
    }
}
