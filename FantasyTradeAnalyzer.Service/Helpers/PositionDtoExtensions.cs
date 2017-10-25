using FantasyTradeAnalyzer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyTradeAnalyzer.Service.Helpers
{
    internal static class PositionDtoExtensions
    {
        public static bool CanBeFilledBy(this PositionDto position, PlayerDto player)
        {
            foreach (var eligiblePosition in player.EligiblePositions)
            {
                if (eligiblePosition.Equals(position))
                {
                    return true;
                }

                string[] possiblePositions = position.DisplayName.Split('/');

                foreach (var possiblePosition in possiblePositions)
                {
                    if (eligiblePosition.Value.Position.DisplayName.Equals(possiblePosition))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
