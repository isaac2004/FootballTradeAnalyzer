using System.IO;
using FantasyTradeAnalyzer.ProjectionGenerator.Injection;
using FantasyTradeAnalyzer.Respository;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FantasyTradeAnalyzer.ProjectionGenerator
{
    public static class ProcessProjections
    {
        [FunctionName("ProcessProjections")]
        public static void Run([TimerTrigger("0 0 10 * * *")]TimerInfo myTimer, ILogger log, [Inject]IProjectionRespository projectionRespository)
        {
            projectionRespository.PopulateProjections();            
        }
    }
}
