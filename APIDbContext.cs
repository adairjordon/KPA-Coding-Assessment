using KPA_JTA2023_Coding_Assessment.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace KPA_JTA2023_Coding_Assessment
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=kpa-coding-assessment-JTA2023;User Id=appuser;Password=adminpassword;TrustServerCertificate=True;Trusted_Connection=False;MultipleActiveResultSets=true;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<football_record> football_record
        {
            get;
            set;
        }
        public DbSet<football_team> football_team
        {
            get;
            set;
        }
    }
}
