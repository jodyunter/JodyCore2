using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Data
{
    public static class Utility
    {

        public static IStandings CreateStandingsNoRecords()
        {
            return new Standings(Guid.NewGuid(), "Standings Name", 1, 1, 1, 250, "Test", "Test this", new List<IStandingsRecord>());
        }
    }

    public class TestCompetition : Competition
    {
        public TestCompetition(CompetitionType type):base(type) { CompetitionType = type; }

        public TestCompetition(Guid identifier, string name, int startYear, int endYear, int startDay, int endDay, string description, CompetitionType type) : this(type)
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;
            EndYear = endYear;
            StartDay = startDay;
            EndDay = endDay;
            Description = description;
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            throw new NotImplementedException();
        }
    }
}
