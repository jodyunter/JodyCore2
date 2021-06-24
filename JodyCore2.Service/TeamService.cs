using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Domain;
using JodyCore2.Service.ViewModels;
using JodyCore2.Service.ViewModels.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service
{
    public class TeamService : ITeamService
    {
        public IList<ITeamViewModel> GetAll()
        {
            using (var context = new JodyContext())
            {
                return context.Teams.Select(t => TeamMapper.TeamToTeamViewModel(t)).ToList();
            }
        }

        public void Save(ITeamViewModel teamModel)
        {
            using (var context = new JodyContext())
            {
                var existingDto = context.Teams.Where(t => t.Identifier == teamModel.Identifier).FirstOrDefault();

                if (existingDto is null)
                {
                    var dto = new TeamDto(TeamMapper.TeamViewModelToTeam(teamModel));
                    context.Add(dto);
                }
                else
                {
                    existingDto.Name = teamModel.Name;
                    existingDto.Skill = teamModel.Skill;
                }

                context.SaveChanges();
            }
        }
    }
}
