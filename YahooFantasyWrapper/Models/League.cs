﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Models
{
    [XmlRoot(ElementName = "league", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class League
    {
        [XmlElement(ElementName = "league_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string LeagueKey { get; set; }
        [XmlElement(ElementName = "league_id", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string LeagueId { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Name { get; set; }
        [XmlElement(ElementName = "url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Url { get; set; }
        [XmlElement(ElementName = "password", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Password { get; set; }
        [XmlElement(ElementName = "draft_status", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string DraftStatus { get; set; }
        [XmlElement(ElementName = "num_teams", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string NumTeams { get; set; }
        [XmlElement(ElementName = "edit_key", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EditKey { get; set; }
        [XmlElement(ElementName = "weekly_deadline", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string WeeklyDeadline { get; set; }
        [XmlElement(ElementName = "league_update_timestamp", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string LeagueUpdateTimestamp { get; set; }
        [XmlElement(ElementName = "scoring_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string ScoringType { get; set; }
        [XmlElement(ElementName = "league_type", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string LeagueType { get; set; }
        [XmlElement(ElementName = "renew", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Renew { get; set; }
        [XmlElement(ElementName = "renewed", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Renewed { get; set; }
        [XmlElement(ElementName = "short_invitation_url", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string ShortInvationUrl { get; set; }
        [XmlElement(ElementName = "allow_add_to_dl_extra_pos", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string AllowAddToDlExtraPos { get; set; }
        [XmlElement(ElementName = "is_pro_league", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsProLeague { get; set; }
        [XmlElement(ElementName = "is_cash_league", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string IsCashLeague { get; set; }
        [XmlElement(ElementName = "current_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string CurrentWeek { get; set; }
        [XmlElement(ElementName = "start_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string StartWeek { get; set; }
        [XmlElement(ElementName = "start_date", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string StartDate { get; set; }
        [XmlElement(ElementName = "end_week", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EndWeek { get; set; }
        [XmlElement(ElementName = "end_date", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string EndDate { get; set; }
        [XmlElement(ElementName = "game_code", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string GameCode { get; set; }
        [XmlElement(ElementName = "season", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public string Season { get; set; }

        [XmlElement(ElementName = "players", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Players Players { get; set; }

        [XmlElement(ElementName = "teams", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Teams Teams { get; set; }
        [XmlElement(ElementName = "settings", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public Settings Settings { get; set; }
    }

    [XmlRoot(ElementName = "leagues", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
    public class Leagues
    {
        [XmlElement(ElementName = "league", Namespace = "http://fantasysports.yahooapis.com/fantasy/v2/base.rng")]
        public League League { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }
}
