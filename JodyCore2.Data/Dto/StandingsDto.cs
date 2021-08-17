using JodyCore2.Domain;
using JodyCore2.Domain.Bo.Standings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public class StandingsDto : Standings, IStandings, IBaseDto
    {
        public int Id { get; set; }

        public IList<StandingsRecordDto> RecordsDto { get; set; }

        [NotMapped]
        public override IList<IStandingsRecord> Records { get { return RecordsDto.ToList<IStandingsRecord>(); }  set { RecordsDto = value.Select(r => (StandingsRecordDto)r).ToList(); } }

        public StandingsDto() { }

        public StandingsDto(Guid identifier, string name, int startYear, int endYear, int startDay, int endDay, string description, string division, IList<StandingsRecordDto> records):base()            
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;
            EndYear = endYear;
            StartDay = startDay;
            EndDay = endDay;
            Description = description;
            Division = division;
            RecordsDto = records;
        }
    }
}
