namespace FantasyTradeAnalyzer.Database.Model
{
    public class ActualStatisticDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public int? Week { get; set; }
        public double PassingCompletions { get; set; }
        public double PassingAttempts { get; set; }
        public double PassingYards { get; set; }
        public double PassingTouchdowns { get; set; }
        public double Interceptions { get; set; }
        public double RushingAttempts { get; set; }
        public double RushingYards { get; set; }
        public double RushingTouchdowns { get; set; }
        public double Receptions { get; set; }
        public double ReceivingYards { get; set; }
        public double ReceivingTouchdowns { get; set; }
        public double Fumbles { get; set; }
        public double ReturnYards { get; set; }
        public double ReturnTouchdowns { get; set; }
        public int GamesPlayed { get; set; }
        public System.DateTime InsertDateTime { get; set; }
        public System.Guid SessionId { get; set; }
    }
}