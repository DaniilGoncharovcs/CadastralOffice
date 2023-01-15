namespace IdentityWebApi.Data;

public class AppDbContext : IdentityDbContext<User>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
        Database.EnsureCreated();
	}
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var ADMIN_ID = Guid.NewGuid().ToString();
        var ADMIN_ROLE_ID = Guid.NewGuid().ToString();
        var MANAGER_ROLE_ID = Guid.NewGuid().ToString();

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID
            },
            new IdentityRole
            {
                Name = "manager",
                NormalizedName = "MANAGER",
                Id = MANAGER_ROLE_ID,
                ConcurrencyStamp = MANAGER_ROLE_ID
            });

        var adminUser = new User
        {
            Id = ADMIN_ID,
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
        };

        var hasher = new PasswordHasher<User>();
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "~Q3GR9n$mq~qq2fK");

        builder.Entity<User>().HasData(adminUser);

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ADMIN_ROLE_ID,
            UserId = ADMIN_ID,
        });

        var i = 1;
    }
}