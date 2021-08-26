using JodyCore2.Domain;
using JodyCore2.Domain.Bo;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyCore2.Data.Dto
{
    public class GameDto:Game, IGame, IStandingsGame, IBaseDto
    {
        public int Id { get; set; }
        public TeamDto HomeDto { get; set; }
        public TeamDto AwayDto { get; set; }
        public StandingsDto StandingsDto { get; set; }

        [NotMapped]
        public override ITeam Home { get { return HomeDto; } set { HomeDto = (TeamDto)value; } }
        [NotMapped]
        public override ITeam Away { get { return AwayDto; } set { AwayDto = (TeamDto)value; } }
        [NotMapped]
        public IStandings Standings { get { return StandingsDto; } set { StandingsDto = (StandingsDto)value; } }

        public GameDto() { }

        public GameDto(Guid identifier, StandingsDto standingsDto, int year, int day, TeamDto home, TeamDto away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
        {
            Identifier = identifier;
            Day = day;
            Year = year;
            HomeDto = home;
            AwayDto = away;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Complete = complete;
            Processed = processed;
            CanTie = canTie;
            StandingsDto = standingsDto;
        }

        public GameDto(Guid identifier, int year, int day, TeamDto home, TeamDto away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
        {            
            Identifier = identifier;
            Day = day;
            Year = year;
            HomeDto = home;
            AwayDto = away;
            HomeScore = homeScore;
            AwayScore = awayScore;
            Complete = complete;
            Processed = processed;
            CanTie = canTie;
            StandingsDto = null;
        }

        public override bool Equals(object obj)
        {
            return obj is GameDto dto &&
                   Identifier.Equals(dto.Identifier) &&
                   Day == dto.Day &&
                   Year == dto.Year &&
                   EqualityComparer<TeamDto>.Default.Equals(HomeDto, dto.HomeDto) &&
                   EqualityComparer<TeamDto>.Default.Equals(AwayDto, dto.AwayDto) &&
                   HomeScore == dto.HomeScore &&
                   AwayScore == dto.AwayScore &&
                   Complete == dto.Complete &&
                   Processed == dto.Processed &&
                   CanTie == dto.CanTie &&
                   ((StandingsDto == null && dto.StandingsDto == null) || (StandingsDto.Identifier == dto.StandingsDto.Identifier));
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Identifier);
            hash.Add(Day);
            hash.Add(Year);
            hash.Add(HomeDto);
            hash.Add(AwayDto);
            hash.Add(HomeScore);
            hash.Add(AwayScore);
            hash.Add(Complete);
            hash.Add(Processed);
            hash.Add(CanTie);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", Id, Identifier, Day, Year, Home, Away, HomeScore, AwayScore, Complete, Processed, CanTie);
        }
    }
}
