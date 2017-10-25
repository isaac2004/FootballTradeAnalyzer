using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using YahooFantasyWrapper.Models;

namespace FantasyTradeAnalyzer.Model.Dto
{
    [DataContract]
    public class StatCategoryDto
    {
        [DataMember]
        public string PositionType { get; set; }
        [DataMember]
        public int StatId { get; set; }
        [DataMember]
        public string Name { get; set; }

        public StatCategoryDto() { }

        public StatCategoryDto(Stat stat)
        {
            this.Name = stat.Name;
            this.StatId = Convert.ToInt32(stat.StatId);
            this.PositionType = stat.PositionType;
        }
    }
}
