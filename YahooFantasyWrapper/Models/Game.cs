﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{
    [XmlRoot(ElementName = "game_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class GameWeek
    {
        [XmlElement(ElementName = "week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Week { get; set; }
        [XmlElement(ElementName = "start", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Start { get; set; }
        [XmlElement(ElementName = "end", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string End { get; set; }
    }

    [XmlRoot(ElementName = "game_weeks", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class GameWeeks
    {
        [XmlElement(ElementName = "game_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public List<GameWeek> GameWeek { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }


    [XmlRoot(ElementName = "manager", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Manager
    {
        [XmlElement(ElementName = "manager_id", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Manager_id { get; set; }
        [XmlElement(ElementName = "nickname", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Nickname { get; set; }
        [XmlElement(ElementName = "guid", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Guid { get; set; }
        [XmlElement(ElementName = "is_commissioner", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsCommissioner { get; set; }
        [XmlElement(ElementName = "is_current_login", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsCurrentLogin { get; set; }
        [XmlElement(ElementName = "email", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Email { get; set; }
        [XmlElement(ElementName = "image_url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string ImageUrl { get; set; }
    }

    [XmlRoot(ElementName = "managers", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Managers
    {
        [XmlElement(ElementName = "manager", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Manager Manager { get; set; }
    }

    [XmlRoot(ElementName = "game", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Game
    {
        [XmlElement(ElementName = "game_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string GameKey { get; set; }
        [XmlElement(ElementName = "game_id", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string GameId { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Name { get; set; }
        [XmlElement(ElementName = "code", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Code { get; set; }
        [XmlElement(ElementName = "type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Type { get; set; }
        [XmlElement(ElementName = "url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Url { get; set; }
        [XmlElement(ElementName = "season", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Season { get; set; }
        [XmlElement(ElementName = "is_registration_over", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsRegistrationOver { get; set; }
        [XmlElement(ElementName = "is_game_over", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsGameOver { get; set; }
        [XmlElement(ElementName = "is_offseason", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsOffseason { get; set; }
        [XmlElement(ElementName = "game_weeks", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public GameWeeks GameWeeks { get; set; }
        [XmlElement(ElementName = "leagues", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Leagues Leagues { get; set; }
        [XmlElement(ElementName = "stat_categories", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public StatCategories StatCategories { get; set; }
        [XmlElement(ElementName = "position_types", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public PositionTypes PositionTypes { get; set; }
        [XmlElement(ElementName = "roster_positions", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public RosterPositions RosterPositions { get; set; }
        [XmlElement(ElementName = "teams", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Teams Teams { get; set; }
    }

    [XmlRoot(ElementName = "games", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Games
    {
        [XmlElement(ElementName = "game", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Game Game { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }

    [XmlRoot(ElementName = "user", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class User
    {
        [XmlElement(ElementName = "guid", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Guid { get; set; }
        [XmlElement(ElementName = "games", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Games Games { get; set; }
    }

    [XmlRoot(ElementName = "users", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Users
    {
        [XmlElement(ElementName = "user", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public User User { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }

    [XmlRoot(ElementName = "fantasy_content", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class FantasyContent
    {
        [XmlElement(ElementName = "users", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Users Users { get; set; }
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
        [XmlAttribute(AttributeName = "uri", Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public string Uri { get; set; }
        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }
        [XmlAttribute(AttributeName = "copyright")]
        public string Copyright { get; set; }
        [XmlAttribute(AttributeName = "refresh_rate")]
        public string RefreshRate { get; set; }
        [XmlAttribute(AttributeName = "yahoo", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Yahoo { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    public enum GameCode
    {
        nfl = 0,
        pnfl = 1,
        mlb = 2,
        pmlb = 3,
        nba = 4,
        nhl = 5,
        yahoops = 6,
        nflp = 7
    }
}
