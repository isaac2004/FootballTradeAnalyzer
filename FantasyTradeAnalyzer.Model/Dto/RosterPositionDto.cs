
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FantasyTradeAnalyzer.Model.Dto
{
[DataContract]
    public class RosterPositionDto : IEqualityComparer<RosterPositionDto>
    {
        public RosterPositionDto() { }
        internal RosterPositionDto(PositionDto position, int count)
        {
            this.Position = position;
            this.Count = count;
        }
        [DataMember]

        public PositionDto Position { get; private set; }
        [DataMember]

        public int Count { get; set; }

        public bool Equals(RosterPositionDto x, RosterPositionDto y)
        {
            return x.Position.Equals(y.Position);
        }

        public int GetHashCode(RosterPositionDto obj)
        {
            return obj.Position.GetHashCode();
        }
    }
}