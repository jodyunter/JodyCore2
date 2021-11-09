using JodyCore2.Data.Repositories.Games;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Competitions;
using JodyCore2.Service;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.ProcessWork.Competitions.Standing
{
    public class NewStandingsProcess
    {
        public AppService AppService { get; set; }

        public NewStandingsProcess()
        {
            AppService = new AppService();
        }        

        public void CreateTeams()
        {
            var nameList = new List<string>()
            {
                "Team 1",
                "Team 2",
                "Team 3",
                "Team 4",
                "Team 5",
                "Team 6",
                "Team 7",
                "Team 8",
                "Team 9",
                "Team 10",
                "Team 11",
                "Team 12",
                "Team 13",
                "Team 14",
                "Team 15",
                "Team 16",
                "Team 17",
                "Team 18",
                "Team 19",
                "Team 20",
                "Team 21"
            };

            nameList.ForEach(name => {
                AppService.TeamService.Create(name, 5);
            });
        }

        //these are configuration groups not competition groups
        public void CreateRankingGroups()
        {
            var div1List = new List<string>() { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6", "Team 7" };
            var div2List = new List<string>() { "Team 8", "Team 9", "Team 10", "Team 11", "Team 12", "Team 13", "Team 14" };
            var div3List = new List<string>() { "Team 15", "Team 16", "Team 17", "Team 18", "Team 19", "Team 20", "Team 21" };

            var teams = AppService.TeamService.GetAll();

            var div1Teams = teams.Where(t => div1List.Contains(t.Name)).ToList();
            var div2Teams = teams.Where(t => div2List.Contains(t.Name)).ToList();
            var div3Teams = teams.Where(t => div3List.Contains(t.Name)).ToList();

            AppService.RankingService.CreateRankingGroup("Main", teams);
            AppService.RankingService.CreateRankingGroup("Division 1", div1Teams);
            AppService.RankingService.CreateRankingGroup("Division 2", div2Teams);
            AppService.RankingService.CreateRankingGroup("Division 3", div3Teams);

        }

        public void CreateCompetitionRankingGroups(ISimpleCompetitionViewModel competition, IRankingGroupViewModel model)
        {
            AppService.RankingService.CreateCompetitionRakingGroupFromRankingGroup(model.Identifier, new List<Guid>() { competition.Identifier });
        }

        public void Scratch()
        {
            CreateTeams();
            CreateRankingGroups();
            //Create base teams
            //Create base ranking groups

            //Choose Teams
            //Set Year and starting day
            //Create the standings            
            //Create competition ranking groups            
            //create Competition Games

            int year = 1;
            string name = "Season 12";
            int day = 1;
            

        }

        public void CreateStandings(string name, int year, int day, IList<ITeamViewModel> teams)
        {
            
        }
    }
}
