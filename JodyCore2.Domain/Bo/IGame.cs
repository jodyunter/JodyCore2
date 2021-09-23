using JodyCore2.Domain.Bo.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo
{
    //todo add a game stats class to keep track of all game stats
    public interface IGame:IScheduleGame, IBO
    {        
        int HomeScore { get; }
        int AwayScore { get; }
        bool Complete { get; }
        bool Processed { get; }
        bool CanTie { get; }
        void Play(Random r);
        void Process();
        ITeam GetWinner();
        ITeam GetLoser();
    }
}
