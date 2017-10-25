using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace FantasyTradeAnalyzer.Model.Dto
{
    [DataContract]
    public class PositionDto : IEqualityComparer<PositionDto>
    {
        public PositionDto() { }
        private PositionDto(PositionAbbreviationDto abbreviation, PositionTypeDto type, string displayName, bool isFlex, ICollection<PositionChildDto> possiblePositions)
            : this(abbreviation, type, displayName, isFlex)
        {
            this.PossiblePositions = possiblePositions;
        }

        private PositionDto(PositionAbbreviationDto abbreviation, PositionTypeDto type, string displayName, bool isFlex)
        {
            this.Abbreviation = abbreviation;
            this.Type = type;
            this.DisplayName = displayName;
            this.IsFlex = isFlex;
            this.PossiblePositions = new List<PositionChildDto>() { new PositionChildDto (this) };
        }
        [DataMember]
        public PositionAbbreviationDto Abbreviation { get; private set; }
        [DataMember]
        public PositionTypeDto Type { get; private set; }
        [DataMember]
        public string DisplayName { get; private set; }
        [DataMember]
        public bool IsFlex { get; private set; }
        [DataMember]
        public ICollection<PositionChildDto> PossiblePositions { get; private set; }

        public bool Equals(PositionDto x, PositionDto y)
        {
            return x.Abbreviation == y.Abbreviation;
        }

        public int GetHashCode(PositionDto obj)
        {
            return obj.Abbreviation.GetHashCode();
        }

        public static PositionDto GetPosition(string name)
        {
            return _positionMap[name];
        }

        public static PositionDto Quarterback = new PositionDto(PositionAbbreviationDto.QB, PositionTypeDto.Offense, "Quarterback", false);
        public static PositionDto WideReceiver = new PositionDto(PositionAbbreviationDto.WR, PositionTypeDto.Offense, "Wide Receiver", false);
        public static PositionDto RunningBack = new PositionDto(PositionAbbreviationDto.RB, PositionTypeDto.Offense, "Running Back", false);
        public static PositionDto TightEnd = new PositionDto(PositionAbbreviationDto.TE, PositionTypeDto.Offense, "Tight End", false);
        public static PositionDto Kicker = new PositionDto(PositionAbbreviationDto.K, PositionTypeDto.Kickers, "Kicker", false);
        public static PositionDto Defense = new PositionDto(PositionAbbreviationDto.DEF, PositionTypeDto.Defense_SpecialTeams, "Defense/Special Teams", false);
        public static PositionDto DefensiveBack = new PositionDto(PositionAbbreviationDto.DB, PositionTypeDto.DefensivePlayers, "Defensive Back", false);
        public static PositionDto DefensiveLineman = new PositionDto(PositionAbbreviationDto.DL, PositionTypeDto.DefensivePlayers, "Defensive Lineman", false);
        public static PositionDto Linebacker = new PositionDto(PositionAbbreviationDto.LB, PositionTypeDto.DefensivePlayers, "Linebacker", false);
        public static PositionDto DefensiveTackle = new PositionDto(PositionAbbreviationDto.DT, PositionTypeDto.DefensivePlayers, "Defensive Tackle", false);
        public static PositionDto DefensiveEnd = new PositionDto(PositionAbbreviationDto.DE, PositionTypeDto.DefensivePlayers, "Defensive End", false);
        public static PositionDto CornerBack = new PositionDto(PositionAbbreviationDto.CB, PositionTypeDto.DefensivePlayers, "Cornerback", false);
        public static PositionDto Safety = new PositionDto(PositionAbbreviationDto.S, PositionTypeDto.DefensivePlayers, "Safety", false);
        public static PositionDto Bench = new PositionDto(PositionAbbreviationDto.BN, PositionTypeDto.Defense_SpecialTeams, "Bench", false);
        public static PositionDto InjuredReserve = new PositionDto(PositionAbbreviationDto.IR, PositionTypeDto.Defense_SpecialTeams, "Injured Reserve", false);

        public static PositionDto WideReceiverTightEnd = new PositionDto(PositionAbbreviationDto.W_T, PositionTypeDto.Offense, "Wide Receiver/Tight End", true, new Collection<PositionChildDto>() { new PositionChildDto( WideReceiver), new PositionChildDto(TightEnd) });
        public static PositionDto WideReceiverRunningBack = new PositionDto(PositionAbbreviationDto.W_R, PositionTypeDto.Offense, "Wide Receiver/Running Back", true, new Collection<PositionChildDto>() { new PositionChildDto(WideReceiver), new PositionChildDto(RunningBack) });
        public static PositionDto WideReceiverRunningBackTightEnd = new PositionDto(PositionAbbreviationDto.W_R_T, PositionTypeDto.Offense, "Wide Receiver/Running Back/Tight End", true, new Collection<PositionChildDto>() { new PositionChildDto(WideReceiver), new PositionChildDto(RunningBack), new PositionChildDto(TightEnd) });
        public static PositionDto QuarterBackWideReceiverRunningBackTightEnd = new PositionDto(PositionAbbreviationDto.Q_W_R_T, PositionTypeDto.Offense, "Quarterback/Wide Receiver/Running Back/Tight End", true, new Collection<PositionChildDto>() { new PositionChildDto(Quarterback), new PositionChildDto(WideReceiver), new PositionChildDto(RunningBack), new PositionChildDto(TightEnd)});
        public static PositionDto DefensivePlayer = new PositionDto(PositionAbbreviationDto.D, PositionTypeDto.DefensivePlayers, "Defensive Player", true, new Collection<PositionChildDto>() { new PositionChildDto(DefensiveBack), new PositionChildDto(DefensiveLineman), new PositionChildDto(Linebacker), new PositionChildDto(DefensiveTackle), new PositionChildDto(DefensiveEnd), new PositionChildDto(CornerBack), new PositionChildDto(Safety) });

        private static Dictionary<string, PositionDto> _positionMap = new Dictionary<string, PositionDto>()
        {
            {"QB", Quarterback},
            {"WR", WideReceiver},
            {"RB", RunningBack},
            {"TE", TightEnd},
            {"W/T", WideReceiverTightEnd},
            {"W/R", WideReceiverRunningBack},
            {"W/R/T", WideReceiverRunningBackTightEnd},
            {"Q/W/R/T", QuarterBackWideReceiverRunningBackTightEnd},
            {"K", Kicker},
            {"DEF", Defense},
            {"DB", DefensiveBack},
            {"DL", DefensiveLineman},
            {"LB", Linebacker},
            {"DT", DefensiveTackle},
            {"DE", DefensiveEnd},
            {"CB", CornerBack},
            {"S", Safety},
            {"D", DefensivePlayer},
            {"BN", Bench},
            {"IR", InjuredReserve},
        };
    }
    [DataContract]
    public class PositionChildDto
    {
        [DataMember]
        public PositionAbbreviationDto Abbreviation { get;  set; }
        [DataMember]
        public PositionTypeDto Type { get;  set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public bool IsFlex { get;  set; }

        public PositionChildDto() { }

        public PositionChildDto(PositionDto dto)
        {
            this.Abbreviation = dto.Abbreviation;
            this.DisplayName = dto.DisplayName;
            this.IsFlex = dto.IsFlex;
            this.Type = dto.Type;
        }
    }

    public enum PositionAbbreviationDto
    {
        QB = 1,
        WR = 2,
        RB = 3,
        TE = 4,
        W_T = 5,
        W_R = 6,
        W_R_T = 7,
        Q_W_R_T = 8,
        K = 9,
        DEF = 10,
        D = 11,
        DB = 12,
        DL = 13,
        LB = 14,
        DT = 15,
        DE = 16,
        CB = 17,
        S = 18,
        BN = 19,
        IR = 20
    }
    public enum PositionTypeDto
    {
        Offense = 1,
        Kickers = 2,
        Defense_SpecialTeams = 3,
        DefensivePlayers = 4
    }
}
