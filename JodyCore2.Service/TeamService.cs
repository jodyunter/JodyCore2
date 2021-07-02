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

        public ITeamViewModel Create(string name, int skill)
        {            

            using (var context = new JodyContext())
            {                
                var teamDto = new TeamDto(Guid.NewGuid(), name, skill);
                teamRepository.Create(teamDto, context);

                context.SaveChanges();

                return TeamMapper.TeamToTeamViewModel(teamDto);
            }
            
        }

        public ITeamViewModel Save(Guid identifier, string name, int skill)
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

                return TeamMapper.TeamToTeamViewModel(existingDto);
            }
        }

        public ITeamViewModel GetByIdentifier(Guid identifier)
        {
            using (var context = new JodyContext())
            {
                var team = teamRepository.GetByIdentifier(identifier, context).FirstOrDefault();

                if (team == null)
                {
                    throw new ApplicationException(string.Format("Team with identifier {0} does not exist.", identifier));
                }

                return TeamMapper.TeamToTeamViewModel(team);
            }
        }

        public ITeamViewModel GetByName(string name)
        {
            using (var context = new JodyContext())
            {
                var team = teamRepository.GetByName(name, context);

                if (team == null)
                {
                    throw new ApplicationException(string.Format("Team with name {0} does not exist.", name));
                }

                return TeamMapper.TeamToTeamViewModel(team);
            }
        }

        public void Delete(Guid identifier)
        {
            using (var context = new JodyContext())
            {
                var team = teamRepository.GetByIdentifier(identifier, context).FirstOrDefault();

                if (team == null)
                {
                    throw new ApplicationException(string.Format("Team with id {0} does not exist.", identifier));
                }

                teamRepository.Delete(team, context);                

                context.SaveChanges();
            }
        }
    }
}
