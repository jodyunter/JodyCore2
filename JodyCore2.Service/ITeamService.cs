using JodyCore2.Domain;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public interface ITeamService
    {
        ITeamViewModel Save(Guid identifier, string name, int skill);
        ITeamViewModel Create(string name, int skill);
        ITeamViewModel GetByIdentifier(Guid identifier);
        ITeamViewModel GetByName(string name);
        IList<ITeamViewModel> GetAll();
    }
}
