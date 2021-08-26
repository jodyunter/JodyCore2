using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Rankings
{    
    public interface IRanking
    {
        Guid Identifier { get;  }
        ITeam Team { get; }
        int Rank { get; }
        IRankingGroup Group { get; }

    }
}
