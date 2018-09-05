using System;

namespace APIGK2V.Entidades
{
    public class Cup : EntidadeBase
    {
        public int Year { get; set; }
        public string Country { get; set; }
        public string Winner { get; set; }  
        public string RunnersUp { get; set; }   
        public string Third { get; set; }
        public string Fourth { get; set; }
        public int QualifiedTeams { get; set; }
        public int MatchesPlayed { get; set; }
        public string Attendance { get; set; }
        public int GoalsScored { get; set; }
    }
}