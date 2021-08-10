using JodyCore2.Data.Repositories;
using JodyCore2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ConsoleApp
{
    public class Services
    {
        public ITeamService TeamService { get; set; }

        public Services()
        {
            var teamRepository = new TeamRepository();
            var gameRepository = new GameRepository();

            TeamService = new TeamService(teamRepository, gameRepository);
        }
        
    }
}
