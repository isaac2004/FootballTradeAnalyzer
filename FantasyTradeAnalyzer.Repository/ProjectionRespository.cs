using AutoMapper;
using FantasyTradeAnalyzer.Database;
using FantasyTradeAnalyzer.Database.Entity;
using FantasyTradeAnalyzer.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.BulkExtensions;
using System.Net;
using AngleSharp;
using HtmlAgilityPack;

namespace FantasyTradeAnalyzer.Respository
{
    public class ProjectionRespository : IProjectionRespository
    {
        private readonly FootballContext _footballContext;

        public ProjectionRespository(FootballContext footballContext)
        {
            _footballContext = footballContext;
        }
        public async Task<IEnumerable<ProjectionEntity>> GetProjections()
        {
            return await _footballContext.Projections.ToListAsync();

        }

        public async Task PopulateProjections()
        {
            Guid sessionId = Guid.NewGuid();

            string sql = "DELETE FROM projections WHERE site = @name and sessionId <> @sessionId ";
            int result = _footballContext.Database.ExecuteSqlCommand(sql, new SqlParameter("name", "FantasySharks"), new SqlParameter("sessionId", sessionId));
            await _footballContext.SaveChangesAsync();

            List<ProjectionEntity> projectionList = new List<ProjectionEntity>();

            var config = Configuration.Default.WithDefaultLoader();

            HtmlDocument doc = new HtmlDocument();

            string[] positions = { "1", "2", "4", "5" };

            //Parallel.For((563 + 1), 580 + 1, i =>
            for (int i = 596; i <= 612; i++)
            {
                int week = i - 595;
                foreach (var pos in positions)
                {
                    var address = $"https://www.fantasysharks.com/apps/bert/forecasts/projections.php?League=&Position={pos}&Segment={i}&uid=4";
                    var document = BrowsingContext.New(config).OpenAsync(address).Result;
                    doc.LoadHtml(document.Source.Text);
                    lock (projectionList)
                    {
                        GetProjection(doc, ref projectionList, week, sessionId, pos);

                    }
                }
            }//);


            _footballContext.BulkInsert(projectionList);
            await _footballContext.SaveChangesAsync();
        }

        private static void GetProjection(HtmlDocument doc, ref List<ProjectionEntity> projectionsList, int week, Guid sessionId, string pos)
        {
            var myTable = doc.DocumentNode.SelectSingleNode("//table[@id='toolData']");
            var trs = myTable.SelectNodes("//tr").Skip(5);
            foreach (var tr in trs)
            {
                FantasySharks projection = new FantasySharks(tr, week, sessionId, pos);
                projectionsList.Add(projection);
            }
            projectionsList.RemoveAll(vehicle => string.IsNullOrEmpty(vehicle.Name));
        }
    }

    public class FantasySharks : ProjectionEntity
    {
        public FantasySharks(HtmlNode tr, int week, Guid sessionId, string pos)
        {
            int parse = 0;
            if (int.TryParse(tr.ChildNodes[0].InnerText, out parse))
            {
                this.Name = FormatName(string.Join(" ", tr.ChildNodes[1].InnerText.Replace("&nbsp;", "").Split(',').Select(a => a.Trim()).Reverse()));
                this.Team = tr.ChildNodes[2].InnerText;

                this.Week = week;
                this.InsertDateTime = DateTime.Now;
                this.Site = this.GetType().Name;
                this.SessionId = sessionId;

                switch (pos)
                {
                    case "1":
                        this.Position = "QB";
                        this.PassingAttempts = Convert.ToDouble(tr.ChildNodes[4].InnerText);
                        this.PassingCompletions = Convert.ToDouble(tr.ChildNodes[5].InnerText);
                        this.PassingYards = Convert.ToDouble(tr.ChildNodes[6].InnerText);
                        this.PassingTouchdowns = Convert.ToDouble(tr.ChildNodes[7].InnerText);
                        this.Interceptions = Convert.ToDouble(tr.ChildNodes[14].InnerText);

                        this.RushingAttempts = Convert.ToDouble(tr.ChildNodes[16].InnerText);
                        this.RushingYards = Convert.ToDouble(tr.ChildNodes[17].InnerText);
                        this.RushingTouchdowns = Convert.ToDouble(tr.ChildNodes[18].InnerText);
                        this.Fumbles = Convert.ToDouble(tr.ChildNodes[19].InnerText);
                        break;
                    case "2":
                        this.Position = "RB";

                        this.RushingAttempts = Convert.ToDouble(tr.ChildNodes[4].InnerText);
                        this.RushingYards = Convert.ToDouble(tr.ChildNodes[5].InnerText);
                        this.RushingTouchdowns = Convert.ToDouble(tr.ChildNodes[6].InnerText);

                        this.Receptions = Convert.ToDouble(tr.ChildNodes[13].InnerText);
                        this.ReceivingYards = Convert.ToDouble(tr.ChildNodes[14].InnerText);
                        this.ReceivingTouchdowns = Convert.ToDouble(tr.ChildNodes[15].InnerText);
                        this.Fumbles = Convert.ToDouble(tr.ChildNodes[20].InnerText);
                        break;
                    case "4":
                        this.Position = "WR";
                        this.Receptions = Convert.ToDouble(tr.ChildNodes[6].InnerText);
                        this.ReceivingYards = Convert.ToDouble(tr.ChildNodes[7].InnerText);
                        this.ReceivingTouchdowns = Convert.ToDouble(tr.ChildNodes[8].InnerText);

                        this.RushingYards = Convert.ToDouble(tr.ChildNodes[15].InnerText);
                        this.RushingTouchdowns = Convert.ToDouble(tr.ChildNodes[16].InnerText);
                        this.Fumbles = Convert.ToDouble(tr.ChildNodes[19].InnerText);
                        break;
                    case "5":
                        this.Position = "TE";
                        this.Receptions = Convert.ToDouble(tr.ChildNodes[6].InnerText);
                        this.ReceivingYards = Convert.ToDouble(tr.ChildNodes[7].InnerText);
                        this.ReceivingTouchdowns = Convert.ToDouble(tr.ChildNodes[8].InnerText);

                        this.RushingYards = Convert.ToDouble(tr.ChildNodes[15].InnerText);
                        this.RushingTouchdowns = Convert.ToDouble(tr.ChildNodes[16].InnerText);
                        this.Fumbles = Convert.ToDouble(tr.ChildNodes[19].InnerText);
                        break;
                }
            }
        }

        public static string FormatName(string name)
        {
            string fullName = name.Replace("Stevie Johnson", "Steve Johnson").Replace("Christopher Ivory", "Chris Ivory").Replace("&nbsp;", "");
            fullName = fullName.Replace("Sr.", "").Replace("Sr.", "").Replace("III", "").Trim();
            return fullName;
        }
    }
}