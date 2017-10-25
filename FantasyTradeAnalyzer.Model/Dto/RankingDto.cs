namespace FantasyTradeAnalyzer.Database.Model
{
    public class RankingDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public int Rank { get; set; }
        public int PositionRank { get; set; }
        public int Bye { get; set; }
        public int Best { get; set; }
        public int Worst { get; set; }
        public double Avg { get; set; }
        public double StdDev { get; set; }
        public int? Week { get; set; }
        public System.DateTime InsertDateTime { get; set; }
        public string Site { get; set; }
        public string PPR { get; set; }
        public System.Guid SessionId { get; set; }
    }
}