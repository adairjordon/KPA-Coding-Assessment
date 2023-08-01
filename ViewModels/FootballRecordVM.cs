using KPA_JTA2023_Coding_Assessment.Models;

namespace KPA_JTA2023_Coding_Assessment.ViewModels
{
    public class FootballRecordVM
    {

        public int Id { get; set; }


        public DateTime ModifiedDate { get; set; }


        public DateTime CreatedDate { get; set; }


        public int FootballTeamId { get; set; }


        public int TeamWins { get; set; }


        public int TeamLosses { get; set; }


        public string TeamName { get; set; }
    }
}
