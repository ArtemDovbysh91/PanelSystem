using Cint.CodingChallenge.Model.DBSet;
using Microsoft.EntityFrameworkCore;

namespace Cint.CodingChallenge.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Survey> Surveys { get; set; }

        public DatabaseContext() { }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)  {  }
    }
}
