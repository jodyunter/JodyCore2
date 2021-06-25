using JodyCore2.Bo.Domain;
using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using JodyCore2.Service.Mappers;
using JodyCore2.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JodyCore2.Service
{
    public class TeamService : ITeamService
    {
        private ITeamRepository teamRepository;

        public TeamService(ITeamRepository _teamRepository)
        {
            teamRepository = _teamRepository;
        }

        public IList<ITeamViewModel> GetAll()
        {
            using (var context = new JodyContext())
            {
                return teamRepository.GetAll(context).Select(s => TeamMapper.TeamToTeamViewModel(s)).ToList();
            }
        }

        public void Create(string name, int skill)
        {            

            using (var context = new JodyContext())
            {                
                var teamDto = new TeamDto(Guid.NewGuid(), name, skill);
                teamRepository.Create(teamDto, context);

                context.SaveChanges();
            }
        }

        public void Save(Guid identifier, string name, int skill)
        {
            using (var context = new JodyContext())
            {
                var existingDto = teamRepository.GetByIdentifier(identifier, context).FirstOrDefault();

                if (existingDto is null)
                {
                    throw new ApplicationException(string.Format("Team with identifier {0} does not exist.", identifier));
                }

                existingDto.Name = name;
                existingDto.Skill = skill;

                teamRepository.Update(existingDto, context);                

                context.SaveChanges();
            }
        }
    }
}
