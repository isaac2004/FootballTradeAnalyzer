using System;
using Microsoft.Azure.WebJobs.Description;

namespace FantasyTradeAnalyzer.ProjectionGenerator.Injection
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class InjectAttribute : Attribute
    {
    }
}