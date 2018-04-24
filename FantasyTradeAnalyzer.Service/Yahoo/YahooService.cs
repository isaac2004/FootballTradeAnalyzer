using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using FantasyTradeAnalyzer.Model.Dto;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Models;
using System.Threading.Tasks;
using System.Linq;
using FantasyTradeAnalyzer.Respository;
using AutoMapper;
using FantasyTradeAnalyzer.Database.Model;
using System.Collections.Concurrent;
using FantasyTradeAnalyzer.Service.Helpers;
using FantasyTradeAnalyzer.Model;
using Microsoft.Extensions.Caching.Memory;

namespace FantasyTradeAnalyzer.Service.Yahoo
{
    public class YahooService : IYahooService
    {
        private readonly IYahooAuthClient _client;
        private readonly IYahooFantasyClient _fantasy;
        private readonly IProjectionRespository _projectionService;
        private IMemoryCache _cache;
        private MemoryCacheEntryOptions cacheEntryOptions;

        public YahooService(IYahooAuthClient client, IYahooFantasyClient fantasyClient, IProjectionRespository projectionService, IMemoryCache cache)
        {
            _client = client;
            _fantasy = fantasyClient;
            _projectionService = projectionService;
            _cache = cache;

            cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromDays(1));
        }

        public string GetAccessToken()
        {
            return _client.Auth.AccessToken;
        }

        public string GetAuthUrl()
        {
            return _client.GetLoginLinkUri();
        }

        public async Task<IEnumerable<LeagueDto>> GetLeagues()
        {
            var users = await _fantasy.UserResourceManager.GetUserGameLeagues(_client.Auth.AccessToken, new string[] { "371" }, EndpointSubResourcesCollection.BuildResourceList(EndpointSubResources.Settings));
            var leagues = users.GameList.Games.SelectMany(a => a.LeagueList.Leagues).ToList();

            List<ProjectionDto> projections;

            if (!_cache.TryGetValue("Projections", out projections))
            {
                projections = Mapper.Map<List<ProjectionDto>>(await _projectionService.GetProjections());
                _cache.Set("Projections", projections, cacheEntryOptions);
            }

            var leagueTeams = await _fantasy.TeamsCollectionManager.GetLeagueTeams(_client.Auth.AccessToken, leagues.Select(a=> a.LeagueKey).ToArray(), EndpointSubResourcesCollection.BuildResourceList(EndpointSubResources.Roster));


            var leaguesDto = new List<LeagueDto>();
            foreach (var league in leagues)
            {
                league.TeamList = (from a in leagueTeams
                                   where a.LeagueKey == league.LeagueKey
                                   select a.TeamList).FirstOrDefault();

                var tempLeague = new LeagueDto(league);
                projections.PopulateProjectedPoints(tempLeague.Settings);
                var leagueDto = new LeagueDto(league, projections);
                leaguesDto.Add(leagueDto);
            }
            return leaguesDto;
        }

        public async Task<UserInfo> GetUserProfile(NameValueCollection parameters)
        {
            return await _client.GetUserInfo(parameters);
        }

        public async Task<List<WeeklyLineupModel>> BuildRoster(LeagueDto league, TeamDto team, List<PlayerDto> tradedPlayers)
        {
            ConcurrentBag<WeeklyLineupModel> lineupList = new ConcurrentBag<WeeklyLineupModel>();
            Parallel.For(league.StartWeek, league.EndWeek + 1, week =>

            {
                var startingRoster = RosterAssignmentAnalyzer.GetRoster(week, league, team.Roster.Where(a => a.EligiblePositions.Any(b => b.Value.Position.Type == PositionTypeDto.Offense)).ToList());

                var lineup = BuildWeeklyLineupModel(startingRoster, week, tradedPlayers);
                lineupList.Add(lineup);

            });
            foreach (var lineup in lineupList)
            {
                lineup.ProjectedPoints = Math.Round(lineup.ProjectedPoints, 2);
            }
            return lineupList.OrderBy(a => a.Week).ToList();
        }
        private static WeeklyLineupModel BuildWeeklyLineupModel(IDictionary<PositionDto, IList<PlayerDto>> p, int week, List<PlayerDto> tradedPlayers)
        {

            Dictionary<PositionViewModel, List<PlayerLineupModel>> dict = new Dictionary<PositionViewModel, List<PlayerLineupModel>>();
            List<PlayerLineupModel> lineup = new List<PlayerLineupModel>();
            foreach (var r in p)
            {
                if (r.Value.Count == 0)
                {
                    PlayerLineupModel v = new PlayerLineupModel(null, r.Key, week, tradedPlayers);
                    lineup.Add(v);
                }

                foreach (var s in r.Value)
                {
                    PlayerLineupModel v = new PlayerLineupModel(s, r.Key, week, tradedPlayers);
                    lineup.Add(v);
                }
            }


            var weekLineup = new WeeklyLineupModel()
            {
                Week = week,
                ProjectedPoints = p.Where(a => a.Key != PositionDto.Bench)
                                                    .SelectMany(a => a.Value)
                                                    .Select(b => b.WeeklyMatchups[week])
                                                    .Sum(c => Math.Round(c.WeekProjections, 2)),
                Roster = lineup
            };

            return weekLineup;
        }

    }


    public static class ProjectionDtoExtensions
    {
        public static List<ProjectionDto> PopulateProjectedPoints(this List<ProjectionDto> projections, LeagueSettingsDto settings)
        {
            var stats = (from a in settings.StatCategories
                         join b in settings.StatModifiers on a.StatId equals b.StatId
                         where a.PositionType == "O"
                         select new
                         {
                             Key = a.Name.Replace(" ", string.Empty),
                             Value = b.Value
                         }).ToDictionary(b => b.Key, b => b.Value, StringComparer.OrdinalIgnoreCase);

            var props = new ProjectionDto().GetType().GetProperties();

            Parallel.ForEach(projections, proj =>
            {
                double points = 0;
                Parallel.ForEach(props, prop =>
                {

                    var stat = (from a in stats
                                where a.Key == prop.Name
                                select
                                 Convert.ToDouble(prop.GetValue(proj, null)) * a.Value
                                ).FirstOrDefault();

                    points += stat;
                });
                proj.ProjectedPoints = Math.Round(points, 2);

            });
            return projections;
        }
    }
}
