using AutoMapper;
using FantasyTradeAnalyzer.Database.Entity;
using FantasyTradeAnalyzer.Database.Model;
using FantasyTradeAnalyzer.Model.ProjectionsSources;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyTradeAnalyzer.Service
{
    public static class MapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProjectionEntity, ProjectionDto>()
                .ForMember(dest => dest.ProjectedPoints, o => o.Ignore());
            });
        }
    }
}
