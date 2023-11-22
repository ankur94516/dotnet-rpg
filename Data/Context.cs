

namespace dotnet_rpg.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        public DbSet<Character> Characters { get; set; }
        
    }
}