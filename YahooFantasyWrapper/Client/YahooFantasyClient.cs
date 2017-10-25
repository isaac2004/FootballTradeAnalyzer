using System.Threading.Tasks;
using System.Net;
using YahooFantasyWrapper.Models;
using System.Reflection;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Infrastructure;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;

namespace YahooFantasyWrapper.Client
{
    public class YahooFantasyClient : IYahooFantasyClient
    {
        private readonly IRequestFactory _factory;

        public YahooFantasyClient(IRequestFactory factory)
        {
            _factory = factory;
        }



        #region Users
        public async Task<User> GetUser(string AccessToken)
        {
            var xml = await GetResponseData(ApiEndpoints.UserEndPoint, AccessToken);
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            XElement userElement = xml.Descendants(YahooXml.XMLNS + "user").FirstOrDefault();
            var user = (User)serializer.Deserialize(userElement.CreateReader());
            return user;
        }

        #endregion

        #region Games
        /// <summary>
        /// Get Games for Logged-In User
        /// </summary>
        /// <param name="gameKeys"></param>
        /// <param name="subresources"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IList<Game>> GetUserGames(string[] gameKeys, EndpointSubResourcesCollection subresources, string AccessToken)
        {
            //https://fantasysports.yahooapis.com/fantasy/v2/users;use_login=1/games;game_keys={gameKeys};out={subresources}


            var xml = await GetResponseData(ApiEndpoints.UserGamesEndPoint(gameKeys, subresources, true), AccessToken);
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            List<XElement> gameElements = xml.Descendants(YahooXml.XMLNS + "game").ToList();
            IList<Game> games = new List<Game>();
            foreach (var gameElement in gameElements)
            {
                games.Add((Game)serializer.Deserialize(gameElement.CreateReader()));
            }
            return games;

        }

        public async Task<IList<League>> GetLeagues(string[] leagueKeys, EndpointSubResourcesCollection subresources, string AccessToken)
        {
            var xml = await GetResponseData(ApiEndpoints.UserGameLeaguesWithKeyEndPoint(leagueKeys, subresources), AccessToken);
            XmlSerializer serializer = new XmlSerializer(typeof(League));
            List<XElement> leagueElements = xml.Descendants(YahooXml.XMLNS + "league").ToList();
            IList<League> leagues = new List<League>();
            foreach (var leagueElement in leagueElements)
            {
                leagues.Add((League)serializer.Deserialize(leagueElement.CreateReader()));
            }
            return leagues;

        }

        public async Task<IList<League>> GetLeagueTeams(string[] leagueKeys, EndpointSubResourcesCollection subresources, string AccessToken)
        {
            var xml = await GetResponseData(ApiEndpoints.LeagueTeamsEndPoint(leagueKeys, subresources), AccessToken);
            XmlSerializer serializer = new XmlSerializer(typeof(League));
            List<XElement> leagueElements = xml.Descendants(YahooXml.XMLNS + "league").ToList();
            IList<League> leagues = new List<League>();
            foreach (var leagueElement in leagueElements)
            {
                leagues.Add((League)serializer.Deserialize(leagueElement.CreateReader()));
            }
            return leagues;
        }
        #endregion

        //        #region Transactions

        //        public async Task AddPlayer(string leagueKey, string teamKey, string playerKey, NameValueCollection parameters)
        //        {
        //            // FAAB: Add This to transaction Node  <faab_bid>25</faab_bid>
        //            var xml = @"
        //<fantasy_content>
        //  <transaction>
        //    <type>add</type>
        //    <player>
        //      <player_key>{player_key}</player_key>
        //      <transaction_data>
        //        <type>add</type>
        //        <destination_team_key>{team_key}</destination_team_key>
        //      </transaction_data>
        //    </player>
        //  </transaction>
        //</fantasy_content>
        //";
        //            //var xml = await PostResponseData(ApiEndpoints.LeagueTransactions(leagueKey), parameters);
        //        }

        //        public async Task DropPlayer(string leagueKey, string teamKey, string playerKey, NameValueCollection parameters)
        //        {
        //            var xml = @"
        //<fantasy_content>
        //  <transaction>
        //    <type>drop</type>
        //    <player>
        //      <player_key>{player_key}</player_key>
        //      <transaction_data>
        //        <type>drop</type>
        //        <source_team_key>{team_key}</source_team_key>
        //      </transaction_data>
        //    </player>
        //  </transaction>
        //</fantasy_content>
        //";
        //            //var xml = await PostResponseData(ApiEndpoints.LeagueTransactions(leagueKey), parameters);
        //        }
        //        public async Task AddDropPlayer(string leagueKey, string teamKey, string playerKey, NameValueCollection parameters)
        //        {
        //            // FAAB: Add This to transaction Node  <faab_bid>25</faab_bid>
        //            var xml = @"
        //<fantasy_content>
        //  <transaction>
        //    <type>add/drop</type>
        //    <players>
        //      <player>
        //        <player_key>{player_key}</player_key>
        //        <transaction_data>
        //          <type>add</type>
        //          <destination_team_key>{team_key}</destination_team_key>
        //        </transaction_data>
        //      </player>
        //      <player>
        //        <player_key>{player_key}</player_key>
        //        <transaction_data>
        //          <type>drop</type>
        //          <source_team_key>{team_key}</source_team_key>
        //        </transaction_data>
        //      </player>
        //    </players>
        //  </transaction>
        //</fantasy_content>
        //";
        //            //var xml = await PostResponseData(ApiEndpoints.LeagueTransactions(leagueKey), parameters);
        //        }
        //        #endregion

        /// <summary>
        /// Gets Access Token and makes Request against Endpoint passed in
        /// </summary>
        /// <param name="endpoint">Uri of Api to Query</param>
        /// <param name="parameters">Items on QS/Form Data used to fulfill Api Call</param>
        /// <returns></returns>
        private async Task<XDocument> GetResponseData(Endpoint endpoint, string AccessToken)
        {
            var client = _factory.CreateClient(endpoint, new AuthenticationHeaderValue("Bearer", AccessToken));

            var request = _factory.CreateRequest(endpoint);

            var response = await client.GetAsync(request.RequestUri);
            var result = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("Combination of Resource and SubResources Not Allowed, Please try altering");
            }

            return XDocument.Parse(result);
        }

    }
}