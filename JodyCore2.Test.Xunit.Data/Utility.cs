using JodyCore2.Domain.Bo;
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
            return new Standings(Guid.NewGuid(), "Standings Name", 1, 1, 1, "Test", "Test this", new List<IStandingsRecord>(), null, false, false, false, false);
        }
    }

    public class TestCompetition : Competition
    {
        public TestCompetition(CompetitionType type):base(type) { CompetitionType = type; }

        public TestCompetition(Guid identifier, string name, int startYear, int startDay, int order, string description, bool setup, bool started, bool complete, bool processed, IList<ICompetitionRankingGroup> groups, CompetitionType type) 
            :base(identifier, name, startYear, startDay, order, description, setup, started, complete, processed, groups, type)
        { 

        }

        public override ICompetitionGame CreateGame(int year, int day, ITeam home, ITeam away)
        {
            throw new NotImplementedException();
        }

        public override IList<ICompetitionGame> CreateGames(IList<ICompetitionGame> games)
        {
            throw new NotImplementedException();
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            throw new NotImplementedException();
        }

        public override void SetupCompetition()
        {
            throw new NotImplementedException();
        }

        public override void StartCompetition()
        {
            throw new NotImplementedException();
        }
    }
}
