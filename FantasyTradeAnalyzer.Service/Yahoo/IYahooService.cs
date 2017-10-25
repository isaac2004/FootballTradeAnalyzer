using FantasyTradeAnalyzer.Model;
using FantasyTradeAnalyzer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;

namespace FantasyTradeAnalyzer.Service.Yahoo
{
    public interface IYahooService
    {
        string GetAuthUrl();
        Task<UserInfo> GetUserProfile(NameValueCollection parameters);

        Task<IEnumerable<LeagueDto>> GetLeagues();

        Task<List<WeeklyLineupModel>> BuildRoster(LeagueDto league, TeamDto team, List<PlayerDto> tradedPlayers);

        string GetAccessToken();

    }
}
