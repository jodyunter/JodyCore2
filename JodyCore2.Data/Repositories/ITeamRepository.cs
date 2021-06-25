using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public interface ITeamRepository:IBaseRepository<TeamDto>
    {
        TeamDto GetByName(string name, JodyContext context);        
        IList<TeamDto> GetAll(JodyContext context);

    }
}
