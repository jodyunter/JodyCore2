using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Domain.Bo.Playoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Xunit.Domain.Bo.Playoffs
{
    public class TestPlayoffSeries : PlayoffSeries
    {
        public TestPlayoffSeries() : base(SeriesType.BestOf) { }
        public override IList<ICompetitionGame> CreateGames()
        {
            throw new NotImplementedException();
        }

        public override ITeam GetLoser()
        {
            throw new NotImplementedException();
        }

        public override ITeam GetWinner()
        {
            throw new NotImplementedException();
        }

        public override bool IsComplete()
        {
            throw new NotImplementedException();
        }

        public override void ProcessGame(ICompetitionGame game)
        {
            throw new NotImplementedException();
        }
    }
}
