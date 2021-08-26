using System;
using System.Linq;

namespace JodyCore2.Domain.Bo.Standings
{
    public static class StandingsExtensions
    {
        public static int DefaultGetPoints(this IStandingsRecord record, IStandingsRecord thatRecord)
        {
            return record.Wins * 2 + record.Ties;
        }    
     
        public static void DefaultProcessGame(this IStandings standings, IGame game)
        {
            if (!game.Complete)            
                throw new ApplicationException("Can't process an incomplete game.");


            if (game.Processed)
                throw new ApplicationException("Can't process and already processed game.");
                

            var homeTeamRecord = standings.GetRecord(game.Home);
            var awayTeamRecord = standings.GetRecord(game.Away);

            if (game.HomeScore > game.AwayScore)
            {
                homeTeamRecord.RegulationWins++;
                awayTeamRecord.RegulationLoses++;
            }
            else if (game.HomeScore < game.AwayScore)
            {
                homeTeamRecord.RegulationLoses++;
                awayTeamRecord.RegulationWins++;
            }
            else
            {
                homeTeamRecord.Ties++;
                awayTeamRecord.Ties++;
            }

            homeTeamRecord.GoalsFor += game.HomeScore;
            awayTeamRecord.GoalsAgainst += game.HomeScore;
            homeTeamRecord.GoalsAgainst += game.AwayScore;
            awayTeamRecord.GoalsFor += game.AwayScore;

            game.Process();
            
        }
    }
}
