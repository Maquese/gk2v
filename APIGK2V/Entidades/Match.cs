using System;

namespace APIGK2V.Entidades
{
    public class Match : EntidadeBase
    {
     public int Year { get; set; }
     public String Datetime { get; set; }

     public string Stage  { get; set; }
    public string Stadium { get; set; }
    public string City { get; set; }
    public string HomeTeamName { get; set; }
    public int HomeTeamGoals{ get; set; }
    public int AwayTeamGoals{ get; set; }
    public string AwayTeamName{ get; set; }
    public string WinConditions{ get; set; }
    public string Attendance{ get; set ; }
    public int HalftimeHomeGoals{ get; set; }
    public int HalftimeAwayGoals{ get; set; }
    public string Referee { get; set; }
    public string FirstAssistant{ get; set; }
    public string SeccondAssistant{ get; set; }
    public int RoundID{ get; set; }
    public int MatchID{ get; set; }
    public string HomeTeamInitials{ get; set; }
    public string AwayTeamInitials{ get; set; }

    }
}