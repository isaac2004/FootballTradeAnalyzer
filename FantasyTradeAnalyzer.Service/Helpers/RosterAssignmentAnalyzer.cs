using FantasyTradeAnalyzer.Model;
using FantasyTradeAnalyzer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyTradeAnalyzer.Service.Helpers
{
    public class RosterAssignmentAnalyzer
    {
        private readonly IDictionary<PositionAbbreviationDto, RosterPositionDto> _rosterPositions;
        private readonly IList<PlayerDto> _availablePlayers;

        public RosterAssignmentAnalyzer(IDictionary<PositionAbbreviationDto, RosterPositionDto> rosterPositions, IList<PlayerDto> availablePlayers)
        {
            if (availablePlayers.Count > rosterPositions.Sum(rp => rp.Value.Count))
            {
                //throw new ArgumentException("There are more players than roster positions available.");
            }

            _rosterPositions = rosterPositions;
            _availablePlayers = availablePlayers;
        }
        public IDictionary<PositionDto, IList<PlayerDto>> GetOptimalAssignment(int week)
        {
            // Determine possible players for each position
            var assignments = DeterminePossiblePlayerAssignments(week);

            // Start making assignments
            return AssignPlayers(assignments);
        }

        private IDictionary<RosterPositionDto, List<PlayerDto>> DeterminePossiblePlayerAssignments(int week)
        {
            var assignments = new Dictionary<RosterPositionDto, List<PlayerDto>>();

            foreach (var rosterPosition in _rosterPositions)
            {
                if (rosterPosition.Value.Position.Type == PositionTypeDto.Offense)
                {
                    var possiblePlayers = new List<PlayerDto>();
                    foreach (var player in _availablePlayers)
                    {
                        if (rosterPosition.Value.Position.CanBeFilledBy(player))
                        {
                            possiblePlayers.Add(player);
                        }
                    }

                    var matchups = possiblePlayers.
                           Where(a => a.ByeWeek.FirstOrDefault() != week);

                    List<PlayerDto> p = new List<PlayerDto>();
                    foreach (var player in matchups)
                    {
                        var playerWeek = player.WeeklyMatchups.Where(a => a.Key == week && a.Value != null).FirstOrDefault();

                        if (playerWeek.Value != null)
                        {
                            p.Add(player);
                        }
                    }

                    assignments[rosterPosition.Value] = p.OrderByDescending(x => x.WeeklyMatchups
                                            .FirstOrDefault()
                                            .Value
                                            .WeekProjections).ToList();

                }
            }

            return assignments;
        }

        private IDictionary<PositionDto, IList<PlayerDto>> AssignPlayers(
            IDictionary<RosterPositionDto, List<PlayerDto>> orderedAssignments)
        {
            var finalAssignments = new Dictionary<PositionDto, IList<PlayerDto>>();
            List<PlayerDto> availablePlayers = _availablePlayers.ToList();

            foreach (var assignment in orderedAssignments)
            {
                var rosterPosition = assignment.Key;
                var playerQueue = assignment.Value;
                var assignedPlayers = new List<PlayerDto>();

                int required = rosterPosition.Count;

                while (required > 0 && playerQueue.Count > 0)
                {
                    var player = playerQueue.Take(1).FirstOrDefault();
                    playerQueue.Remove(player);
                    if (availablePlayers.Contains(player))
                    {
                        assignedPlayers.Add(player);
                        availablePlayers.Remove(player);
                        required--;
                    }
                }

                finalAssignments[rosterPosition.Position] = assignedPlayers;
            }


            finalAssignments.Add(PositionDto.Bench, new List<PlayerDto>());
            foreach (var player in availablePlayers)
            {
                finalAssignments[PositionDto.Bench].Add(player);
            }

            return finalAssignments;
        }

        public static IDictionary<PositionDto, IList<PlayerDto>> GetRoster(int week, LeagueDto league, IList<PlayerDto> players)
        {
            var depthAnalyzer = new RosterDepthAnalyzer(league.Settings.RosterPositions, players);
            return depthAnalyzer.GetStartingRoster(week);
        }
    }
}
