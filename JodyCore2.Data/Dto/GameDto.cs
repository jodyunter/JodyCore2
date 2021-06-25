using JodyCore2.Domain;
using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JodyCore2.Data.Dto
{
    public class GameDto:Game, IGame, IBaseDto
    {
        public int Id { get; set; }
        public TeamDto HomeDto { get; set; }
        public TeamDto AwayDto { get; set; }
        [NotMapped]
        public new ITeam Home { get; set; }
        [NotMapped]
        public new ITeam Away { get; set; }

        public GameDto() { }

        public GameDto(Guid identifier, int day, int year, TeamDto home, TeamDto away, int homeScore, int awayScore, bool complete, bool processed, bool canTie)
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
                   CanTie == dto.CanTie;
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
    }
}
