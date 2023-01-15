namespace CadastralOfficePersistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Document> Documents { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}