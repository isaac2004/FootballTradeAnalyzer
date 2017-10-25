using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client
{
    public interface IYahooFantasyClient
    {
        Task<User> GetUser(string AccessToken);
        Task<IList<Game>> GetUserGames(string[] gameKeys, EndpointSubResourcesCollection subresources, string AccessToken);
        Task<IList<League>> GetLeagueTeams(string[] leagueKeys, EndpointSubResourcesCollection subresources, string AccessToken);
        Task<IList<League>> GetLeagues(string[] leagueKeys, EndpointSubResourcesCollection subresources, string AccessToken);

        //Task<User> GetUser(NameValueCollection parameters);
        //Task<IList<Game>> GetUserGames(string[] gameKeys, EndpointSubResources[] subresources);
        //Task<Game> GetGame(string gameKey, EndpointSubResources[] subresources);
        //Task<IList<Game>> GetUserGame(string[] gameKeys, string[] subresources, NameValueCollection parameters);
        //Task<Team> GetTeam(string teamKey, NameValueCollection parameters);
        //Task<LeagueTeamCollection> GetTeams(string leagueKey, NameValueCollection parameters);
        //Task<LeagueTeamPlayerCollection<Player>> GetLeagueTeamPlayers(string leagueKey, NameValueCollection parameters);
        //Task<LeagueCollection> GetLeagues(GameCode gameCode, NameValueCollection parameters);
        //Task<LeagueCollection> GetLeagues(int gameId, NameValueCollection parameters);
        //Task<LeagueDraftResultCollection> GetDraftResults(string leagueKey, NameValueCollection parameters);
        //Task<TeamRosterPlayerCollection> GetRosterPlayers(string teamKey, int? week, NameValueCollection parameters);
        //Task<TeamPlayerCollection<PlayerWithStats>> GetTeamPlayerStats(string teamKey, NameValueCollection parameters);
        //Task<LeagueSettings> GetLeagueSettings(string leagueKey, NameValueCollection parameters);
        //Task<ICollection<Game>> GetUserGames(NameValueCollection parameters);
        //Task<ICollection<Matchup>> GetMatchups(string teamKey, NameValueCollection parameters);

    }
}
