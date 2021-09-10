using JodyCore2.Domain.Bo;
using System.Linq;

namespace JodyCore2.Data.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {

        public Team GetByName(string name, JodyContext context)
        {
            return context.Teams.Where(t => t.Name == name).FirstOrDefault();
        }
    }
}
