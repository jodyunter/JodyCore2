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
        public override ITeam Home { get { return HomeDto; } set { HomeDto = (TeamDto)value; } }
        [NotMapped]
        public override ITeam Away { get { return AwayDto; } set { AwayDto = (TeamDto)value; } }

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

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}", Id, Identifier, Day, Year, Home, Away, HomeScore, AwayScore, Complete, Processed, CanTie);
        }
    }
}
