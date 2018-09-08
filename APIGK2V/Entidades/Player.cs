namespace APIGK2V.Entidades
{
    public class Player : EntidadeBase
    {
        public int RoundId { get; set; }
        public int MatchId { get; set; }
        public string TeamInitials { get; set; }
        public string CoachName { get; set; }
        public string LineUp { get; set; }
        public int ShirtNumber { get; set; }
        public string PlayerName { get; set; } 
        public string Position { get; set; }
        public string Event { get; set; }
    }
}