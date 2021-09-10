using JodyCore2.Data.Repositories;
using JodyCore2.Data.Repositories.Games;
using JodyCore2.Data.Repositories.Teams;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public static class ServicesSetup
    {
        public static void SetupServicesAndRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IGameRepository, GameRepository>();


        }
    }
}
