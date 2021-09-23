using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo
{
    public interface ITeam: IBO
    {        
        string Name { get; set; }
        int Skill { get; set; }
    }
}
