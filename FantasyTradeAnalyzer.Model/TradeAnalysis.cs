using FantasyTradeAnalyzer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FantasyTradeAnalyzer.Model
{
    //[DataContract]
    //public class WeeklyLineup
    //{
    //    [DataMember]
    //    // public IDictionary<PositionDto, IList<PlayerDto>> Roster { get; set; }
    //    public IList<PlayerDto> Roster { get; set; }
    //    [DataMember]
    //    public int Week { get; set; }
    //    [DataMember]
    //    public double ProjectedPoints { get; set; }
    //}
    [DataContract]
    public class WeeklyLineupModel
    {
        [DataMember]
        public int Week { get; set; }
        [DataMember]
        public List<PlayerLineupModel> Roster { get; set; }
        [DataMember]
        public double ProjectedPoints { get; set; }

    }
    [DataContract]
    public class PlayerLineupModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public PositionViewModel Position { get; set; }
        [DataMember]
        public double ProjectedPoints { get; set; }
        [DataMember]
        public List<int> ByeWeek { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public MatchupWeekDto Matchup { get; set; }
        [DataMember]
        public bool BeingTraded { get; set; }

        public PlayerLineupModel(PlayerDto t, PositionDto p, int currentWeek, List<PlayerDto> tradedPlayers)
        {
            this.BeingTraded = false;
            if (p != null)
            {
                this.Position = new PositionViewModel()
                {
                    Abbreviation = p.Abbreviation.ToString(),
                    FullName = p.DisplayName,
                    PositionType = p.Type,
                    IsFlex = p.IsFlex
                };
            }

            if (t == null)
            {
                this.Name = string.Empty;

                this.Status = string.Empty;
                this.ByeWeek = new List<int> { 0 };

                this.ProjectedPoints = 0;
                this.Matchup = new MatchupWeekDto()
                {
                    Opponent = string.Empty,
                    WeekProjections = 0
                };
            }



            if (t != null && p != null)
            {
                this.Name = t.Name;

                this.Status = t.Status;
                this.ByeWeek = t.ByeWeek;

                this.ProjectedPoints = Math.Round(t.ProjectedPoints, 2);
                this.Matchup = (from a in t.WeeklyMatchups
                                where a.Key == currentWeek
                                select
                                    new MatchupWeekDto()
                                    {
                                        Opponent = a.Value.Opponent,
                                        WeekProjections = Math.Round(a.Value.WeekProjections, 2)
                                    }
                                ).FirstOrDefault();
            }
            if (tradedPlayers.Select(a => a.Name).ToList().Contains(this.Name) && this.Name != string.Empty)
            {
                this.BeingTraded = true;
            }
        }
    }
    [DataContract]
    public class PositionViewModel
    {
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public bool IsFlex { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }
        [DataMember]
        public PositionTypeDto PositionType { get; set; }
    }
}
