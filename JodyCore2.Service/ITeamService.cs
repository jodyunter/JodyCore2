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
        void Save(Guid identifier, string name, int skill);
        void Create(string name, int skill);
        IList<ITeamViewModel> GetAll();
    }
}
