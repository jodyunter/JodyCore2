using JodyCore2.Domain.Bo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JodyCore2.Data.Repositories
{
    public class GameRepository : BaseGameRepository<Game>, IGameRepository
    {

    }
}
