using System.ComponentModel.DataAnnotations.Schema;

namespace KPA_JTA2023_Coding_Assessment.Models
{
    [Table("football_team")]
    public class football_team
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("team_name")]
        public string TeamName { get; set; }
    }
}
