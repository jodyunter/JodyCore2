using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competitions
{
    public abstract class Competition : ICompetition
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
        public string Description { get; set; }
        public CompetitionType CompetitionType { get; set; }
        public abstract void ProcessGame(ICompetitionGame game);

        public Competition(CompetitionType type) { CompetitionType = type; }

        public Competition(Guid identifier, string name, int startYear, int endYear, int startDay, int endDay, string description, CompetitionType type) : this(type)
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;
            EndYear = endYear;
            StartDay = startDay;
            EndDay = endDay;
            Description = description;
        }
    }
}
