using FantasyTradeAnalyzer.Database.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using YahooFantasyWrapper.Models;

namespace FantasyTradeAnalyzer.Model.Dto
{
    [DataContract]
    public class LeagueDto
    {
        [DataMember]
        public string LeagueKey { get; set; }

        [DataMember]
        public int LeagueId { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int CurrentWeek { get; set; }
        [DataMember]
        public int EndWeek { get; set; }
        [DataMember]
        public int StartWeek { get; set; }
        [DataMember]
        public LeagueSettingsDto Settings { get; set; }
        [DataMember]
        public List<TeamDto> Teams { get; set; }

        public LeagueDto() { }

        public LeagueDto(League league)
        {
            this.LeagueId = Convert.ToInt32(league.LeagueId);
            this.LeagueKey = league.LeagueKey;
            this.Name = league.Name;
            this.CurrentWeek = Convert.ToInt32(league.CurrentWeek);
            this.StartWeek = Convert.ToInt32(league.StartWeek);
            this.EndWeek = Convert.ToInt32(league.EndWeek);

            this.Settings = new LeagueSettingsDto(league.Settings);
        }


        public LeagueDto(League league, List<ProjectionDto> projections)
        {
            this.LeagueId = Convert.ToInt32(league.LeagueId);
            this.LeagueKey = league.LeagueKey;
            this.Name = league.Name;
            this.CurrentWeek = Convert.ToInt32(league.CurrentWeek);
            this.StartWeek = Convert.ToInt32(league.StartWeek);
            this.EndWeek = Convert.ToInt32(league.EndWeek);

            this.Settings = new LeagueSettingsDto(league.Settings);

            this.Teams = new List<TeamDto>();
            foreach (var team in league.TeamList.Teams)
            {
                this.Teams.Add(new TeamDto(team, projections, this.CurrentWeek));
            }
        }
    }
    [DataContract]
    public class LeagueSettingsDto
    {
        [DataMember]
        public IDictionary<PositionAbbreviationDto, RosterPositionDto> RosterPositions { get; private set; }
        [DataMember]
        public List<StatCategoryDto> StatCategories { get; set; }
        [DataMember]

        public List<StatModifierDto> StatModifiers { get; set; }

        public LeagueSettingsDto() { }

        public LeagueSettingsDto(Settings settings)
        {
            this.RosterPositions = new Dictionary<PositionAbbreviationDto, RosterPositionDto>();

            foreach (var position in settings.RosterPositions.RosterPosition)
            {
                PositionDto pos = PositionDto.GetPosition(position.Position);
                int count = Convert.ToInt32(position.Count);
                var rosterPosition = new RosterPositionDto(pos, count);

                this.RosterPositions.Add(rosterPosition.Position.Abbreviation, rosterPosition);
            }

            this.StatCategories = new List<StatCategoryDto>();

            foreach (var category in settings.StatCategories.Stats.Stat)
            {
                this.StatCategories.Add(new StatCategoryDto(category));
            }

            this.StatModifiers = new List<StatModifierDto>();

            foreach (var modifier in settings.StatModifiers.Stats.Stat)
            {
                this.StatModifiers.Add(new StatModifierDto(modifier));
            }
        }
    }
}
