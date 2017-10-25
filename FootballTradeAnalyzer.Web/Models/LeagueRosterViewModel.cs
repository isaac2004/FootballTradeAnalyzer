using FantasyTradeAnalyzer.Model;
using FantasyTradeAnalyzer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyTradeAnalyzer.Web.Models
{
    public class LeagueRosterViewModel
    {
        public string LeagueKey { get; set; }

        public int LeagueId { get; set; }

        public string Name { get; set; }

        public int CurrentWeek { get; set; }

        public int EndWeek { get; set; }

        public int StartWeek { get; set; }

        public List<TeamRosterViewModel> Teams { get; set; }
    }

    public class MatchupWeekModel
    {
        public double WeekProjections { get; set; }

        public string Opponent { get; set; }
    }

    public class PlayerViewModel
    {
        public string Name { get; set; }

        public PositionViewModel Position { get; set; }

        public double ProjectedPoints { get; set; }

        public List<int> ByeWeek { get; set; }

        public string Status { get; set; }

        public Dictionary<int, MatchupWeekModel> WeeklyMatchups { get; set; }

        public PlayerViewModel(PlayerDto t, PositionDto p)
        {
            if (p != null)
                this.Position = new PositionViewModel()
                {
                    Abbreviation = p.Abbreviation.ToString(),
                    FullName = p.DisplayName,
                    PositionType = p.Type
                };
            if (t == null)
            {
                this.Name = string.Empty;
                this.Status = string.Empty;
                this.ByeWeek = new List<int>() { 0 };
                this.ProjectedPoints = 0.0;
                this.WeeklyMatchups = (from a in Enumerable.Range(1, 17)
                                       select new KeyValuePair<int, MatchupWeekModel>(

                                           a,
                                           new MatchupWeekModel()
                                           {
                                               Opponent = string.Empty,
                                               WeekProjections = 0
                                           }
                                       )).ToDictionary(a => a.Key, a => a.Value);
            }
            if (t == null || p == null)
                return;

            this.Name = t.Name;
            this.Status = t.Status;
            this.ByeWeek = t.ByeWeek;
            this.ProjectedPoints = Math.Round(t.ProjectedPoints, 2);
            this.WeeklyMatchups = (from a in t.WeeklyMatchups
                                   select new KeyValuePair<int, MatchupWeekModel>(

                                       a.Key,
                                       new MatchupWeekModel()
                                       {
                                           Opponent = a.Value.Opponent,
                                           WeekProjections = Math.Round(a.Value.WeekProjections, 2)
                                       }
                                   )).ToDictionary(a => a.Key, a => a.Value);
        }
    }


    public class PositionViewModel
    {
        public string FullName { get; set; }

        public string Abbreviation { get; set; }

        public PositionTypeDto PositionType { get; set; }
    }

    public class TeamRosterViewModel
    {
        public string TeamName { get; set; }

        public int TeamId { get; set; }

        public List<PlayerViewModel> Roster { get; set; }

        public double ProjectedPoints { get; set; }

        public bool IsMyTeam { get; set; }
    }

    public class TradeBreakdownViewModel
    {
        public List<WeeklyLineupModel> MyBeforeTradeLineup { get; set; }

        public List<WeeklyLineupModel> MyAfterTradeLineup { get; set; }

        public List<WeeklyLineupModel> ThierBeforeTradeLineup { get; set; }

        public List<WeeklyLineupModel> ThierAfterTradeLineup { get; set; }
    }
    public class ViewModel
    {
        public string Token { get; set; }

        public IEnumerable<LeagueDto> Leagues { get; set; }
    }
    public class ProposedTradeModel
    {
        public TeamDto MyTeam { get; set; }

        public TeamDto ThierTeam { get; set; }

        public PlayerDto[] myPlayers { get; set; }

        public PlayerDto[] thierPlayers { get; set; }

        public LeagueDto selectedLeague { get; set; }
    }
}
