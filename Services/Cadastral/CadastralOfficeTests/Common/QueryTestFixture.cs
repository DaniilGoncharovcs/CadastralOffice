namespace CadastralOfficeTests.Common;

public class QueryTestFixture : IDisposable
{
    public AppDbContext Context;
    public IMapper Mapper;

    public QueryTestFixture()
    {
        Context = TestDbContextFactory.Create();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(
                typeof(IAppDbContext).Assembly));
        });

        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
        => TestDbContextFactory.Destroy(Context);
}
