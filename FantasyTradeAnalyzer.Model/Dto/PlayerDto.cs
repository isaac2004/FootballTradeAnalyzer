using FantasyTradeAnalyzer.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using YahooFantasyWrapper.Models;

namespace FantasyTradeAnalyzer.Model.Dto
{
    [DataContract]
    public class PlayerDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Team { get; set; }
        [DataMember]
        public string DisplayPosition { get; set; }
        [DataMember]
        public IDictionary<PositionAbbreviationDto, RosterPositionDto> EligiblePositions { get; private set; }
        [DataMember]
        public PositionDto WeekPosition { get; set; }
        [DataMember]
        public double ProjectedPoints { get; set; }
        [DataMember]
        public List<int> ByeWeek { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public bool selected { get; set; }
        [DataMember]
        public Dictionary<int, MatchupWeekDto> WeeklyMatchups { get; set; }

        public PlayerDto() { }

        public PlayerDto(Player player, List<ProjectionDto> projections, int week)
        {
            this.Name = player.Name.Full;
            this.Team = player.EditorialTeamFullName;
            this.Status = player.Status;
            this.DisplayPosition = player.DisplayPosition;
            this.EligiblePositions = new Dictionary<PositionAbbreviationDto, RosterPositionDto>();
            this.ByeWeek = player.ByeWeeks.Week.Select(a => Convert.ToInt32(a)).ToList();
            foreach (var position in player.EligiblePositions.Position)
            {
                PositionDto pos = PositionDto.GetPosition(position);
                var rosterPosition = new RosterPositionDto(pos, 1);

                this.EligiblePositions.Add(rosterPosition.Position.Abbreviation, rosterPosition);
            }


            this.WeeklyMatchups = (from a in projections
                                   where NormalizeNames(a.Name, this.Name) && a.Position == this.DisplayPosition
                                   select new KeyValuePair<int, MatchupWeekDto>(
                                       a.Week.Value, new MatchupWeekDto
                                       {
                                           WeekProjections = a.ProjectedPoints,
                                           Opponent = ""
                                       }
                                       )).ToDictionary(t => t.Key, t => t.Value);

            this.ProjectedPoints = (from a in this.WeeklyMatchups
                                    where a.Key >= week
                                    select Math.Round(a.Value.WeekProjections, 2)).Sum();

            this.ProjectedPoints = Math.Round(this.ProjectedPoints, 2);
        }

        private static bool NormalizeNames(string name1, string name2)
        {
            string[] strip = { ".", "jr", "sr" };
            foreach (var s in strip)
            {
                name1 = name1.ToLower().Replace(s.ToLower(), "");
                name2 = name2.ToLower().Replace(s.ToLower(), "");
            }

            string[] romans = { "III", "II", "IV", "V" };

            foreach (var roman in romans)
            {
                if (name1.Trim().EndsWith(roman.ToLower()))
                {
                    name1 = name1.ToLower().Replace(roman.ToLower(), "");
                }

                if (name2.Trim().EndsWith(roman.ToLower()))
                {
                    name2 = name2.ToLower().Replace(roman.ToLower(), "");
                }
            }

            return name1.Trim() == name2.Trim();
        }
    }
    [DataContract]
    public class MatchupWeekDto
    {
        [DataMember]
        public double WeekProjections { get; set; }
        [DataMember]
        public string Opponent { get; set; }

        public MatchupWeekDto() { }
    }
}
