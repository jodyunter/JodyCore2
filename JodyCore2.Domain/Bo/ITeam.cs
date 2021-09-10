using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo
{
    public interface ITeam
    {
        Guid Identifier { get; set; }
        string Name { get; set; }
        int Skill { get; set; }
    }
}
