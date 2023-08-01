using System.ComponentModel.DataAnnotations.Schema;

namespace KPA_JTA2023_Coding_Assessment.Models
{
    [Table("football_record")]
    public class football_record
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("football_team_id")]
        public int FootballTeamId { get; set; }

        [Column("team_wins")]
        public int TeamWins { get; set; }

        [Column("team_losses")]
        public int TeamLosses { get; set; }

        [ForeignKey("FootballTeamId")]
        public virtual football_team FootballTeam{ get; set; }
    }
}
