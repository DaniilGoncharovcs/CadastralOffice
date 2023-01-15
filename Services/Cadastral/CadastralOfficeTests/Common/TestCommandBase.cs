namespace CadastralOfficeTests.Common;
public abstract class TestCommandBase : IDisposable
{
    protected readonly AppDbContext Context;

    public TestCommandBase()
        => Context = TestDbContextFactory.Create();

    public void Dispose()
        => TestDbContextFactory.Destroy(Context);
}
