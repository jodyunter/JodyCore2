using JodyCore2.Domain.Bo;

namespace JodyCore2.Data.Repositories.Teams
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        Team GetByName(string name, JodyContext context);

    }
}
