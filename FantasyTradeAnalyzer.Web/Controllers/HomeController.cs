using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FantasyTradeAnalyzer.Web.Models;
using FantasyTradeAnalyzer.Service.Yahoo;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using YahooFantasyWrapper.Models;
using System.Web;
using FantasyTradeAnalyzer.Model.Dto;

namespace FantasyTradeAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IYahooService _yahooService;

        private NameValueCollection Parameters
        {
            get
            {
                return HttpUtility.ParseQueryString(Request.QueryString.Value);
            }
        }

        public HomeController(IYahooService yahooService)
        {
            this._yahooService = yahooService;
        }

        public async Task<IActionResult> Index()
        {
            ViewModel model = new ViewModel();
            if (this.Parameters != null & this.Parameters.Count > 0)
            {
                UserInfo userInfo = await this._yahooService.GetUserProfile(this.Parameters);
                UserInfo user = userInfo;
                userInfo = (UserInfo)null;
                IEnumerable<LeagueDto> leagueDtos = await this._yahooService.GetLeagues();
                IEnumerable<LeagueDto> leagues = leagueDtos;
                leagueDtos = (IEnumerable<LeagueDto>)null;
                List<string> pos = new List<string>()
        {
          "DEF",
          "K"
        };
                model.Leagues = leagues;
                model.Token = this._yahooService.GetAccessToken();
                user = (UserInfo)null;
                leagues = (IEnumerable<LeagueDto>)null;
                pos = (List<string>)null;
            }
            return (IActionResult)this.View((object)model);
        }

        public RedirectResult Login()
        {
            return new RedirectResult(this._yahooService.GetAuthUrl());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}