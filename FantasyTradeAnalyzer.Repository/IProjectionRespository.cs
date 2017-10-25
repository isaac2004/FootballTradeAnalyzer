using FantasyTradeAnalyzer.Database.Entity;
using FantasyTradeAnalyzer.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FantasyTradeAnalyzer.Respository
{
    public interface IProjectionRespository
    {
        Task<IEnumerable<ProjectionEntity>> GetProjections();

        Task PopulateProjections();
    }
}
