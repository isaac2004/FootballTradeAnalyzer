using FantasyTradeAnalyzer.Model;
using FantasyTradeAnalyzer.Model.Dto;
using FantasyTradeAnalyzer.Service.Yahoo;
using FantasyTradeAnalyzer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyTradeAnalyzer.Web.Controllers
{
    [Route("api/[controller]")]
    public class TradeController : Controller
    {
        private readonly IYahooService _yahooService;

        public TradeController(IYahooService yahooService)
        {
            this._yahooService = yahooService;
        }

        [HttpPost]
        public async Task<TradeBreakdownViewModel> Post([FromBody] ProposedTradeModel model)
        {
            if (model == null)
                return (TradeBreakdownViewModel)null;
            List<PlayerDto> tradedPlayers = ((IEnumerable<PlayerDto>)model.myPlayers).Concat<PlayerDto>((IEnumerable<PlayerDto>)model.thierPlayers).ToList<PlayerDto>();
            TradeBreakdownViewModel tradeModel = new TradeBreakdownViewModel();
            TradeBreakdownViewModel breakdownViewModel1 = tradeModel;
            List<WeeklyLineupModel> weeklyLineupModelList1 = await this._yahooService.BuildRoster(model.selectedLeague, model.MyTeam, tradedPlayers);
            breakdownViewModel1.MyBeforeTradeLineup = weeklyLineupModelList1;
            breakdownViewModel1 = (TradeBreakdownViewModel)null;
            weeklyLineupModelList1 = (List<WeeklyLineupModel>)null;
            TradeBreakdownViewModel breakdownViewModel2 = tradeModel;
            List<WeeklyLineupModel> weeklyLineupModelList2 = await this._yahooService.BuildRoster(model.selectedLeague, model.ThierTeam, tradedPlayers);
            breakdownViewModel2.ThierBeforeTradeLineup = weeklyLineupModelList2;
            breakdownViewModel2 = (TradeBreakdownViewModel)null;
            weeklyLineupModelList2 = (List<WeeklyLineupModel>)null;
            model.MyTeam.Roster.AddRange((IEnumerable<PlayerDto>)model.thierPlayers);
            for (int i = 0; i <= model.myPlayers.Length - 1; ++i)
            {
                PlayerDto playerToRemove = model.MyTeam.Roster.Where<PlayerDto>((Func<PlayerDto, bool>)(a => a.Name == model.myPlayers[i].Name)).FirstOrDefault<PlayerDto>();
                model.MyTeam.Roster.Remove(playerToRemove);
                playerToRemove = (PlayerDto)null;
            }
            model.ThierTeam.Roster.AddRange((IEnumerable<PlayerDto>)model.myPlayers);
            for (int i = 0; i <= model.thierPlayers.Length - 1; ++i)
            {
                PlayerDto playerToRemove = model.ThierTeam.Roster.Where<PlayerDto>((Func<PlayerDto, bool>)(a => a.Name == model.thierPlayers[i].Name)).FirstOrDefault<PlayerDto>();
                model.ThierTeam.Roster.Remove(playerToRemove);
                playerToRemove = (PlayerDto)null;
            }
            TradeBreakdownViewModel breakdownViewModel3 = tradeModel;
            List<WeeklyLineupModel> weeklyLineupModelList3 = await this._yahooService.BuildRoster(model.selectedLeague, model.MyTeam, tradedPlayers);
            breakdownViewModel3.MyAfterTradeLineup = weeklyLineupModelList3;
            breakdownViewModel3 = (TradeBreakdownViewModel)null;
            weeklyLineupModelList3 = (List<WeeklyLineupModel>)null;
            TradeBreakdownViewModel breakdownViewModel4 = tradeModel;
            List<WeeklyLineupModel> weeklyLineupModelList4 = await this._yahooService.BuildRoster(model.selectedLeague, model.ThierTeam, tradedPlayers);
            breakdownViewModel4.ThierAfterTradeLineup = weeklyLineupModelList4;
            breakdownViewModel4 = (TradeBreakdownViewModel)null;
            weeklyLineupModelList4 = (List<WeeklyLineupModel>)null;
            return tradeModel;
        }
    }
}
