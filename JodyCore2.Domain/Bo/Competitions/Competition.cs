using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Domain.Bo.Competitions
{
    public abstract class Competition : ICompetition
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }        
        public int StartDay { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public bool Setup { get; set; }
        public bool Started { get; set; }
        public bool Complete { get; set; }
        public bool Processed { get; set; }
        public CompetitionType CompetitionType { get; set; }
        public abstract void ProcessGame(ICompetitionGame game);
        public abstract IList<ICompetitionGame> CreateGames();
        public virtual void SetupCompetition()
        {
            Setup = true;
        }
        public virtual void StartCompetition()
        {
            Started = true;
        }
        public virtual void CompleteCompetition()
        {
            Complete = true;
        }
        public virtual void ProcessCompetition()
        {
            Processed = true;
        }


        public Competition(CompetitionType type) { CompetitionType = type; }

        public Competition(Guid identifier, string name, int startYear, int startDay, int order,  string description, bool setup, bool started, bool complete, bool processed, CompetitionType type) : this(type)
        {
            Identifier = identifier;
            Name = name;
            StartYear = startYear;            
            StartDay = startDay;
            Order = order;
            Description = description;
            Setup = setup;
            Started = started;
            Complete = complete;
            Processed = processed;
        }

        public override bool Equals(object obj)
        {
            return obj is Competition competition &&
                   Identifier.Equals(competition.Identifier) &&
                   Name == competition.Name &&
                   StartYear == competition.StartYear &&
                   StartDay == competition.StartDay &&
                   Order == competition.Order &&
                   Description == competition.Description &&
                   Setup == competition.Setup &&
                   Started == competition.Started &&
                   Complete == competition.Complete &&
                   Processed == competition.Processed &&
                   CompetitionType == competition.CompetitionType;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Identifier);
            hash.Add(Name);
            hash.Add(StartYear);
            hash.Add(StartDay);
            hash.Add(Order);
            hash.Add(Description);
            hash.Add(Setup);
            hash.Add(Started);
            hash.Add(Complete);
            hash.Add(Processed);
            hash.Add(CompetitionType);
            return hash.ToHashCode();
        }
    }
}
