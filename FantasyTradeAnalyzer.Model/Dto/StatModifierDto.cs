using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using YahooFantasyWrapper.Models;

namespace FantasyTradeAnalyzer.Model.Dto
{
    [DataContract]
    public class StatModifierDto
    {
        [DataMember]
        public int StatId { get; set; }
        [DataMember]
        public double Value { get; set; }

        public StatModifierDto() { }

        public StatModifierDto(Stat stat)
        {
            this.StatId = Convert.ToInt32(stat.StatId);
            this.Value = Convert.ToDouble(stat.Value);
        }
    }
}
