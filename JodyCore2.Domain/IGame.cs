using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain
{
    public interface IGame
    {
        Guid Identifier { get; }
        int Day { get; }
        int Year { get; }
        ITeam Home { get; }
        ITeam Away { get; }
        int HomeScore { get; }
        int AwayScore { get; }
        bool Complete { get; }
        bool Processed { get; }
        bool CanTie { get; }

        void Play(Random r);
    }
}
