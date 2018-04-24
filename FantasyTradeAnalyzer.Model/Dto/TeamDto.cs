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
    public class TeamDto
    {
        [DataMember]
        public string TeamName { get; set; }
        [DataMember]
        public int TeamId { get; set; }
        [DataMember]
        public List<PlayerDto> Roster { get; set; }
        [DataMember]
        public double ProjectedPoints { get; set; }
        [DataMember]
        public bool IsMyTeam { get; set; }

        public TeamDto() { }

        public TeamDto(Team team, List<ProjectionDto> projections, int week)
        {
            this.TeamName = team.Name;
            this.IsMyTeam = team.IsOwnedByCurrentLogin;
            this.TeamId = Convert.ToInt32(team.TeamId);

            this.Roster = new List<PlayerDto>();
            var badPositions = new string[] { "K", "DEF" };
            foreach (var player in team.Roster.PlayerList.Players)
            {
                var playerDto = new PlayerDto(player, projections, week);
                if (!badPositions.Contains(playerDto.DisplayPosition))
                {
                    this.Roster.Add(playerDto);
                }
            }
            this.Roster = this.Roster.OrderByDescending(a => a.ProjectedPoints).ToList();
            this.ProjectedPoints = Math.Round(this.Roster.Sum(a => a.ProjectedPoints), 2);
        }
    }
}
