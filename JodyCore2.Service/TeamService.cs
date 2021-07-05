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
        private IGameRepository gameRepository;

        public TeamService(ITeamRepository _teamRepository, IGameRepository _gameRepository)
        {
            teamRepository = _teamRepository;
            gameRepository = _gameRepository;
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
                if (teamRepository.GetByName(name, context) != null)
                {
                    throw new ApplicationException(string.Format("Team with name {0} already exists.", name));
                }

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

                if (!(existingDto.Name.Equals(name)))
                {
                    if (teamRepository.GetByName(name, context) != null)
                    {
                        throw new ApplicationException(string.Format("Team with name {0} already exists.", name));
                    }
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
                    throw new ApplicationException(string.Format("Team with identifier {0} does not exist.", identifier));
                }

                if (gameRepository.GetAll(context).Where(g => g.HomeDto.Identifier == identifier || g.AwayDto.Identifier == identifier).FirstOrDefault() != null)
                {
                    throw new ApplicationException(string.Format("Games with Team {0} exist. Cannot delete.", identifier));
                }

                teamRepository.Delete(team, context);                

                context.SaveChanges();
            }
        }
    }
}
