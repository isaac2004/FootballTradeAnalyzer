using FantasyTradeAnalyzer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FantasyTradeAnalyzer.Service.Helpers
{
    public class RosterDepthAnalyzer
    {
        private readonly IDictionary<PositionAbbreviationDto, RosterPositionDto> _rosterPositions;
        private readonly IList<PlayerDto> _availablePlayers;

        public RosterDepthAnalyzer(IDictionary<PositionAbbreviationDto, RosterPositionDto> rosterPositions, IList<PlayerDto> availablePlayers)
        {
            _rosterPositions = rosterPositions;
            _availablePlayers = availablePlayers;
        }

        public IDictionary<PositionDto, IList<PlayerDto>> GetStartingRoster(int week)
        {
            IList<PlayerDto> availablePlayers = new List<PlayerDto>();

            foreach (var player in _availablePlayers)
            {
                if (player != null )
                {
                    availablePlayers.Add(player);
                }
            }

            // Determine the optimal roster assignments
            var assignmentAnalyzer = new RosterAssignmentAnalyzer(_rosterPositions, availablePlayers);
            var optimalAssignments = assignmentAnalyzer.GetOptimalAssignment(week);

            return optimalAssignments;
        }
    }
}
