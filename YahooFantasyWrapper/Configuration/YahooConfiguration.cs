using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace YahooFantasyWrapper.Configuration
{
    public class YahooConfiguration
    {
        public string ClientSecret { get; set; }

        public string ClientPublic { get; set; }

        public string RedirectUri { get; set; }
        public string ClientId { get; set; }
    }
}
