﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Infrastructure
{
    internal static class ResponseDeserializer
    {
        //internal static T Deserialize<T>(XElement rootElement)
        //{
        //    Type type = typeof(T);

        //    if (type == typeof(Player))
        //    {
        //        return (T)(object)ResponseDeserializer.DeserializePlayer(rootElement);
        //    }
        //    else if (type == typeof(PlayerWithStats))
        //    {
        //        return (T)(object)ResponseDeserializer.DeserializePlayerWithStats(rootElement);
        //    }

        //    throw new Exception("Unable to deserialize type.");
        //}

        //internal static RosterPosition DeserializeRosterPosition(XElement rosterPositionElement)
        //{
        //    var position = ResponseDeserializer.DeserializePosition(rosterPositionElement);
        //    int count = Convert.ToInt32(rosterPositionElement.Element(YahooXml.XMLNS + "count").Value);

        //    return new RosterPosition(position, count);
        //}

        //internal static Position DeserializePosition(XElement rosterPositionElement)
        //{
        //    string name = rosterPositionElement.Element(YahooXml.XMLNS + "position").Value;
        //    return Position.GetPosition(name);
        //}

        //private static Player DeserializePlayer(XElement playerElement)
        //{
        //    int id = Convert.ToInt32(playerElement.Element(YahooXml.XMLNS + "player_id").Value);
        //    string key = playerElement.Element(YahooXml.XMLNS + "player_key").Value;
        //    string name = playerElement.Element(YahooXml.XMLNS + "name").Element(YahooXml.XMLNS + "full").Value;

        //    var statusElement = playerElement.Element(YahooXml.XMLNS + "status");
        //    string status = statusElement != null ? statusElement.Value : null;

        //    var eligiblePositions = new List<Position>();
        //    foreach (var positionElement in playerElement.Elements(YahooXml.XMLNS + "eligible_positions"))
        //    {
        //        eligiblePositions.Add(ResponseDeserializer.DeserializePosition(positionElement));
        //    }

        //    var byeWeeks = new List<int>();
        //    foreach (var weekElement in playerElement.Elements(YahooXml.XMLNS + "bye_weeks"))
        //    {
        //        byeWeeks.Add(Convert.ToInt32(weekElement.Value));
        //    }

        //    return new Player(id, key, name, status, eligiblePositions, byeWeeks);
        //}

        //private static PlayerWithStats DeserializePlayerWithStats(XElement playerElement)
        //{
        //    var player = DeserializePlayer(playerElement);
        //    var points = DeserializePlayerPoints(playerElement.Element(YahooXml.XMLNS + "player_points"));

        //    return new PlayerWithStats(player, null, points);
        //}

        //internal static PlayerPoints DeserializePlayerPoints(XElement playerPointsElement)
        //{
        //    int season = Convert.ToInt32(playerPointsElement.Element(YahooXml.XMLNS + "season").Value);
        //    double total = Convert.ToDouble(playerPointsElement.Element(YahooXml.XMLNS + "total").Value);
        //    return new PlayerPoints(season, total);
        //}

        //internal static LeagueSettings DeserializeLeagueSettings(XElement leagueElement)
        //{
        //    var settingElement = leagueElement.Element(YahooXml.XMLNS + "settings");

        //    var rosterPositions = new Dictionary<PositionAbbreviation, RosterPosition>();
        //    foreach (var rosterPositionElement in settingElement.Descendants(YahooXml.XMLNS + "roster_position"))
        //    {
        //        var rosterPosition = ResponseDeserializer.DeserializeRosterPosition(rosterPositionElement);
        //        rosterPositions.Add(rosterPosition.Position.Abbreviation, rosterPosition);
        //    }

        //    var league = ResponseDeserializer.DeserializeLeague(leagueElement);

        //    return new LeagueSettings(rosterPositions, league);
        //}

        //internal static League DeserializeLeague(XElement leagueElement)
        //{
        //    int id = Convert.ToInt32(leagueElement.Element(YahooXml.XMLNS + "league_id").Value);
        //    string name = leagueElement.Element(YahooXml.XMLNS + "name").Value;
        //    string key = leagueElement.Element(YahooXml.XMLNS + "league_key").Value;
        //    int startWeek = Convert.ToInt32(leagueElement.Element(YahooXml.XMLNS + "start_week").Value);
        //    int endWeek = Convert.ToInt32(leagueElement.Element(YahooXml.XMLNS + "end_week").Value);

        //    return new League(id, name, key, startWeek, endWeek);
        //}

        //internal static IList<Game> DeserializeGames(IList<XElement> gamesElement)
        //{
        //    var games = new List<Game>();
        //    foreach (var gameElement in gamesElement)
        //    {
        //        games.Add(ResponseDeserializer.DeserializeGame(gameElement));
        //    }
        //    return games;
        //}

        //internal static Game DeserializeGame(XElement gameElement)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(Game));
        //    var game = (Game)serializer.Deserialize(gameElement.CreateReader());
        //    return game;
        //}


        //internal static User DeserializeUser(XElement userElement)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(User));
        //    var user = (User)serializer.Deserialize(userElement.CreateReader());
        //    return user;
        //}

        //internal static IList<Users> DeserializeUsers(IList<XElement> usersElement)
        //{
        //    var users = new List<User>();
        //    foreach (var gameElement in usersElement)
        //    {
        //        games.Add(ResponseDeserializer.DeserializeGame(gameElement));
        //    }
        //    return users;
        //}

        //internal static T Deserialize(XElement element)
        //{

        //}

        //internal static TeamRosterPlayerCollection DeserializeTeamRosterPlayerCollection(XElement teamElement)
        //{
        //    var team = DeserializeTeam(teamElement);
        //    var players = DeserializePlayerCollection<Player>(teamElement);

        //    var rosterElement = teamElement.Element(YahooXml.XMLNS + "roster");
        //    int week = Convert.ToInt32(rosterElement.Element(YahooXml.XMLNS + "week").Value);

        //    return new TeamRosterPlayerCollection(team, players, week);
        //}

        //internal static Team DeserializeTeam(XElement teamElement)
        //{
        //    int id = Convert.ToInt32(teamElement.Element(YahooXml.XMLNS + "team_id").Value);
        //    string key = teamElement.Element(YahooXml.XMLNS + "team_key").Value;
        //    string name = teamElement.Element(YahooXml.XMLNS + "name").Value;

        //    return new Team(id, key, name);
        //}

        //internal static PlayerCollection<TPlayerType> DeserializePlayerCollection<TPlayerType>(XElement rootElement) where TPlayerType : Player
        //{
        //    var players = new PlayerCollection<TPlayerType>();
        //    foreach (var playerElement in rootElement.Descendants(YahooXml.XMLNS + "player"))
        //    {
        //        players.Add(ResponseDeserializer.Deserialize<TPlayerType>(playerElement));
        //    }

        //    return players;
        //}

        //internal static TeamPlayerCollection<TPlayerType> DeserializeTeamPlayerCollection<TPlayerType>(XElement teamElement) where TPlayerType : Player
        //{
        //    var team = ResponseDeserializer.DeserializeTeam(teamElement);
        //    var players = DeserializePlayerCollection<TPlayerType>(teamElement);

        //    return new TeamPlayerCollection<TPlayerType>(team, players);
        //}

        //internal static LeagueTeamPlayerCollection<TPlayerType> DeserializeLeagueTeamPlayerCollection<TPlayerType>(XElement leagueElement) where TPlayerType : Player
        //{
        //    var leagueTeamPlayers = new LeagueTeamPlayerCollection<TPlayerType>();

        //    foreach (var teamElement in leagueElement.Descendants(YahooXml.XMLNS + "team"))
        //    {
        //        leagueTeamPlayers.Add(ResponseDeserializer.DeserializeTeamPlayerCollection<TPlayerType>(teamElement));
        //    }

        //    return leagueTeamPlayers;
        //}

        //internal static TeamCollection DeserializeTeamCollection(XElement leagueElement)
        //{
        //    var teams = new TeamCollection();
        //    foreach (var teamElement in leagueElement.Descendants(YahooXml.XMLNS + "team"))
        //    {
        //        teams.Add(ResponseDeserializer.DeserializeTeam(teamElement));
        //    }

        //    return teams;
        //}

        //internal static LeagueTeamCollection DeserializeLeagueTeamCollection(XElement leagueElement)
        //{
        //    var league = ResponseDeserializer.DeserializeLeague(leagueElement);
        //    var teams = ResponseDeserializer.DeserializeTeamCollection(leagueElement);

        //    return new LeagueTeamCollection(league, teams);
        //}

        //internal static LeagueDraftResultCollection DeserializeLeagueDraftResultCollection(XElement leagueElement)
        //{
        //    var league = ResponseDeserializer.DeserializeLeague(leagueElement);
        //    var draftResults = ResponseDeserializer.DeserializeDraftResultCollection(leagueElement);

        //    return new LeagueDraftResultCollection(league, draftResults);
        //}

        //internal static DraftResultCollection DeserializeDraftResultCollection(XElement leagueElement)
        //{
        //    var draftResults = new DraftResultCollection();

        //    foreach (var draftResultElement in leagueElement.Descendants(YahooXml.XMLNS + "draft_result"))
        //    {
        //        draftResults.Add(ResponseDeserializer.DeserializeDraftResult(draftResultElement));
        //    }

        //    return draftResults;
        //}

        //internal static DraftResult DeserializeDraftResult(XElement draftResultElement)
        //{
        //    var costElement = draftResultElement.Element(YahooXml.XMLNS + "cost");
        //    int? cost = null;
        //    if (costElement != null)
        //    {
        //        cost = Convert.ToInt32(costElement.Value);
        //    }

        //    return new DraftResult(
        //        Convert.ToInt32(draftResultElement.Element(YahooXml.XMLNS + "pick").Value),
        //        Convert.ToInt32(draftResultElement.Element(YahooXml.XMLNS + "round").Value),
        //        draftResultElement.Element(YahooXml.XMLNS + "team_key").Value,
        //        draftResultElement.Element(YahooXml.XMLNS + "player_key").Value,
        //        cost
        //    );
        //}

        //internal static ICollection<Matchup> DeserializeMatchups(XElement teamElement)
        //{
        //    var matchups = new List<Matchup>();
        //    var matchupsElement = teamElement.Element(YahooXml.XMLNS + "matchups");

        //    foreach (var matchupElement in matchupsElement.Elements(YahooXml.XMLNS + "matchup"))
        //    {
        //        string teamKeySelf = string.Empty;
        //        string teamKeyOpponent = string.Empty;
        //        int week = Convert.ToInt32(matchupElement.Element(YahooXml.XMLNS + "week").Value);

        //        foreach (var matchupTeamElement in matchupElement.Element(YahooXml.XMLNS + "teams").Elements(YahooXml.XMLNS + "team"))
        //        {
        //            var isOwnedByCurrentLogin = matchupTeamElement.Element(YahooXml.XMLNS + "is_owned_by_current_login");
        //            if (isOwnedByCurrentLogin != null)
        //            {
        //                teamKeySelf = matchupTeamElement.Element(YahooXml.XMLNS + "team_key").Value;
        //            }
        //            else
        //            {
        //                teamKeyOpponent = matchupTeamElement.Element(YahooXml.XMLNS + "team_key").Value;
        //            }
        //        }

        //        matchups.Add(new Matchup(week, teamKeySelf, teamKeyOpponent));
        //    }

        //    return matchups;
        //}
    }
}
