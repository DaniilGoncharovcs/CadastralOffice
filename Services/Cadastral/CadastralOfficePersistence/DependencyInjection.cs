namespace CadastralOfficePersistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
    IConfiguration configuration,
    bool isDevelopment)
    {
        if (isDevelopment)
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Test");
            });

        else
        {
            var connectionString = configuration["ConnectionString"];

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        services.AddScoped<IAppDbContext>(provider =>
            provider.GetService<AppDbContext>()
        );

        return services;
    }
}